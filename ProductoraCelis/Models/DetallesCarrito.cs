namespace ProductoraCelis.Models
{
    public class DetallesCarrito
    {
        public int IdDetalleCarrito { get; set; }
        public int IdProducto { get; set; }
        public int CarritoId { get; set; }
        public Productos Producto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public Decimal PrecioUnitario { get; set; }
        public decimal Subtotal => Cantidad * PrecioUnitario;
    }
}
