using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace ExpirationDate.Controllers
{
    public class BaseController<T> : Controller where T : Controller
    {
        protected readonly IStringLocalizer<T> _localizer;

        public BaseController(IStringLocalizer<T> localizer)
        {
            _localizer = localizer;
        }

        protected void SetViewData()
        {
            ViewData["Title"] = _localizer["Home"];
            ViewData["CurrentCulture"] = Thread.CurrentThread.CurrentCulture.Name;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            if (string.IsNullOrEmpty(culture))
            {
                culture = "en-US";
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            if (string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = Url.Action("Index", "Home");
            }

            return LocalRedirect(returnUrl);
        }
    }
}
