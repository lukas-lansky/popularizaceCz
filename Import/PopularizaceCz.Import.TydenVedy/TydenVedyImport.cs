using HtmlAgilityPack;
using PopularizaceCz.Import.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PopularizaceCz.Import.TydenVedy
{
    public class TydenVedyImport : IImport
    {
        private string Url = "http://www.tydenvedy.cz/sys/search.jsp?fulltext_search_all_words=Praha+prednaska&fulltext_search_pager_number_of_pages=200&search=Hledat&fulltext_search_exact_phrase=&fulltext_search_any_words=&fulltext_search_not_contain_words=&fulltext_search_file_formats=anytype&fulltext_search_date=anytime&order_asc=true&order_title=true&q1=Praha&q2=&q3=prednaska&q4=&searchFields=keywords";

        public async Task<string> Import()
        {
            var sourceCode = await GetPage(this.Url);

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(sourceCode);

            var sql = new StringBuilder();
            var orgName = "Týden vědy a techniky 2015";
            sql.AppendLine($@"

IF NOT EXISTS (SELECT 1 FROM [Organization] [o] WHERE [o].[Name]=N'{orgName}')
BEGIN
    INSERT INTO [Organization] ([Name], [Url])
    VALUES (N'{orgName}', N'http://www.tydenvedy.cz/')
END

");

            foreach (var div in htmlDoc.DocumentNode
                .SelectNodes("//div")
                .Where(d => d.Attributes.Any(a => a.Name == "class" && a.Value == "record")))
            {
                var href = "http://www.tydenvedy.cz/" + div.SelectSingleNode("h3/a").Attributes.Where(a => a.Name == "href").FirstOrDefault()?.Value;
                
                var subContent = await GetPage(href);
                var subHtmlDoc = new HtmlDocument();
                subHtmlDoc.LoadHtml(subContent);

                var talkData = this.ParseTalk(href, subHtmlDoc, sql);

                if (talkData == null) continue;

                sql.AppendLine($@"

IF NOT EXISTS (SELECT 1 FROM [Venue] [v] WHERE [v].[Name]=N'{talkData.VenueName}')
BEGIN
    INSERT INTO [Venue] ([Name], [Latitude], [Longitude])
    VALUES (N'{talkData.VenueName}', {talkData.VenueLatitude}, {talkData.VenueLongitude})
END

IF NOT EXISTS (SELECT 1 FROM [Talk] [t] WHERE [t].[Url]=N'{talkData.TalkUrl}')
BEGIN
    INSERT INTO [Talk] ([Name], [Url], [Start], [VenueId])
    VALUES (
        N'{talkData.TalkName}', N'{talkData.TalkUrl}', '{talkData.TalkStart.ToString("yyyy-MM-dd HH:mm:ss")}',
        (SELECT [v].[Id] FROM [Venue] [v] WHERE [v].[Name]=N'{talkData.VenueName}'))
END

IF NOT EXISTS (SELECT 1 FROM [Person] p WHERE p.[Name]=N'{talkData.SpeakerName}')
BEGIN
    INSERT INTO [Person] ([Name])
    VALUES (N'{talkData.SpeakerName}')
END

INSERT INTO [TalkSpeaker] ([TalkId], [PersonId])
VALUES (
    (SELECT [t].[Id] FROM [Talk] [t] WHERE [t].[Url]=N'{talkData.TalkUrl}'),
    (SELECT [p].[Id] FROM [Person] [p] WHERE [p].[Name]=N'{talkData.SpeakerName}'))

INSERT INTO [TalkOrganizer] ([TalkId], [OrganizationId])
VALUES (
    (SELECT [t].[Id] FROM [Talk] [t] WHERE [t].[Url]=N'{talkData.TalkUrl}'),
    (SELECT [o].[Id] FROM [Organization] [o] WHERE [o].[Name]=N'{orgName}'))

                ");

                foreach (var categoryName in talkData.Categories)
                {
                    sql.AppendLine($@"

IF NOT EXISTS (SELECT 1 FROM [Category] c WHERE c.[Name]=N'{categoryName}')
BEGIN
    INSERT INTO [Category] ([Name])
    VALUES (N'{categoryName}')
END

INSERT INTO [TalkCategory] ([TalkId], [CategoryId])
VALUES (
    (SELECT [t].[Id] FROM [Talk] [t] Where [t].[Url]=N'{talkData.TalkUrl}'),
    (SELECT [c].[Id] FROM [Category] [c] WHERE [c].[Name]=N'{categoryName}'))

");
                }

                sql.AppendLine(@"

GO

");
            }
            
            return sql.ToString();
        }

        private class TalkData
        {
            public string TalkUrl { get; set; }

            private string _talkName;
            public string TalkName {
                get { return _talkName; }
                set { _talkName = value.Replace("'", "''"); }
            }

            public DateTime TalkStart { get; set; }

            public string SpeakerName { get; set; }
            
            public IEnumerable<string> Categories { get; set; }

            private string _venueName;
            public string VenueName {
                get { return _venueName; }
                set { _venueName = value.Replace("'", "''"); }
            }

            public decimal VenueLatitude { get; set; }

            public decimal VenueLongitude { get; set; }
        }

        private TalkData ParseTalk(string url, HtmlDocument doc, StringBuilder sql)
        {
            var talkName = doc.DocumentNode.SelectSingleNode("//h1").InnerText.Replace("&nbsp;", " ").Trim();

            /* categories */

            var talkCategories = doc.DocumentNode
                .SelectNodes("//*[contains(@class,'event-keyw2')]")
                ?.Select(node => node.InnerText.Trim()) ?? new List<string>();

            /* speaker */

            var speaker = doc.DocumentNode
                .SelectNodes("//*[contains(@id,'m2-event-detail')]/p/p")
                ?.Where(node => node.InnerText.Contains("Přednášející:"))
                ?.Select(node => node.InnerText.Trim()).FirstOrDefault() ?? "";
            speaker = speaker.Split('\n').FirstOrDefault().Trim() ?? "";
            speaker = string.Join(" ", speaker
                .Split(' ')
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Where(p => !p.EndsWith("."))
                .TakeUntil(p => p.EndsWith(",")));
            if (speaker.StartsWith("Přednášející: "))
            {
                var subslen = "Přednášející: ";
                speaker = speaker.Substring(subslen.Length, speaker.Length - subslen.Length);
            }
            if (speaker.EndsWith(","))
            {
                speaker = speaker.Substring(0, speaker.Length - 1);
            }

            /* venue */

            var venueNodes = doc.DocumentNode
                .SelectNodes("//*[contains(@id,'m2-event-detail')]/p/p")
                ?.Where(node => node.InnerText.Contains("Místo konání:"));
            var venue = venueNodes?.Select(node => node.InnerText.Trim()).FirstOrDefault() ?? "";
            venue = venue.Replace("\r", "").Replace("\n", " ").Trim();
            if (venue.StartsWith("Místo konání:  "))
            {
                var subslen = "Místo konání:  ";
                venue = venue.Substring(subslen.Length, venue.Length - subslen.Length);
            }

            var hrefAttr = doc.DocumentNode
                .SelectNodes("//*[contains(@id,'m2-event-detail')]//a")
                ?.Where(a => a.Attributes.Any(aattr => aattr.Name == "href" && aattr.Value.StartsWith("http://mapy.cz/")))
                ?.FirstOrDefault()
                ?.Attributes
                ?.FirstOrDefault(aattr => aattr.Name == "href");

            decimal latitude = 0, longitude = 0;
            if (hrefAttr != null)
            {
                var relPart = hrefAttr.Value
                    .Split('?')[1]
                    .Split(new[] { "&amp;" }, StringSplitOptions.None)
                    .Select(p => p.Substring(2))
                    .ToArray();

                latitude = Convert.ToDecimal(relPart[0]);
                longitude = Convert.ToDecimal(relPart[1]);
            }

            /* start */

            var startDateParts = (doc.DocumentNode.SelectSingleNode("//*[contains(@class,'bluebox')]")?.InnerText ?? "")
                .Split(',')
                .Select(p => p.Trim())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .ToArray();

            if (startDateParts.Length != 2)
            {
                return null;
            }

            var dateParts = startDateParts[0].Split('.');
            var timeParts = startDateParts[1].Split(':');

            DateTime startDate;
            try
            {
                startDate = new DateTime(2015, Convert.ToInt32(dateParts[1]), Convert.ToInt32(dateParts[0]), Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]), 0);
            }
            catch
            {
                return null;
            }

            /* done! */

            if (string.IsNullOrWhiteSpace(speaker) || string.IsNullOrWhiteSpace(venue))
            {
                return null;
            }
            
            return new TalkData {
                TalkUrl = url,
                TalkName = talkName,
                TalkStart = startDate,
                SpeakerName = speaker,
                Categories = talkCategories,
                VenueName = venue,
                VenueLatitude = latitude,
                VenueLongitude = longitude
            };
        }

        private async Task<string> GetPage(string url)
        {
            var folderName = "temp";

            if (!Directory.Exists(folderName))
            {
                Directory.CreateDirectory(folderName);
            }

            var fileName = GetHash(url);

            if (File.Exists(folderName + "/" + fileName))
            {
                return File.ReadAllText(folderName + "/" + fileName);
            }

            var fileByteContent = await new WebClient().DownloadDataTaskAsync(url);
            var fileContent = Encoding.UTF8.GetString(fileByteContent);

            File.WriteAllText(folderName + "/" + fileName, fileContent);
            
            return fileContent;
        }

        private string GetHash(string name)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(name);
            SHA1CryptoServiceProvider cryptoTransformSHA1 = new SHA1CryptoServiceProvider();
            return BitConverter.ToString(cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> TakeUntil<T>(this IEnumerable<T> source, Func<T, bool> condition)
        {
            foreach (var item in source)
            {
                yield return item;

                if (condition(item))
                {
                    yield break;
                }
            }
        }
    }
}
