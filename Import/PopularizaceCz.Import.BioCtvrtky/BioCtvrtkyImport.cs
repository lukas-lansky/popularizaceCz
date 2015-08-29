using HtmlAgilityPack;
using PopularizaceCz.Import.Common;
using System;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PopularizaceCz.Import.BioCtvrtky
{
    public class BioCtvrtkyImport : IImport
    {
        public readonly string Url = "http://bio.natur.cuni.cz/~leb/historie.php?skrok=vse";

        public async Task<string> Import()
        {
            var query = new StringBuilder();

            this.AssertDbState(query);

            var doc = await this.GetDocument();

            foreach (var row in doc.DocumentNode.SelectNodes("//tr"))
            {
                var cells = row.SelectNodes("td");
                if (cells == null || cells.Count < 3)
                {
                    continue;
                }

                this.ProcessRow(row, query);
            }

            return query.ToString();
        }

        private void AssertDbState(StringBuilder query)
        {
            query.AppendLine(@"
IF NOT EXISTS(SELECT 1 FROM [Organization] WHERE [Name]=N'Biologické čtvrtky')
BEGIN
    INSERT INTO [Organization] ([Name])
    VALUES (N'Biologické čtvrtky')
END");

            query.AppendLine(@"
IF NOT EXISTS(SELECT 1 FROM [Venue] WHERE [Name]=N'Viničná')
BEGIN
    INSERT INTO [Venue] ([Name])
    VALUES (N'Viničná')
END");
        }

        private async Task<HtmlDocument> GetDocument()
        {
            var htmlBytes = await new WebClient().DownloadDataTaskAsync(this.Url);
            var htmlString = Encoding.GetEncoding(1250).GetString(htmlBytes);

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            return doc;
        }

        private void ProcessRow(HtmlNode row, StringBuilder query)
        {
            var cells = row.SelectNodes("td");

            DateTime date = new DateTime(1900, 1, 1);
            DateTime.TryParseExact(
                cells[0].InnerText.Replace("&nbsp;", "").Trim(),
                "dd. MM. yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AllowInnerWhite,
                out date);

            var speaker = cells[1].InnerText.Replace("&nbsp;", "").Trim();
            var name = cells[2].InnerText.Replace("&nbsp;", "").Trim();

            if (string.IsNullOrEmpty(speaker) || string.IsNullOrEmpty(name))
            {
                return;
            }

            query.AppendLine(string.Format(@"
IF NOT EXISTS (SELECT 1 FROM [Person] WHERE [Name]=N'{1}')
BEGIN
    INSERT INTO [Person] ([Name])
    VALUES (N'{1}')
END

IF NOT EXISTS (SELECT 1 FROM [Talk] WHERE [Name]=N'{2}')
BEGIN
    INSERT INTO [Talk] ([Name], [Start], [VenueId])
    VALUES (N'{2}', '{0}', (SELECT [Id] FROM [Venue] WHERE [Name]=N'Viničná'))

    INSERT INTO [TalkOrganizer] ([TalkId], [OrganizationId])
    VALUES ((SELECT [Id] FROM [Talk] WHERE [Name]=N'{2}'), (SELECT [Id] FROM [Organization] WHERE [Name]=N'Biologické čtvrtky'))

    INSERT INTO [TalkSpeaker] ([TalkId], [PersonId])
    VALUES ((SELECT [Id] FROM [Talk] WHERE [Name]=N'{2}'), (SELECT [Id] FROM [Person] WHERE [Name]=N'{1}'))
END
", date.ToString("yyyy-MM-dd"), speaker.Replace("'", "''"), name.Replace("'", "''")));
        }
    }
}
