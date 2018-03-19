using RepeatCaller.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RepeatCaller.Librerias.BL;

namespace RepeatCaller.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public ActionResult Index()
        {
            return View("~/Views/Usuarios/Usuarios.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public string getUser(usuarioDTO usuario)
        {
            string result = "";
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.getUser(usuario.UserId);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Supervisor,Ejecutivo")]
        public int cambiarContrasenia(usuarioDTO usuario)
        {
            int result = 0;
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.cambiarContrasenia(usuario.UserId, usuario.password);
            return result;
        }
    }
}