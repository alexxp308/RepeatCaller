#region using
using General.Librerias.CodigoUsuario;
using RepeatCaller.Librerias.DL;
using System;
using System.Data.SqlClient;
using System.Web;
#endregion

namespace RepeatCaller.Librerias.BL
{
    public class blUsuarios:blGeneral
    {
        public string getUser(int userId)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.getUser(userId, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_getUser", url, ex);
                }
            }
            return result;
        }

        public int cambiarContrasenia(int userId, string password)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.cambiarContrasenia(userId, password, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_cambiarContrasenia", url, ex);
                }
            }
            return result;
        }
    }
}
