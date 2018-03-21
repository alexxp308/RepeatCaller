using RepeatCaller.Librerias.BL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace RepeatCaller.API
{
    public class SubidaBaseController : ApiController
    {
        [HttpPost]
        public string Upload()
        {
            string path = HttpContext.Current.Server.MapPath("~/Doc/");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            var tipo = HttpContext.Current.Request.Params["tipo"];
            var campania = HttpContext.Current.Request.Params["campania"];
            var fecha = HttpContext.Current.Request.Params["fecha"];
            var userId = HttpContext.Current.Request.Params["userId"];
            HttpPostedFile fl = HttpContext.Current.Request.Files[0];
            string file, flName, ext, newName, result = "";
            string[] testfiles;
            if (HttpContext.Current.Request.Browser.Browser.ToUpper() == "IE" || HttpContext.Current.Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
            {
                testfiles = fl.FileName.Split(new char[] { '\\' });
                file = testfiles[testfiles.Length - 1];
            }
            else
            {
                file = fl.FileName;
            }

            flName = Path.GetFileName(path + "\\" + file);
            ext = Path.GetExtension(path + "\\" + file);
            string onlyName = (flName.Substring(0, flName.Length - 4) + "_" + DateTime.Now.ToString("yyyy_MM_ddTHH_mm_ss") + ext);
            newName = path + "\\" + onlyName;
            fl.SaveAs(newName);
            blBase oblBase = new blBase();
            int id = oblBase.guardarBase(Convert.ToInt32(userId),onlyName,Convert.ToString(tipo),Convert.ToInt32(campania),Convert.ToString(fecha));
            result = onlyName+"|"+id;

            return result;
        }
    }
}