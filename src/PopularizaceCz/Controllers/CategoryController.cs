using Microsoft.AspNet.Mvc;
using PopularizaceCz.DataLayer.Repositories;
using PopularizaceCz.ViewModels;
using System.Threading.Tasks;

namespace PopularizaceCz.Controllers
{
    public sealed class CategoryController : Controller
    {
        private ICategoryRepository _cats;

        public CategoryController(ICategoryRepository cats)
        {
            this._cats = cats;
        }

        public async Task<IActionResult> Show(int id)
        {
            return View(new CategoryViewModel { DbModel = await this._cats.GetById(id) });
        }
    }
}