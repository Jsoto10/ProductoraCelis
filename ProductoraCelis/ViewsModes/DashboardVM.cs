using ProductoraCelis.Models;

namespace ProductoraCelis.ViewsModes
{
    public class DashboardVM
    {
        public int TotalUsuarios { get; set; }
        public int TotalClientes { get; set; }
        public int TotalProveedores { get; set; }
        public int TotalProductos { get; set; }
        public int TotalVentas { get; set; }
        public int TotalCompras { get; set; }
        public List<ProductoraCelis.Models.ClientesMayoristas> Clientes { get; set; }
        public List<ProductoraCelis.Models.Proveedores>Proveedores { get; set; }
        public List<ProductoraCelis.Models.Productos>Productos { get; set; }
        public List<ProductoraCelis.Models.Usuario> Usuarios { get; set; }
        public List<ProductoraCelis.Models.Carrito> carrito { get; set; }
        public List<ProductoraCelis.Models.Ventas> ventas { get; set; }
        public List<ProductoraCelis.Models.Comprobante> Comprobantes { get; set; }

    }
}
