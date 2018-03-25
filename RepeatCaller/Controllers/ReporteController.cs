#region using
using RepeatCaller.Models;
using System.Web.Mvc;
using RepeatCaller.Librerias.BL;
using RepeatCaller.Librerias.EL;
using Newtonsoft.Json;
#endregion

namespace RepeatCaller.Controllers
{
    [Authorize]
    public class ReporteController : Controller
    {
        [Authorize(Roles = "Ejecutivo,Supervisor")]
        public ActionResult Index()
        {
            return View("~/Views/Reporte/Reporte.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string Reportes(ReporteDTO reporte)
        {
            string result = "";
            
            blReporte oblReporte = new blReporte();
            if(reporte.tipo == 1)
            {
                elReporteCruce elReporteCruce = new elReporteCruce();
                elReporteCruce = oblReporte.ReporteCruceDatos(reporte.campaniaId, reporte.fechaBase);
                result = JsonConvert.SerializeObject(elReporteCruce);
            }
            return result;
        }
    }
}