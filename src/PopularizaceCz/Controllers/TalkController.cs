using System.Linq;
using Microsoft.AspNet.Mvc;
using PopularizaceCz.ViewModels;
using System.Threading.Tasks;
using PopularizaceCz.Services.ICalExport;
using PopularizaceCz.DataLayer.Repositories;
using PopularizaceCz.Services.YouTube;

namespace PopularizaceCz.Controllers
{
    public sealed class TalkController : Controller
    {
        private ITalkRepository _talks;

        private IPersonRepository _persons;

        private IOrganizationRepository _organizations;

        private IUserRepository _users;

        private IYouTubeLinker _ytLinker;

		private IICalExporter _iCalExporter;

        public TalkController(
            ITalkRepository talks, IPersonRepository persons, IOrganizationRepository organizations, IUserRepository users,
            IYouTubeLinker ytLinker, IICalExporter iCalExporter)
        {
            this._talks = talks;
            this._persons = persons;
            this._organizations = organizations;
            this._users = users;

            this._ytLinker = ytLinker;
			this._iCalExporter = iCalExporter;
        }

        public async Task<IActionResult> Show(int id)
        {
            var dbModel = await this._talks.GetById(id);

            var ytRecording = dbModel?.Recordings?.FirstOrDefault(r => r.YouTubeVideoId != null);

            return View(new TalkViewModel {
                DbModel = dbModel,
                CurrentUser = await this._users.GetCurrentUser(),
                YouTubeVideoUrl = ytRecording == null ? null : _ytLinker.GetImageLink(ytRecording.YouTubeVideoId),
                YouTubeImageUrl = ytRecording == null ? null : _ytLinker.GetVideoLink(ytRecording.YouTubeVideoId)
            });
        }

        public async Task<IActionResult> Edit(int id, TalkEditViewModel model)
        {
            if (!string.IsNullOrEmpty(model?.DbModel?.Name))
            {
                model.DbModel.Id = id;

                await this._talks.Update(model.DbModel);

                return RedirectToAction("Show", new { id });
            }

            return View(new TalkEditViewModel {
                DbModel = await this._talks.GetById(id),
                AllSpeakers = await this._persons.GetAllPersons(),
                AllOrganizations = await this._organizations.GetAllOrganizations() });
        }
		
		public async Task<IActionResult> ExportUpcoming(string format = "ical")
		{
			if (format.Trim().ToLower() == "ical")
			{
				var upcomingTalks = await this._talks.GetUpcomingTalks(take: 100);

				var iCal = this._iCalExporter.Export(upcomingTalks.Select(t => new ICalEvent { Name = t.Name, Start = t.Start }));

				return Content(iCal, "text/calendar");
			}

			return new HttpStatusCodeResult(404);
		}
    }
}