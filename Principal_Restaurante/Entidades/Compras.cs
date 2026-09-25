using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Compras
    {
        public int Id { get; set; }

        public int Proveedor { get; set; }
        [ForeignKey("Proveedor")] public Proveedores? _Proveedor { get; set; }

        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public string? Estado { get; set; }

        public List<DetalleCompras> Detalles { get; set; } = new List<DetalleCompras>();
    }
}
