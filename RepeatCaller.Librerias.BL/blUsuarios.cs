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

        public string listarUsuarios(int sedeId)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.listarUsuarios(sedeId, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_listarUsuarios", url, ex);
                }
            }
            return result;
        }

        public string actualizarUsuario(int userId, string username, string roles, string nombreCompleto)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.actualizarUsuario(userId, username, roles, nombreCompleto, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_actualizarUsuario", url, ex);
                }
            }
            return result;
        }

        public string guardarUsuario(string username, string roles, string nombreCompleto)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.guardarUsuario(username, roles, nombreCompleto, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_guardarUsuario", url, ex);
                }
            }
            return result;
        }

        public int actualizarEstado(int iduser, bool estado)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.actualizarEstado(iduser, estado, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_actualizarEstado", url, ex);
                }
            }
            return result;
        }

        public int resetearContrasenia(int userId)
        {
            int result = 0;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlUsuarios odlUser = new dlUsuarios();
                    result = odlUser.resetearContrasenia(userId, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.Url.ToString();
                    Log.Error(logPath, "blUsuarios_resetearContrasenia", url, ex);
                }
            }
            return result;
        }
    }
}
