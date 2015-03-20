using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using PopularizaceCz.Database.Infrastructure;
using PopularizaceCz.ViewModels;
using PopularizaceCz.Database;

namespace PopularizaceCz.Controllers
{
    public sealed class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new HomepageViewModel { UpcomingTalks = new List<TalkDbEntity>() });
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