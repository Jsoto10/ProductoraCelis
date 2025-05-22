using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductoraCelis.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ProductoraCelis.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using ProductoraCelis.ViewsModes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Newtonsoft.Json;

namespace ProductoraCelis.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly pgDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, pgDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Acceso");
        }
        public async Task<IActionResult> Productos()
        {
            List<Productos> listaProductos = _dbContext.Productos.ToList();
            return View(listaProductos);

        }


        [HttpPost]
        public IActionResult AgregarAlCarritoMultiple([FromBody] List<DetallesCarrito> productos)
        {
            if (productos == null || productos.Count == 0)
                return BadRequest();

            // Aquí debes obtener el carrito actual del usuario
            // Por simplicidad, supongamos que tienes una función que retorna el carrito
            var carrito = ObtenerCarritoDelUsuario(); // Implementa esta función según tu lógica

            foreach (var p in productos)
            {
                var productoDb = _dbContext.Productos.FirstOrDefault(x => x.Nombre == p.Nombre);
                if (productoDb == null) continue;

                var detalleExistente = carrito.DetallesCarrito.FirstOrDefault(d => d.IdProducto == productoDb.IdProducto);
                if (detalleExistente != null)
                {
                    detalleExistente.Cantidad += p.Cantidad;
                    _dbContext.Update(detalleExistente);
                }
                else
                {
                    var detalle = new DetallesCarrito
                    {
                        IdProducto = productoDb.IdProducto,
                        Cantidad = p.Cantidad,
                        PrecioUnitario = productoDb.Precio,
                        CarritoId = carrito.IdCarrito // Si usas FK para relacionar
                    };
                    _dbContext.DetallesCarrito.Add(detalle);
                }
            }

            _dbContext.SaveChanges();

            return Ok();
        }
        private Carrito ObtenerCarritoDelUsuario()
        {
            var userName = User.Identity.Name;

            // Buscar el usuario para obtener su IdUsuario numérico
            var usuario = _dbContext.Usuario.FirstOrDefault(u => u.Nombres == userName);
            if (usuario == null)
                throw new Exception("Usuario no encontrado");

            var userId = usuario.Id; // suponiendo que es int

            var carrito = _dbContext.Carrito
                .Include(c => c.DetallesCarrito)
                .FirstOrDefault(c => c.IdUsuario == userId);

            if (carrito == null)
            {
                carrito = new Carrito
                {
                    IdUsuario = userId,
                    DetallesCarrito = new List<DetallesCarrito>()
                };
                _dbContext.Carrito.Add(carrito);
                _dbContext.SaveChanges();
            }

            return carrito;
        }

    }
}
