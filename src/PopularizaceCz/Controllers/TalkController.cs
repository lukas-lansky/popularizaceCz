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

namespace PopularizaceCz.Controllers
{
    public sealed class TalkController : Controller
    {
        private ITalkRepository _talks;

        public TalkController(ITalkRepository talks)
        {
            this._talks = talks;
        }

        public async Task<IActionResult> Show(int id)
        {
            return View(new TalkViewModel { DbModel = await this._talks.GetById(id) });
        }
    }
}