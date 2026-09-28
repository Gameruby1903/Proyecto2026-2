using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
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
    }
}
