using General.Librerias.CodigoUsuario;
using RepeatCaller.Librerias.DL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace RepeatCaller.Librerias.BL
{
    public class blBase : blGeneral
    {
        public int guardarBase(int userId, string archivo, string tipo, int campaniaId, string fechaBase)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlBase odlBase = new dlBase();
                    result = odlBase.guardarBase(userId, archivo, tipo, campaniaId, fechaBase, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.ToString();
                    Log.Error(logPath, "blBase_guardarBase", url, ex);
                }
            }
            return result;
        }

        public int CargarTabla(DataTable tabla,string tipo)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlBase odlBase = new dlBase();
                    result = odlBase.CargarTabla(tabla, tipo, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.ToString();
                    Log.Error(logPath, "blBase_CargarTabla", url, ex);
                }
            }
            return result;
        }

        public string getBaseCargada(int campaniaId, string tipo,string fechaBase)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlBase odlBase = new dlBase();
                    result = odlBase.getBaseCargada(campaniaId, tipo, fechaBase, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.ToString();
                    Log.Error(logPath, "blBase_getBaseCargada", url, ex);
                }
            }
            return result;
        }
    }
}
