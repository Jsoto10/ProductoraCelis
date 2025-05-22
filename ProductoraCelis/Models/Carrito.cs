namespace ProductoraCelis.Models
{
    public class Carrito
    {
        public int IdCarrito { get; set; }
        public int IdUsuario { get; set; } 
        public int IdProducto { get; set; } 
        public int Cantidad { get; set; }
       public Usuario Usuario { get; set; }
        public Productos Producto { get; set; }
        public ICollection<DetallesCarrito> DetallesCarrito { get; set; }
    }
}
