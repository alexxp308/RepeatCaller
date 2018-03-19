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
                    result = drd.GetString(0) + "|" + drd.GetString(1)+"|"+drd.GetBoolean(2);
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
    }
}
