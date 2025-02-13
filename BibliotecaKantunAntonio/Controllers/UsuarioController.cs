using BibliotecaKantunAntonio.Models.Domain;
using BibliotecaKantunAntonio.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaKantunAntonio.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioServices _usuarioServices;

        public UsuarioController(IUsuarioServices usuarioServices)
        {
            _usuarioServices = usuarioServices;
        }
        public IActionResult Index()
        {
            var result = _usuarioServices.ObtenerUsuarios();
            return View(result);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(Usuario request)
        {
            _usuarioServices.CrearUsuario(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(int id)
        {
            var result = _usuarioServices.ObtenerUsuario(id);
            return View(result);
        }

    }
}
