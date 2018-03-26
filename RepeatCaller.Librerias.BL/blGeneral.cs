using System.Configuration;

namespace RepeatCaller.Librerias.BL
{
    public class blGeneral
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static readonly string logPath = ConfigurationManager.AppSettings["config:log"];
        public static readonly string reportesPath = ConfigurationManager.AppSettings["config:reportes"];
    }
}
