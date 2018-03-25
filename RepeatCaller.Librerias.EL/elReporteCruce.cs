#region using
using System.Collections.Generic;
#endregion
namespace RepeatCaller.Librerias.EL
{
    public class elReporteCruce
    {
        public List<elrcDetalle> elDetalle { get; set; }
        public List<elrcTotalesNumero> elTotalesNumero { get; set; }
        public List<elrcTituloInteraccion> elTituloInteraccion { get; set; }
        public List<elrcTotalAgente> elTotalAgente { get; set; }
    }
}
