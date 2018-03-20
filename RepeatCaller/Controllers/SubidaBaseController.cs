using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepeatCaller.Controllers
{
    public class SubidaBaseController : Controller
    {
        // GET: SubidaBase
        public ActionResult Index()
        {
            return View("~/Views/SubidaBase/SubidaBase.cshtml");
        }
    }
}