using lib_aplicaciones.Entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<TipoEmpleados>? TipoEmpleados { get; set; }
        DbSet<Sucursales>? Sucursales { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Clientes>? Clientes { get; set; }
        DbSet<Categorias>? Categorias { get; set; }
        DbSet<Productos>? Productos { get; set; }
        DbSet<Mesas>? Mesas { get; set; }
        DbSet<Pedidos>? Pedidos { get; set; }
        DbSet<DetallePedidos>? DetallePedidos { get; set; }
        DbSet<MetodoPagos>? MetodoPagos { get; set; }
        DbSet<Facturas>? Facturas { get; set; }
        DbSet<Reservas>? Reservas { get; set; }
        DbSet<Proveedores>? Proveedores { get; set; }
        DbSet<Ingredientes>? Ingredientes { get; set; }
        DbSet<RecetaIngredientes>? RecetaIngredientes { get; set; }
        DbSet<Inventarios>? Inventarios { get; set; }
        DbSet<Compras>? Compras { get; set; }
        DbSet<DetalleCompras>? DetalleCompras { get; set; }
        DbSet<Turnos>? Turnos { get; set; }
        DbSet<Resenas>? Resenas { get; set; }
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}

