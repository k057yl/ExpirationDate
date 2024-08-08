using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;


namespace ExpirationDate.Controllers
{
    [Authorize]
    public class HomeController : BaseController<HomeController>
    {
        public HomeController(IStringLocalizer<HomeController> localizer) : base(localizer)
        {
        }
        public IActionResult Index()
        {
            SetViewData();
            return View();
        }

        public IActionResult Privacy()
        {
            SetViewData();
            return View();
        }            
    }
}