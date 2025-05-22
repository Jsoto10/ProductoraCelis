using Microsoft.EntityFrameworkCore;
using ProductoraCelis.Models;

namespace ProductoraCelis.Data
{
    public class pgDbContext : DbContext
    {
        public pgDbContext(DbContextOptions<pgDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<ClientesMayoristas> Clientes { get; set; }
        public DbSet<Proveedores> Proveedor { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Carrito> Carrito { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }
        public DbSet<Ventas> Ventas { get; set; }
        public DbSet<DetalleVenta> DetallesVenta { get; set; }
        public DbSet<DetallesCarrito> DetallesCarrito { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //usuarios
            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(col => col.Id);
                tb.Property(col => col.Id).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombres).HasMaxLength(50);
                tb.Property(col => col.Apellidos).HasMaxLength(50);
                tb.Property(col => col.Dni).HasMaxLength(8);
                tb.Property(col => col.Celular).HasMaxLength(9);
                tb.Property(col => col.Email).HasMaxLength(50);
                tb.Property(col => col.Password).HasMaxLength(50);
                tb.Property(col => col.Rol).HasMaxLength(20);
            });
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");

            // clientes
            modelBuilder.Entity<ClientesMayoristas>(tb =>
            {
                tb.HasKey(col => col.IdClientes);
                tb.Property(col => col.IdClientes).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombres).HasMaxLength(50);
                tb.Property(col => col.Apellidos).HasMaxLength(50);
                tb.Property(col => col.Dni).HasMaxLength(8);
                tb.Property(col => col.Celular).HasMaxLength(9);
                tb.Property(col => col.Direccion).HasMaxLength(50);
                tb.Property(col => col.FechaRegistro).HasColumnType("datetime");
                tb.Property(col => col.Descripcion).HasMaxLength(100);
                tb.Property(col => col.Email).HasMaxLength(50);

            });
            modelBuilder.Entity<ClientesMayoristas>().ToTable("Clientes_Mayoristas");


            //proveedores
            modelBuilder.Entity<Proveedores>(tb =>
            {
                tb.HasKey(col => col.IdProveedor);
                tb.Property(col => col.IdProveedor).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombres).HasMaxLength(50);
                tb.Property(col => col.Apellidos).HasMaxLength(50);
                tb.Property(col => col.Dni).HasMaxLength(9);
                tb.Property(col => col.Celular).HasMaxLength(9);
                tb.Property(col => col.Direccion).HasMaxLength(50);
                tb.Property(col => col.FechaRegistro).HasColumnType("datetime");
                tb.Property(col => col.Descripcion).HasMaxLength(100);
                tb.Property(col => col.Tipo).HasMaxLength(100);
                tb.Property(col => col.Email).HasMaxLength(100);
            });
            modelBuilder.Entity<Proveedores>().ToTable("Proveedor");

            // tabla para productos
            modelBuilder.Entity<Productos>(tb =>
            {
                tb.HasKey(col => col.IdProducto);
                tb.Property(col => col.IdProducto).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(col => col.Nombre).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Categoria).IsRequired().HasMaxLength(50);
                tb.Property(col => col.Descripcion).IsRequired().HasMaxLength(100);
                tb.Property(col => col.Stock).IsRequired();
                tb.Property(col => col.FechaProduccion).HasColumnType("datetime");
                tb.Property(col => col.FechaVencimiento).HasColumnType("datetime");
                tb.Property(col => col.Precio).IsRequired().HasPrecision(18, 2);
                //tb.Property(col => col.Estado);
                tb.Property(col => col.UrlImagen).HasMaxLength(200);
            });
            modelBuilder.Entity<Productos>().ToTable("Producto");


            modelBuilder.Entity<Carrito>(tb =>
            {
                tb.HasKey(c => c.IdCarrito);
                tb.Property(c => c.IdCarrito).UseIdentityColumn().ValueGeneratedOnAdd();

                tb.HasOne(c => c.Usuario)
                  .WithMany(u => u.Carritos)
                  .HasForeignKey(c => c.IdUsuario)
                  .OnDelete(DeleteBehavior.Cascade);

                tb.HasOne(c => c.Producto)
                  .WithMany(p => p.Carritos)
                  .HasForeignKey(c => c.IdProducto)
                  .OnDelete(DeleteBehavior.Cascade);

                tb.Property(c => c.Cantidad).IsRequired();

                tb.ToTable("Carrito");
            });
            // Comprobante
            modelBuilder.Entity<Comprobante>(tb =>
            {
                tb.HasKey(c => c.IdComprobante);
                tb.Property(c => c.IdComprobante).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(c => c.TipoComprobante).HasMaxLength(20).IsRequired();
                tb.Property(c => c.Serie).HasMaxLength(10).IsRequired();
                tb.Property(c => c.Numero).HasMaxLength(20).IsRequired();
                tb.Property(c => c.FechaEmision).HasColumnType("datetime");
                tb.Property(c => c.Total).HasPrecision(18, 2).IsRequired();

                tb.ToTable("Comprobantes");
            });

            // Venta
            modelBuilder.Entity<Ventas>(tb =>
            {
                tb.HasKey(v => v.IdVenta);
                tb.Property(v => v.IdVenta).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(v => v.FechaVenta).HasColumnType("datetime").IsRequired();
                tb.Property(v => v.TotalVenta).HasPrecision(18, 2).IsRequired();

                

                tb.HasOne(v => v.Comprobante)
                  .WithMany(c => c.Ventas)
                 // .HasForeignKey(v => v.IdComprobante)
                  .OnDelete(DeleteBehavior.Restrict);

                tb.ToTable("Ventas");
            });

            // DetallesVenta
            modelBuilder.Entity<DetalleVenta>(tb =>
            {
                tb.HasKey(dv => dv.IdDetalleVenta);
                tb.Property(dv => dv.IdDetalleVenta).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(dv => dv.Cantidad).IsRequired();
                tb.Property(dv => dv.PrecioUnitario).HasPrecision(18, 2).IsRequired();

                tb.HasOne(dv => dv.Venta)
                  .WithMany(v => v.DetallesVenta)
                  .HasForeignKey(dv => dv.IdVenta)
                  .OnDelete(DeleteBehavior.Cascade);

                tb.HasOne(dv => dv.Producto)
                  .WithMany()
                  .HasForeignKey(dv => dv.IdProducto)
                  .OnDelete(DeleteBehavior.Restrict);

                tb.ToTable("DetallesVenta");
            });

            // DetallesCarrito
            modelBuilder.Entity<DetallesCarrito>(tb =>
            {
                tb.HasKey(dc => dc.IdDetalleCarrito);
                tb.Property(dc => dc.IdDetalleCarrito).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(dc => dc.Cantidad).IsRequired();
                tb.Property(dc => dc.PrecioUnitario).HasPrecision(18, 2).IsRequired();

                tb.HasOne(dc => dc.Producto)
                  .WithMany()
                  .HasForeignKey(dc => dc.IdProducto)
                  .OnDelete(DeleteBehavior.Restrict);

                tb.ToTable("DetallesCarrito");
            });




        }
    }
}
