namespace ProductoraCelis.ViewsModes
{
    public class DetalleCarritoVM
    {
        public int IdDetalleCarrito { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Cantidad * Precio;
    }
}
