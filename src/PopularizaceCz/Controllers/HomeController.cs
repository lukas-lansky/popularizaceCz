using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using PopularizaceCz.Database.Infrastructure;
using PopularizaceCz.ViewModels;
using PopularizaceCz.Database;
using PopularizaceCz.Repositories;
using System.Threading.Tasks;

namespace PopularizaceCz.Controllers
{
    public sealed class HomeController : Controller
    {
        private IPersonRepository _persons { get; set; }

        private ITalkRepository _talks { get; set; }

        public HomeController(IPersonRepository persons, ITalkRepository talks)
        {
            if (persons == null) throw new ArgumentNullException();
            if (talks == null) throw new ArgumentNullException();

            this._persons = persons;
            this._talks = talks;
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomepageViewModel {
                UpcomingTalks = await this._talks.GetUpcomingTalks(),
                FrequentSpeakers = await this._persons.GetPersonsWithMostTalks() });
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}