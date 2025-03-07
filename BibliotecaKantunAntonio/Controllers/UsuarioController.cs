using BibliotecaKantunAntonio.Models.Domain;
using BibliotecaKantunAntonio.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Runtime.CompilerServices;

namespace BibliotecaKantunAntonio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;
        private readonly IRolServices _rolServices;
        

        public UsuarioController(IUsuarioServices usuarioServices, IRolServices rolServices)
        {
            _usuarioServices = usuarioServices;
            _rolServices = rolServices;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _usuarioServices.ObtenerUsuarios();
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            List<Rol> result = await _rolServices.GetAll();
            ViewBag.Roles = result.Select(p => new SelectListItem()
            {
                Text = p.Nombre,
                Value = p.PkRol.ToString()
            });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Usuario request)
        {
            await _usuarioServices.CrearUsuario(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var usuario = await _usuarioServices.ObtenerUsuario(id);
            if (usuario == null)
                return NotFound();

            var roles = await _rolServices.GetAll();
            ViewBag.Roles = roles.Select(r => new SelectListItem
            {
                Text = r.Nombre,
                Value = r.PkRol.ToString(),
                Selected = (r.PkRol == usuario.FkRol)
            }).ToList();

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Usuario usuario)
        {
            // Removemos la validación del Password cuando es una edición
            if (string.IsNullOrEmpty(usuario.Password))
            {
                ModelState.Remove("Password");
            }

            if (!ModelState.IsValid)
            {
                var roles = await _rolServices.GetAll();
                ViewBag.Roles = roles.Select(r => new SelectListItem
                {
                    Text = r.Nombre,
                    Value = r.PkRol.ToString()
                }).ToList();
                return View(usuario);
            }

            var actualizado = await _usuarioServices.ActualizarUsuario(usuario);
            if (actualizado)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "No se pudo actualizar el usuario");
            var rolesParaVista = await _rolServices.GetAll();
            ViewBag.Roles = rolesParaVista.Select(r => new SelectListItem
            {
                Text = r.Nombre,
                Value = r.PkRol.ToString()
            }).ToList();
            return View(usuario);
        }


        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var usuario = await _usuarioServices.ObtenerUsuario(id);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            bool eliminado = await _usuarioServices.EliminarUsuario(id);
            if (eliminado)
                return RedirectToAction("Index");

            ModelState.AddModelError("", "No se pudo eliminar el usuario.");
            var usuario = await _usuarioServices.ObtenerUsuario(id);
            return View(usuario);
        }

        [HttpDelete]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            bool result = await _usuarioServices.EliminarUsuario(id);
            return Json(new { succes = result });
        }

    }
}
