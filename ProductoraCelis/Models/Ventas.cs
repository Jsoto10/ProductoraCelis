namespace ProductoraCelis.Models
{
    public class Ventas
    {
        public int IdVenta { get; set; }
        public string IdUsuario { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public string TipoComprobante { get; set; }
        public decimal TotalVenta { get; set; }

        public Comprobante Comprobante { get; set; }
        public ICollection<DetalleVenta> DetallesVenta { get; set; }
    }
}
