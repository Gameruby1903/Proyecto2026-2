using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_aplicaciones.Implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }
        public string? Usuario { get; set; }

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
        // ---------- AUDITORÍA ----------
        private static readonly HashSet<string> Ignoradas = new() { "UltimoAcceso" };
        private static readonly HashSet<string> Secretas = new() { "Clave" };

        private static string Texto(string campo, object? v) =>
            Secretas.Contains(campo) ? "***" : (v?.ToString() ?? "(vacío)");

        public override int SaveChanges()
        {
            var pendientes = new List<(EntityEntry Entry, string Entidad, int Id, string Accion, string Detalle)>();

            foreach (var entry in ChangeTracker.Entries().ToList())
            {
                if (entry.Entity is Auditorias) continue;

                var entidad = entry.Metadata.ClrType.Name;
                var id = (int)(entry.Property("Id").CurrentValue ?? 0);

                if (entry.State == EntityState.Added)
                {
                    var detalle = string.Join(" | ", entry.Properties
                        .Where(p => !p.Metadata.IsPrimaryKey() && !Ignoradas.Contains(p.Metadata.Name))
                        .Select(p => $"{p.Metadata.Name}: {Texto(p.Metadata.Name, p.CurrentValue)}"));
                    pendientes.Add((entry, entidad, id, "Insertar", detalle));
                }
                else if (entry.State == EntityState.Modified)
                {
                    var bd = entry.GetDatabaseValues();   // lo que hay en la BD = el "antes"
                    if (bd == null) continue;

                    var cambios = entry.Properties
                        .Where(p => !Ignoradas.Contains(p.Metadata.Name)
                                 && !Equals(bd[p.Metadata.Name], p.CurrentValue))
                        .Select(p => Secretas.Contains(p.Metadata.Name)
                            ? $"{p.Metadata.Name}: (cambiada)"
                            : $"{p.Metadata.Name}: {Texto(p.Metadata.Name, bd[p.Metadata.Name])} -> {Texto(p.Metadata.Name, p.CurrentValue)}")
                        .ToList();

                    if (cambios.Count == 0) continue;     // no cambió nada, no se audita
                    pendientes.Add((entry, entidad, id, "Actualizar", string.Join(" | ", cambios)));
                }
                else if (entry.State == EntityState.Deleted)
                {
                    var bd = entry.GetDatabaseValues();
                    var detalle = bd == null ? "" : string.Join(" | ", bd.Properties
                        .Where(p => !p.IsPrimaryKey())
                        .Select(p => $"{p.Name}: {Texto(p.Name, bd[p.Name])}"));
                    pendientes.Add((entry, entidad, id, "Borrar", detalle));
                }
            }

            if (pendientes.Count == 0) return base.SaveChanges();

            using var tx = Database.BeginTransaction();   // dato y auditoría juntos, o ninguno
            var filas = base.SaveChanges();

            foreach (var p in pendientes)
            {
                Auditorias!.Add(new Auditorias
                {
                    Usuario = Usuario ?? "desconocido",
                    Accion = p.Accion,
                    Entidad = p.Entidad,
                    IdRegistro = p.Accion == "Insertar"
                        ? (int)p.Entry.Property("Id").CurrentValue!   // el Id nuevo existe solo tras guardar
                        : p.Id,
                    Detalle = p.Detalle,
                    Fecha = DateTime.Now
                });
            }
            base.SaveChanges();
            tx.Commit();
            return filas;
        }
        // ---------- FIN AUDITORÍA ----------
        public DbSet<Auditorias>? Auditorias { get; set; }
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
