using Microsoft.AspNet.Identity;
using System.Security.Claims;
using System.Web.Mvc;

namespace TwitterWebApplication.Controllers
{
    // [RequireHttps]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;
            // Try to profile image
            if (User.Identity.GetUserId() != null && claimsIdentity != null)
            {
                var claims = claimsIdentity.Claims;
                // Download the twitter profile image
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}