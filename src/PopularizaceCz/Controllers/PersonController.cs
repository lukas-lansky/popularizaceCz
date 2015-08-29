using Microsoft.AspNet.Mvc;
using PopularizaceCz.DataLayer.Repositories;
using PopularizaceCz.ViewModels;
using System.Threading.Tasks;

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