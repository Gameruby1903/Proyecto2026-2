using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatosGenerales.StringConexion(), p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuarios>()
                .HasOne(x => x._Rol)
                .WithMany()
                .HasForeignKey(x => x.Rol);

            modelBuilder.Entity<Usuarios>()
                .HasOne(x => x._Empleado)
                .WithMany()
                .HasForeignKey(x => x.Empleado);

            modelBuilder.Entity<Usuarios>()
                .HasOne(x => x._Cliente)
                .WithMany()
                .HasForeignKey(x => x.Cliente);

            modelBuilder.Entity<RolPermisos>()
                .HasOne(x => x._Rol)
                .WithMany()
                .HasForeignKey(x => x.Rol);

            modelBuilder.Entity<RolPermisos>()
                .HasOne(x => x._Permiso)
                .WithMany()
                .HasForeignKey(x => x.Permiso);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<TipoEmpleados>? TipoEmpleados { get; set; }
        public DbSet<Sucursales>? Sucursales { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Categorias>? Categorias { get; set; }
        public DbSet<Productos>? Productos { get; set; }
        public DbSet<Mesas>? Mesas { get; set; }
        public DbSet<Pedidos>? Pedidos { get; set; }
        public DbSet<DetallePedidos>? DetallePedidos { get; set; }
        public DbSet<MetodoPagos>? MetodoPagos { get; set; }
        public DbSet<Facturas>? Facturas { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Proveedores>? Proveedores { get; set; }
        public DbSet<Ingredientes>? Ingredientes { get; set; }
        public DbSet<RecetaIngredientes>? RecetaIngredientes { get; set; }
        public DbSet<Inventarios>? Inventarios { get; set; }
        public DbSet<Compras>? Compras { get; set; }
        public DbSet<DetalleCompras>? DetalleCompras { get; set; }
        public DbSet<Turnos>? Turnos { get; set; }
        public DbSet<Resenas>? Resenas { get; set; }
        public DbSet<Usuarios>? Usuarios { get; set; }
        public DbSet<Permisos>? Permisos { get; set; }
        public DbSet<Roles>? Roles { get; set; }
        public DbSet<RolPermisos>? RolPermisos { get; set; }
    }
}
