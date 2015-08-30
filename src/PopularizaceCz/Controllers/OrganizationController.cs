using Microsoft.AspNet.Mvc;
using PopularizaceCz.DataLayer.Repositories;
using PopularizaceCz.ViewModels;
using System.Threading.Tasks;

namespace PopularizaceCz.Controllers
{
    public sealed class OrganizationController : Controller
    {
        private IOrganizationRepository _orgs;

        public OrganizationController(IOrganizationRepository orgs)
        {
            this._orgs = orgs;
        }

        public async Task<IActionResult> Show(int id)
        {
            return View(new OrganizationViewModel { DbModel = await this._orgs.GetById(id) });
        }
    }
}