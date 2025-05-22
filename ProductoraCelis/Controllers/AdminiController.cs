using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductoraCelis.Data;
using ProductoraCelis.ViewsModes;
using ProductoraCelis.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

[Authorize(Roles = "Administrador")]
public class AdminController : Controller
{
    private readonly pgDbContext _dbContext;

    public AdminController(pgDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Vista del panel de control del administrador

    public IActionResult Dashboard()
    {
        var viewModel = new DashboardVM
        {
            TotalUsuarios = _dbContext.Usuario.Count(),
            TotalClientes = _dbContext.Clientes.Count(),
            TotalProveedores = _dbContext.Proveedor.Count(),
            TotalProductos = _dbContext.Productos.Count(),
            TotalVentas = _dbContext.Ventas.Count(),
            
           

            Clientes = _dbContext.Clientes.ToList(), 
            Proveedores = _dbContext.Proveedor.ToList(),
            Productos= _dbContext.Productos.ToList(),
            Usuarios = _dbContext.Usuario.ToList(),
            carrito = _dbContext.Carrito.ToList(),
            ventas = _dbContext.Ventas.ToList(),
            Comprobantes = _dbContext.Comprobantes.ToList(),



        };

        return View(viewModel);
        
    }
    // Método cliente
    [HttpGet]
    public async Task<IActionResult> Lista_Clientes()
    {
        List<ClientesMayoristas> listaClientes = await _dbContext.Clientes.ToListAsync();
        return View(listaClientes);
    }
    [HttpGet]
    public IActionResult Registrar_Cliente()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar_Cliente(ClientesMayoristas cliente)
    {
        await _dbContext.Clientes.AddAsync(cliente);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }
    [HttpGet]
    public async Task<IActionResult> Editar_Cliente(int id)
    {
        ClientesMayoristas clientes =await _dbContext.Clientes.FirstAsync(c=>c.IdClientes==id);
        return View(clientes);
    }
    [HttpPost]
    public async Task<IActionResult>Editar_Cliente(ClientesMayoristas cliente)
    {
        _dbContext.Clientes.Update(cliente); 
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }
    [HttpGet]
    public async Task<IActionResult>Eliminar_Clientes(int id)
    {
        ClientesMayoristas clientes=await _dbContext.Clientes.FirstAsync(c=>c.IdClientes==id);
        _dbContext.Clientes.Remove(clientes);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }


    // Método proveedor
    [HttpGet]
    public async Task<IActionResult> Lista_Proveedores()
    {
        List<Proveedores> listaProveedores = await _dbContext.Proveedor.ToListAsync();
        return View(listaProveedores);
    }
    [HttpGet]
    public IActionResult Registrar_Proveedor()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar_Proveedor(Proveedores proveedor)
    {
        await _dbContext.Proveedor.AddAsync(proveedor);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }
    [HttpGet]
    public async Task<IActionResult> Editar_Proveedor(int id)
    {
        Proveedores proveedor = await _dbContext.Proveedor.FirstAsync(p => p.IdProveedor == id);
        return View(proveedor);
    }
    [HttpPost]
    public async Task<IActionResult> Editar_Proveedor(Proveedores proveedor)
    {
        _dbContext.Proveedor.Update(proveedor); 
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }
    [HttpGet]
    public async Task<IActionResult> Eliminar_Proveedor(int id)
    {
        Proveedores proveedor= await _dbContext.Proveedor.FirstAsync(p => p.IdProveedor == id);
        _dbContext.Proveedor.Remove(proveedor);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }

    // Método producto
    [HttpGet]
    public async Task<IActionResult> Lista_Productos()
    {
        List<Productos> listaProductos = await _dbContext.Productos.ToListAsync();
        return View(listaProductos);
    }
    [HttpGet]
    public IActionResult Registrar_Producto()
    {
       return View();
    }

    [HttpPost]
    public async Task<IActionResult> Registrar_Producto(Productos producto, IFormFile Imagen)
    {
        if (Imagen != null && Imagen.Length > 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(Imagen.FileName);
            var extension = Path.GetExtension(Imagen.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            producto.UrlImagen = "/images/" + uniqueFileName;           
            await _dbContext.Productos.AddAsync(producto);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Dashboard));
            
        }
        return View(producto);
    }
    [HttpGet]
    public async Task<IActionResult> Editar_Producto(int id)
    {
        Productos producto =await _dbContext.Productos.FirstAsync(p =>p.IdProducto == id);
        return View(producto);
    }
    [HttpPost]
    public async Task<IActionResult> Editar_Producto(Productos producto, IFormFile Imagen)
    {
        if (Imagen != null && Imagen.Length > 0)
        {
            var fileName = Path.GetFileNameWithoutExtension(Imagen.FileName);
            var extension = Path.GetExtension(Imagen.FileName);
            var uniqueFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }
            var filePath = Path.Combine(uploadPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Imagen.CopyToAsync(stream);
            }
            producto.UrlImagen = "/images/" + uniqueFileName;
            _dbContext.Productos.Update(producto);
            await _dbContext.SaveChangesAsync();

        }
        return RedirectToAction(nameof(Dashboard));
    }
    [HttpGet]
    public async Task<IActionResult> Eliminar_Producto(int id)
    {
        Productos producto = await _dbContext.Productos.FirstAsync(p => p.IdProducto == id);
        _dbContext.Productos.Remove(producto);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction(nameof(Dashboard));
    }
    public IActionResult Ventas()
    {
        var ventas = _dbContext.Ventas
            .Select(v => new
            {
                v.IdVenta,
                v.FechaVenta,
                Cliente = _dbContext.Usuario.Find(v.IdUsuario),
                v.TotalVenta,
                Comprobante = _dbContext.Comprobantes.Find(v.IdVenta)
            }).ToList();

        return View(ventas);
    }

    public IActionResult DetalleVenta(int id)
    {
        //var detalles = _dbContext.DetallesVenta.Where(d => d.IdVenta == id).ToList();
        //return View(detalles);

        var detalles = _dbContext.DetallesVenta
       .Include(d => d.Producto) // asumiendo que tienes una relación con Producto
       .Where(d => d.IdVenta == id)
       .ToList();

        return View(detalles);
    }


    public IActionResult Comprobantes()
    {
        var comprobantes = _dbContext.Comprobantes.ToList();
        return View(comprobantes);
    }


}
