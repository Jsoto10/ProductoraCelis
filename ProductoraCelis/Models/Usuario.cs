namespace ProductoraCelis.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Dni { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Password{ get; set; }
        public string Rol { get; set; }

        public ICollection<Carrito> Carritos { get; set; }
    }
}
