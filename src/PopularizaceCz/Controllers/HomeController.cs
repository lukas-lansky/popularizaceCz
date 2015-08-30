using System;
using Microsoft.AspNet.Mvc;
using PopularizaceCz.ViewModels;
using System.Threading.Tasks;
using PopularizaceCz.DataLayer.Repositories;

namespace PopularizaceCz.Controllers
{
    public sealed class HomeController : Controller
    {
        private IPersonRepository _persons { get; set; }

        private ITalkRepository _talks { get; set; }

        private IOrganizationRepository _orgs { get; set; }

        private ICategoryRepository _cats { get; set; }

        public HomeController(IPersonRepository persons, ITalkRepository talks, IOrganizationRepository orgs, ICategoryRepository cats)
        {
            if (persons == null) throw new ArgumentNullException();
            if (talks == null) throw new ArgumentNullException();

            this._persons = persons;
            this._talks = talks;
            this._orgs = orgs;
            this._cats = cats;
        }

        public async Task<IActionResult> Index()
        {
            return View(new HomepageViewModel {
                UpcomingTalks = await this._talks.GetUpcomingTalks(),
                FrequentSpeakers = await this._persons.GetPersonsWithMostTalks(),
                FrequentOrganizers = await this._orgs.GetOrganizationsWithMostTalks(),
                FrequentCategories = await this._cats.GetCategoriesWithMostTalks() });
        }
        
        public IActionResult Error()
        {
            return View("~/Views/Shared/Error.cshtml");
        }
    }
}