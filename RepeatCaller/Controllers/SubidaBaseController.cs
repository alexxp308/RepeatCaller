using RepeatCaller.Librerias.BL;
using RepeatCaller.Models;
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

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public int CargarTabla(BaseDTO laBase)
        {
            int result = 0;
            blBase oblBase = new blBase();
            string path = System.Web.HttpContext.Current.Server.MapPath("~/Doc/") + laBase.nombreArchivo;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(path, System.Text.Encoding.Default))
            {
                try
                {
                    string[] columnas, filas, cabes;
                    string data = sr.ReadToEnd();
                    filas = System.Text.RegularExpressions.Regex.Split(data, Environment.NewLine);
                    cabes = System.Text.RegularExpressions.Regex.Split(filas[0], "\\t");
                    System.Data.DataTable dt = new System.Data.DataTable("TbReporte");
                    dt.Columns.Add("BaseId", Type.GetType("System.String"));
                    for (int i = 0; i < cabes.Length; i++)
                    {
                        cabes[i] = (cabes[i].Substring(0, 1) == " ") ? cabes[i].Substring(1, cabes[i].Length-1) : ((cabes[i].Substring(cabes[i].Length-1, 1) == " ")? cabes[i].Substring(1, cabes[i].Length-2):cabes[i]);
                        dt.Columns.Add(cabes[i], Type.GetType("System.String"));
                    }
                    System.Data.DataRow row = null;
                    for (int i = 1; i < filas.Length; i++)
                    {
                        columnas = System.Text.RegularExpressions.Regex.Split(filas[i], "\\t");
                        row = dt.NewRow();
                        row["BaseId"] = laBase.baseId;
                        for (int j = 0; j < columnas.Length; j++)
                        {
                           row[cabes[j]] = columnas[j].ToString();
                        }
                        dt.Rows.Add(row);
                    }

                    result = oblBase.CargarTabla(dt, laBase.tipo);
                }
                catch (Exception ex)
                {
                    string url = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                    General.Librerias.CodigoUsuario.Log.Error(blGeneral.logPath, "SubidaBaseController_CargarTabla", url, ex);
                }
            }

            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string getBaseCargada(BaseDTO laBase)
        {
            string result = "";
            blBase olbBase = new blBase();
            result = olbBase.getBaseCargada(laBase.campaniaId,laBase.tipo,laBase.fechaBase);
            return result;
        }
    }
}