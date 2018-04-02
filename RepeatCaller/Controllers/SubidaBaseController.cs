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
    public class SubidaBaseController : Controller
    {
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public ActionResult Index()
        {
            return View("~/Views/SubidaBase/SubidaBase.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string CargarTabla(BaseDTO laBase)
        {
            string result = "";
            int cantFilas = 0;
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
                    dt.Columns.Add("campaniaId", Type.GetType("System.String"));
                    dt.Columns.Add("fechaBase", Type.GetType("System.String"));
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
                        row["campaniaId"] = laBase.campaniaId;
                        row["fechaBase"] = laBase.fechaBase;
                        for (int j = 0; j < columnas.Length; j++)
                        {
                           row[cabes[j]] = columnas[j].ToString();
                        }
                        dt.Rows.Add(row);
                    }

                    cantFilas = oblBase.CargarTabla(dt, laBase.tipo,laBase.campaniaId,laBase.fechaBase,laBase.baseId);
                    if(cantFilas == 0) result = oblBase.backBaseAnterior(laBase.tipo, laBase.campaniaId, laBase.fechaBase, laBase.baseId);
                }
                catch (Exception ex)
                {
                    result = oblBase.backBaseAnterior(laBase.tipo, laBase.campaniaId, laBase.fechaBase, laBase.baseId);
                }
            }

            return (cantFilas == 0) ? result : cantFilas+"";
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

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string obtenerBases(BaseDTO laBase)
        {
            string result = "";
            blBase olbBase = new blBase();
            result = olbBase.obtenerBases(laBase.campaniaId, laBase.tipo, laBase.fechaBase);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string verStatus(BaseDTO laBase)
        {
            string result = "";
            blBase olbBase = new blBase();
            result = olbBase.verStatus(laBase.campaniaId, laBase.fechaBase);
            return result;
        }
    }
}