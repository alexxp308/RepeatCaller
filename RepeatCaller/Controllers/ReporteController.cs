#region using
using RepeatCaller.Models;
using System.Web.Mvc;
using RepeatCaller.Librerias.BL;
using RepeatCaller.Librerias.EL;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using SpreadsheetGear;
using System.Drawing;
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
            IWorkbook Libro = Factory.GetWorkbook(System.Globalization.CultureInfo.CurrentCulture);

            IWorksheet Hoja = Libro.Worksheets[0];
            IRange celda = Hoja.Cells;
            Int32 Fila_Inicio = 2;
            string exclName = "";
            blReporte oblReporte = new blReporte();
            if (reporte.tipo == 1)
            {
                elReporteCruce elReporteCruce = new elReporteCruce();
                elReporteCruce = oblReporte.ReporteCruceDatos(reporte.campaniaId, reporte.fechaBase);

                //celda["A1:DZ5000"].Interior.Color = Color.FromArgb(255, 255, 255);
                celda["A2:F2"].Merge();
                celda["A2:F2"].Value = "DETALLE";
                celda["A2:F2"].Font.Size = 16;
                celda["A2:F2"].Font.Bold = true;
                Hoja.Name = "Detalle";

                Fila_Inicio++;

                //////////////// INICIO DE CABECERA ////////////////
                int num = 1;
                string[] cabeceras = { "Agente", "Fecha llamada", "Numero", "Titulo interacción" };
                for (int i = 0; i < 4; i++)
                {
                    celda[Fila_Inicio, num].Font.Bold = true;
                    celda[Fila_Inicio, num].ColumnWidth = ((i==3)?100:15);
                    celda[Fila_Inicio, num].Interior.Color = Color.FromArgb(56, 96, 146);
                    celda[Fila_Inicio, num].Font.Color = Color.FromArgb(255, 255, 255);
                    celda[Fila_Inicio, num].Font.Name = "Arial";
                    celda[Fila_Inicio, num].HorizontalAlignment = HAlign.Center;
                    celda[Fila_Inicio, num].Font.Size = 10;
                    celda[Fila_Inicio, num].Value = cabeceras[i];
                    num++;
                }
                //int borInicio = Fila_Inicio + 1;
                List<elrcDetalle> datosDetalle = elReporteCruce.elDetalle;
                for (int i = 0; i < datosDetalle.Count; i++)
                {
                    Fila_Inicio++;
                    celda[Fila_Inicio, 1].HorizontalAlignment = HAlign.Center;
                    celda[Fila_Inicio,1].Value = datosDetalle[i].Agente;
                    celda[Fila_Inicio, 2].HorizontalAlignment = HAlign.Center;
                    celda[Fila_Inicio, 2].Value = datosDetalle[i].FechaLlamada;
                    celda[Fila_Inicio, 3].HorizontalAlignment = HAlign.Center;
                    celda[Fila_Inicio, 3].Value = datosDetalle[i].Numero;
                    celda[Fila_Inicio, 4].HorizontalAlignment = HAlign.Left;
                    celda[Fila_Inicio,4].Value = datosDetalle[i].TituloInteraccion;
                }

                /*for (Int32 j = borInicio; j <= Fila_Inicio; j++)
                {
                    for (Int32 x = 1; x < num; x++)
                    {
                        celda[j, x].Borders[BordersIndex.EdgeRight].LineStyle = LineStyle.Continuous;
                        celda[j, x].Borders[BordersIndex.EdgeRight].Color = Color.FromArgb(200, 200, 200);
                        celda[j, x].Borders[BordersIndex.EdgeTop].LineStyle = LineStyle.Continuous;
                        celda[j, x].Borders[BordersIndex.EdgeTop].Color = Color.FromArgb(200, 200, 200);
                        celda[j, x].Borders[BordersIndex.EdgeBottom].LineStyle = LineStyle.Continuous;
                        celda[j, x].Borders[BordersIndex.EdgeBottom].Color = Color.FromArgb(200, 200, 200);
                        celda[j, x].Borders[BordersIndex.EdgeLeft].LineStyle = LineStyle.Continuous;
                        celda[j, x].Borders[BordersIndex.EdgeLeft].Color = Color.FromArgb(200, 200, 200);
                    }
                }*/

                IWorksheet hoja2 = Libro.Worksheets.Add();
                IRange celd2 = hoja2.Cells;
                Fila_Inicio = 2;

                celd2["A2:F2"].Merge();
                celd2["A2:F2"].Value = "REPORTE GENERAL DE TOTALES NUMERO";
                celd2["A2:F2"].Font.Size = 16;
                celd2["A2:F2"].Font.Bold = true;

                hoja2.Name = "Reporte_Numero";
                
                Fila_Inicio++;

                //////////////// INICIO DE CABECERA ////////////////
                num = 1;
                cabeceras = new string[] {"Fecha llamada", "Número", "Total" };
                for (int i = 0; i < 3; i++)
                {
                    celd2[Fila_Inicio, num].Font.Bold = true;
                    celd2[Fila_Inicio, num].ColumnWidth = 15;
                    celd2[Fila_Inicio, num].Interior.Color = Color.FromArgb(56, 96, 146);
                    celd2[Fila_Inicio, num].Font.Color = Color.FromArgb(255, 255, 255);
                    celd2[Fila_Inicio, num].Font.Name = "Arial";
                    celd2[Fila_Inicio, num].HorizontalAlignment = HAlign.Center;
                    celd2[Fila_Inicio, num].Font.Size = 10;
                    celd2[Fila_Inicio, num].Value = cabeceras[i];
                    num++;
                }

                List<elrcTotalesNumero> datosNumeros = elReporteCruce.elTotalesNumero;

                for (int i = 0; i < datosNumeros.Count; i++)
                {
                    Fila_Inicio++;
                    celd2[Fila_Inicio, 1].HorizontalAlignment = HAlign.Center;
                    celd2[Fila_Inicio, 1].Value = datosNumeros[i].FechaLlamada;
                    celd2[Fila_Inicio, 2].HorizontalAlignment = HAlign.Center;
                    celd2[Fila_Inicio, 2].Value = datosNumeros[i].Numero;
                    celd2[Fila_Inicio, 3].HorizontalAlignment = HAlign.Center;
                    celd2[Fila_Inicio, 3].Value = datosNumeros[i].Total;
                }

                IWorksheet hoja3 = Libro.Worksheets.Add();
                IRange celd3 = hoja3.Cells;
                Fila_Inicio = 2;

                celd3["A2:F2"].Merge();
                celd3["A2:F2"].Value = "REPORTE GENERAL DE TOTALES TITULO INTERACCIÓN";
                celd3["A2:F2"].Font.Size = 16;
                celd3["A2:F2"].Font.Bold = true;

                hoja3.Name = "Reporte_interacción";
                Fila_Inicio++;

                //////////////// INICIO DE CABECERA ////////////////
                num = 1;
                cabeceras = new string[] { "Fecha llamada", "Titulo interacción","Número","Total" };
                for (int i = 0; i < 4; i++)
                {
                    celd3[Fila_Inicio, num].Font.Bold = true;
                    celd3[Fila_Inicio, num].ColumnWidth = ((i == 1) ? 100 : 15);
                    celd3[Fila_Inicio, num].Interior.Color = Color.FromArgb(56, 96, 146);
                    celd3[Fila_Inicio, num].Font.Color = Color.FromArgb(255, 255, 255);
                    celd3[Fila_Inicio, num].Font.Name = "Arial";
                    celd3[Fila_Inicio, num].HorizontalAlignment = HAlign.Center;
                    celd3[Fila_Inicio, num].Font.Size = 10;
                    celd3[Fila_Inicio, num].Value = cabeceras[i];
                    num++;
                }

                List<elrcTituloInteraccion> datosInteraccion = elReporteCruce.elTituloInteraccion;
                for (int i = 0; i < datosInteraccion.Count; i++)
                {
                    Fila_Inicio++;
                    celd3[Fila_Inicio, 1].HorizontalAlignment = HAlign.Center;
                    celd3[Fila_Inicio, 1].Value = datosInteraccion[i].FechaLlamada;
                    celd3[Fila_Inicio, 1].HorizontalAlignment = HAlign.Left;
                    celd3[Fila_Inicio, 2].Value = datosInteraccion[i].TituloInteraccion;
                    celd3[Fila_Inicio, 1].HorizontalAlignment = HAlign.Center;
                    celd3[Fila_Inicio, 3].Value = datosInteraccion[i].Numero;
                    celd3[Fila_Inicio, 1].HorizontalAlignment = HAlign.Center;
                    celd3[Fila_Inicio, 4].Value = datosInteraccion[i].Total;
                }

                IWorksheet hoja4 = Libro.Worksheets.Add();
                IRange celd4 = hoja4.Cells;
                Fila_Inicio = 2;

                celd4["A2:F2"].Merge();
                celd4["A2:F2"].Value = "REPORTE GENERAL DE TOTALES AGENTE";
                celd4["A2:F2"].Font.Size = 16;
                celd4["A2:F2"].Font.Bold = true;

                hoja4.Name = "Reporte_Agente";
                Fila_Inicio++;
                num = 1;
                cabeceras = new string[] { "Fecha llamada", "Agente", "Número", "Total" };
                for (int i = 0; i < 4; i++)
                {
                    celd4[Fila_Inicio, num].Font.Bold = true;
                    celd4[Fila_Inicio, num].ColumnWidth = ((i == 1 || i==0) ? 20 : 15);
                    celd4[Fila_Inicio, num].Interior.Color = Color.FromArgb(56, 96, 146);
                    celd4[Fila_Inicio, num].Font.Color = Color.FromArgb(255, 255, 255);
                    celd4[Fila_Inicio, num].Font.Name = "Arial";
                    celd4[Fila_Inicio, num].HorizontalAlignment = HAlign.Center;
                    celd4[Fila_Inicio, num].Font.Size = 10;
                    celd4[Fila_Inicio, num].Value = cabeceras[i];
                    num++;
                }

                List<elrcTotalAgente> datosAgente = elReporteCruce.elTotalAgente;
                for (int i = 0; i < datosAgente.Count; i++)
                {
                    Fila_Inicio++;
                    celd4[Fila_Inicio, 1].HorizontalAlignment = HAlign.Center;
                    celd4[Fila_Inicio, 1].Value = datosAgente[i].FechaLlamada;
                    celd4[Fila_Inicio, 2].HorizontalAlignment = HAlign.Center;
                    celd4[Fila_Inicio, 2].Value = datosAgente[i].Agente;
                    celd4[Fila_Inicio, 3].HorizontalAlignment = HAlign.Center;
                    celd4[Fila_Inicio, 3].Value = datosAgente[i].Numero;
                    celd4[Fila_Inicio, 4].HorizontalAlignment = HAlign.Center;
                    celd4[Fila_Inicio, 4].Value = datosAgente[i].Total;
                }
                exclName = "Reporte_Con_Cruce_De_Datos" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ".xls";
                Libro.SaveAs(blGeneral.reportesPath + exclName,FileFormat.Excel8);
                result = "/Reportes/" + exclName;
            }

            return result;
        }
    }
}