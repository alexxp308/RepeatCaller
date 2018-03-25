#region usign
using General.Librerias.CodigoUsuario;
using RepeatCaller.Librerias.DL;
using RepeatCaller.Librerias.EL;
using System;
using System.Data.SqlClient;
using System.Web;
#endregion

namespace RepeatCaller.Librerias.BL
{
    public class blReporte:blGeneral
    {
        public elReporteCruce ReporteCruceDatos(int campaniaId, string fechaBase)
        {
            elReporteCruce oelReporteCruce = new elReporteCruce();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlReporte odlReporte = new dlReporte();
                    oelReporteCruce = odlReporte.ReporteCruceDatos(campaniaId, fechaBase, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.ToString();
                    Log.Error(logPath, "blReporte_ReporteCruceDatos", url, ex);
                }
            }
            return oelReporteCruce;
        }
    }
}
