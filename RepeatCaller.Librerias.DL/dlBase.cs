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

        public int CargarTabla(DataTable dt,string tipo, int campaniaId, string fechaBase,int baseId,SqlConnection cn)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("USP_CARGAR_TABLA_"+tipo, cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@tabla", dt);
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            cmd.Parameters.AddWithValue("@baseId", baseId);
            result = (int)cmd.ExecuteScalar();
            return result;
        }

        public string backBaseAnterior(string tipo, int campaniaId, string fechaBase,int baseId, SqlConnection cn)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_REGRESAR_BASE_ANTERIOR", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@baseId", baseId);
            result = (string)cmd.ExecuteScalar();
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

        public string obtenerBases(int campaniaId, string tipo, string fechaBase, SqlConnection cn)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_OBTENER_BASES", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result += drd.GetInt32(0) + "|" + drd.GetString(1) + "|" + drd.GetString(2) + "|" + drd.GetString(3) + "|" + drd.GetString(4) + "|" + drd.GetString(5) + "|" + drd.GetString(6) + "|" + drd.GetBoolean(7) + "|" + drd.GetBoolean(8) + "£";
                }
                drd.Close();
            }
            return result.Substring(0, result.Length - 1);
        }

        public string verStatus(int campaniaId, string fechaBase, SqlConnection cn)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_OBTENER_STATUS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result += drd.GetString(0) + "|" + drd.GetString(1) + "|" + drd.GetString(2) + "£";
                }
                drd.Close();
            }
            return result.Substring(0, result.Length - 1);
        }

        public string basesFaltantes(int campaniaId, int tipo, string fechaBase,string fechaFinal, SqlConnection cn)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_BASES_FALTANTES", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            cmd.Parameters.AddWithValue("@fechaFinal", fechaFinal);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result += drd.GetString(0) + "|" + drd.GetString(1) + "#";
                }
                drd.Close();
            }
            return result.Substring(0, result.Length - 1);
        }
    }
}
