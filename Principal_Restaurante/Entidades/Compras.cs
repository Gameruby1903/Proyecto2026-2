using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string? Imagen { get; set; }


        [NotMapped] public List<DetalleCompras>? Detalles { get; set; }
    }
}
