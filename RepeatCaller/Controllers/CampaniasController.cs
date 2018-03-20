using RepeatCaller.Librerias.BL;
using RepeatCaller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepeatCaller.Controllers
{
    [Authorize]
    public class CampaniasController : Controller
    {
        [Authorize(Roles = "Ejecutivo")]
        public ActionResult Index()
        {
            return View("~/Views/Campanias/Campanias.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string listarCampanias(CampaniaDTO campania)
        {
            string result = "";
            blCampania oblCampania = new blCampania();
            result = oblCampania.listarCampanias(campania.idSede);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public string guardarCampania(CampaniaDTO campania)
        {
            string result = "";
            blCampania oblCampania = new blCampania();
            result = oblCampania.guardarCampania(campania.nombreCampania, campania.idSede);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public string actualizarCampania(CampaniaDTO campania)
        {
            string result = "";
            blCampania oblCampania = new blCampania();
            result = oblCampania.actualizarCampania(campania.idSede, campania.nombreCampania, campania.idCampania);
            return result;
        }
    }
}