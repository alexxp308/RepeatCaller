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
        public int guardarBase(int userId,string archivo,string tipo,int campaniaId, SqlConnection con)
        {
            int result = 0;
            SqlCommand cmd = new SqlCommand("USP_GUARDAR_BASE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@archivo", archivo);
            cmd.Parameters.AddWithValue("@tipo", tipo);
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            result = cmd.ExecuteNonQuery();
            return result;
        }
    }
}
