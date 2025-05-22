namespace ProductoraCelis.Models
{
    public class Comprobante
    {
        public int IdComprobante { get; set; }
        public string TipoComprobante { get; set; }  // Ejemplo: "Boleta", "Factura"
        public string Serie { get; set; }  // Ejemplo: "F001"
        public string Numero { get; set; }  // Ejemplo: "000123"
        public DateTime FechaEmision { get; set; }
        public decimal Total { get; set; }

        // Relación con Ventas: una Comprobante tiene muchas Ventas
        public ICollection<Ventas> Ventas { get; set; }
    }
}
