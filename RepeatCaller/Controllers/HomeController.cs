using System.Web.Mvc;

namespace RepeatCaller.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Roles = "Ejecutivo,Supervisor")]
        public ActionResult Index()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}