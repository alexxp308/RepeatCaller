using General.Librerias.CodigoUsuario;
using RepeatCaller.Librerias.DL;
using System;
using System.Data.SqlClient;
using System.Web;

namespace RepeatCaller.Librerias.BL
{
    public class blBase:blGeneral
    {
        public int guardarBase(int userId, string archivo, string tipo, int campaniaId)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlBase odlBase = new dlBase();
                    result = odlBase.guardarBase(userId, archivo, tipo, campaniaId, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.ToString();
                    Log.Error(logPath, "blBase_guardarBase", url, ex);
                }
            }
            return result;
        }
    }
}
