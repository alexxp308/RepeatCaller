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

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public string listarUsuarios(usuarioDTO usuario)
        {
            string result = "";
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.listarUsuarios(usuario.idSede);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public string actualizarUsuario(usuarioDTO usuario)
        {
            string result = "";
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.actualizarUsuario(usuario.UserId, usuario.userName, usuario.roles, usuario.nombreCompleto);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public string guardarUsuario(usuarioDTO usuario)
        {
            string result = "";
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.guardarUsuario(usuario.userName, usuario.roles, usuario.nombreCompleto);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public int resetearContrasenia(usuarioDTO usuario)
        {
            int result = 0;
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.resetearContrasenia(usuario.UserId);
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "Ejecutivo")]
        public int actualizarEstado(usuarioDTO usuario)
        {
            int result = 0;
            blUsuarios oblUser = new blUsuarios();
            result = oblUser.actualizarEstado(usuario.UserId, usuario.IsActive);
            return result;
        }
    }
}