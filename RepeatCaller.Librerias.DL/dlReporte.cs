#region using
using RepeatCaller.Librerias.EL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
#endregion

namespace RepeatCaller.Librerias.DL
{
    public class dlReporte
    {
        public elReporteCruce ReporteCruceDatos(int campaniaId, string fechaBase, SqlConnection cn)
        {
            elReporteCruce oelReporteCruce = new elReporteCruce();
            List<elrcDetalle> lelrcDetalle = null;
            List<elrcTituloInteraccion> lelrcTituloInteraccion = null;
            List<elrcTotalAgente> lelrcTotalAgente = null;
            List<elrcTotalesNumero> lelrcTotalesNumero = null;
            SqlCommand cmd = new SqlCommand("USP_REPORTE_CRUCE_DATOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            SqlDataReader drd = cmd.ExecuteReader();
            if (drd != null)
            {
                elrcDetalle oelrcDetalle;
                lelrcDetalle = new List<elrcDetalle>();
                while (drd.Read())
                {
                    oelrcDetalle = new elrcDetalle();
                    oelrcDetalle.Agente = drd.GetString(0);
                    oelrcDetalle.FechaLlamada = drd.GetString(1);
                    oelrcDetalle.Numero = drd.GetString(2);
                    oelrcDetalle.TituloInteraccion = drd.GetString(3);
                    lelrcDetalle.Add(oelrcDetalle);
                }
                oelReporteCruce.elDetalle = lelrcDetalle;
                if (drd.NextResult())
                {
                    elrcTotalesNumero oelrcTotalesNumero;
                    lelrcTotalesNumero = new List<elrcTotalesNumero>();
                    while (drd.Read())
                    {
                        oelrcTotalesNumero = new elrcTotalesNumero();
                        oelrcTotalesNumero.FechaLlamada = drd.GetString(0);
                        oelrcTotalesNumero.Numero = drd.GetString(1);
                        oelrcTotalesNumero.Total = drd.GetInt32(2);
                        lelrcTotalesNumero.Add(oelrcTotalesNumero);
                    }
                    oelReporteCruce.elTotalesNumero = lelrcTotalesNumero;
                }
                if (drd.NextResult())
                {
                    elrcTituloInteraccion oelrcTituloInteraccion;
                    lelrcTituloInteraccion = new List<elrcTituloInteraccion>();
                    while (drd.Read())
                    {
                        oelrcTituloInteraccion = new elrcTituloInteraccion();
                        oelrcTituloInteraccion.TituloInteraccion = drd.GetString(0);
                        oelrcTituloInteraccion.FechaLlamada = drd.GetString(1);
                        oelrcTituloInteraccion.Numero = drd.GetString(2);
                        oelrcTituloInteraccion.Total = drd.GetInt32(3);
                        lelrcTituloInteraccion.Add(oelrcTituloInteraccion);
                    }
                    oelReporteCruce.elTituloInteraccion = lelrcTituloInteraccion;
                }
                if (drd.NextResult())
                {
                    elrcTotalAgente oelrcTotalAgente;
                    lelrcTotalAgente = new List<elrcTotalAgente>();
                    while (drd.Read())
                    {
                        oelrcTotalAgente = new elrcTotalAgente();
                        oelrcTotalAgente.Agente = drd.GetString(0);
                        oelrcTotalAgente.FechaLlamada = drd.GetString(1);
                        oelrcTotalAgente.Numero = drd.GetString(2);
                        oelrcTotalAgente.Total = drd.GetInt32(3);
                        lelrcTotalAgente.Add(oelrcTotalAgente);
                    }
                    oelReporteCruce.elTotalAgente = lelrcTotalAgente;
                }
                drd.Close();
            }

            return oelReporteCruce;
        }

        public elReporteSinCruce ReporteSinCruceDatos(int campaniaId, string fechaBase, SqlConnection cn)
        {
            elReporteSinCruce oelReporteSinCruce = new elReporteSinCruce();
            List<elrcTituloInteraccion> lelrcTituloInteraccion = null;
            List<elrcTotalAgente> lelrcTotalAgente = null;
            SqlCommand cmd = new SqlCommand("USP_REPORTE_SIN_CRUCE_DATOS", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 1800;
            cmd.Parameters.AddWithValue("@campaniaId", campaniaId);
            cmd.Parameters.AddWithValue("@fechaBase", fechaBase);
            SqlDataReader drd = cmd.ExecuteReader();
            if (drd != null)
            {
                elrcTituloInteraccion oelrcTituloInteraccion;
                lelrcTituloInteraccion = new List<elrcTituloInteraccion>();
                while (drd.Read())
                {
                    oelrcTituloInteraccion = new elrcTituloInteraccion();
                    oelrcTituloInteraccion.FechaLlamada = drd.GetString(0);
                    oelrcTituloInteraccion.TituloInteraccion = drd.GetString(1);
                    oelrcTituloInteraccion.Numero = drd.GetString(2);
                    oelrcTituloInteraccion.Total = drd.GetInt32(3);
                    lelrcTituloInteraccion.Add(oelrcTituloInteraccion);
                }
                oelReporteSinCruce.elTituloInteraccion = lelrcTituloInteraccion;
                if (drd.NextResult())
                {
                    elrcTotalAgente oelrcTotalAgente;
                    lelrcTotalAgente = new List<elrcTotalAgente>();
                    while (drd.Read())
                    {
                        oelrcTotalAgente = new elrcTotalAgente();
                        oelrcTotalAgente.Agente = drd.GetString(0);
                        oelrcTotalAgente.FechaLlamada = drd.GetString(1);
                        oelrcTotalAgente.Numero = drd.GetString(2);
                        oelrcTotalAgente.Total = drd.GetInt32(3);
                        lelrcTotalAgente.Add(oelrcTotalAgente);
                    }
                    oelReporteSinCruce.elTotalAgente = lelrcTotalAgente;
                }

                drd.Close();
            }

            return oelReporteSinCruce;
        }
    }
}
