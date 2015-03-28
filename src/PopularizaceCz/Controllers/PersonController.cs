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
    public sealed class PersonController : Controller
    {
        private IPersonRepository _persons;

        public PersonController(IPersonRepository persons)
        {
            this._persons = persons;
        }

        public async Task<IActionResult> Show(int id)
        {
            return View(new PersonViewModel { DbModel = await this._persons.GetById(id) });
        }
    }
}