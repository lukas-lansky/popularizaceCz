using Microsoft.AspNet.Mvc;
using PopularizaceCz.DataLayer.Entities;

namespace PopularizaceCz.ViewComponents
{
    public sealed class TalkListItemViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(TalkDbEntity talk)
        {
            return View(talk);
        }
    }
}
