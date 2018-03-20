using System.Data;
using System.Data.SqlClient;

namespace RepeatCaller.Librerias.DL
{
    public class dlUsuarios
    {
        public string getUser(int iduser, SqlConnection con)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_OBTENER_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@iduser", iduser);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleRow);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result = drd.GetString(0) + "|" + drd.GetString(1) + "|" + drd.GetBoolean(2);
                }
                drd.Close();
            }
            return result;
        }

        public int cambiarContrasenia(int iduser, string password, SqlConnection con)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("USP_CAMBIAR_CONTRASENIA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@iduser", iduser);
            cmd.Parameters.AddWithValue("@password", password);
            result = cmd.ExecuteNonQuery();
            return result;
        }

        public string listarUsuarios(int sedeId, SqlConnection con)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_LISTAR_USUARIOS", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@sedeId", sedeId);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleResult);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result += drd.GetInt32(0) + "|" + drd.GetString(1) + "|" + drd.GetString(2) + "|" + drd.GetString(3) + "|" + drd.GetBoolean(4) + "|" + drd.GetBoolean(5) + "#";
                }
                drd.Close();
            }
            return result.Substring(0, result.Length - 1);
        }

        public string guardarUsuario(string username, string roles, string nombreCompleto, SqlConnection con)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_CREAR_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@roles", roles);
            cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleRow);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result = drd.GetInt32(0) + "|" + drd.GetString(1) + "|" + drd.GetString(2) + "|" + drd.GetString(3) + "|" + drd.GetBoolean(4) + "|" + drd.GetBoolean(5);
                }
                drd.Close();
            }
            return result;
        }

        public string actualizarUsuario(int userId, string username, string roles, string nombreCompleto, SqlConnection con)
        {
            string result = "";
            SqlCommand cmd = new SqlCommand("USP_ACTUALIZAR_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@roles", roles);
            cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
            SqlDataReader drd = cmd.ExecuteReader(CommandBehavior.SingleRow);
            if (drd != null)
            {
                while (drd.Read())
                {
                    result = drd.GetInt32(0) + "|" + drd.GetString(1) + "|" + drd.GetString(2) + "|" + drd.GetString(3) + "|" + drd.GetBoolean(4) + "|" + drd.GetBoolean(5);
                }
                drd.Close();
            }
            return result;
        }

        public int actualizarEstado(int iduser, bool estado, SqlConnection con)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("USP_ACTUALIZAR_ESTADO_USUARIO", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@iduser", iduser);
            cmd.Parameters.AddWithValue("@estado", estado);
            result = cmd.ExecuteNonQuery();
            return result;
        }

        public int resetearContrasenia(int iduser, SqlConnection con)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("USP_RESETEAR_CONTRASENIA", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@iduser", iduser);
            result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
