using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using PopularizaceCz.Database.Infrastructure;
using PopularizaceCz.ViewModels;
using PopularizaceCz.Database;
using PopularizaceCz.Helpers;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using PopularizaceCz.Repositories;
using PopularizaceCz.Services.ICalExport;

namespace PopularizaceCz.Controllers
{
    public sealed class TalkController : Controller
    {
        private ITalkRepository _talks;

		private IICalExporter _iCalExporter;

        public TalkController(ITalkRepository talks, IICalExporter iCalExporter)
        {
            this._talks = talks;
			this._iCalExporter = iCalExporter;
        }

        public async Task<IActionResult> Show(int id)
        {
            return View(new TalkViewModel { DbModel = await this._talks.GetById(id) });
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