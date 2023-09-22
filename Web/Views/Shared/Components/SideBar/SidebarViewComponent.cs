using Microsoft.AspNetCore.Mvc;

namespace Web.Views.Shared.Components.SideBar
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
