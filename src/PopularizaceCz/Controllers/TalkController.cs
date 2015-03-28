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

namespace PopularizaceCz.Controllers
{
    public sealed class TalkController : Controller
    {
        private IDbConnection _db;

        public TalkController(IDbConnection db)
        {
            this._db = db;
        }

        public async Task<IActionResult> Show(int id)
        {
            var talk = (await this._db.QueryAsync<TalkDbModel>("SELECT * FROM [Talk] WHERE [Id]=@Id", new { Id = id })).Single();
            
            return View(new TalkViewModel { DbModel = talk });
        }
    }
}