﻿using General.Librerias.CodigoUsuario;
using System;
using System.Data.SqlClient;
using System.Web;
using RepeatCaller.Librerias.DL;

namespace RepeatCaller.Librerias.BL
{
    public class blLogin:blGeneral
    {
        public string checkLogin(string userName, string password)
        {
            string result = "";
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    con.Open();
                    dlLogin odlLogin = new dlLogin();
                    result = odlLogin.checkLogin(userName, password, con);
                }
                catch (Exception ex)
                {
                    string url = HttpContext.Current.Request.UrlReferrer.ToString();
                    Log.Error(logPath, "blLogin_checkLogin", url, ex);
                }
            }

            return result;
        }
    }
}
