using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepeatCaller.Librerias.DL
{
    public class dlBase
    {
        public int guardarBase(int userId,string archivo,string tipo,int campaniaId,string fechaBase, SqlConnection con)
        {
            int id = 0;
            SqlCommand cmd = new SqlCommand("USP_GUARDAR_BASE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@archivo", archivo);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            id = (int)cmd.ExecuteScalar();
            return id;
        }

        public int CargarTabla(DataTable dt,string tipo,SqlConnection cn)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("USP_CARGAR_TABLA_"+tipo, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@tabla", dt);
            result = cmd.ExecuteNonQuery();
            return result;
        }

        public string getBaseCargada(int campaniaId, string tipo, string fechaBase, SqlConnection cn)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_GET_BASE_CARGADA", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            result = (string)cmd.ExecuteScalar();
            return result;
        }
    }
}
