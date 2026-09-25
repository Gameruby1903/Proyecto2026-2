using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class DetalleCompras
    {
        public int Id { get; set; }

        public int Ingrediente { get; set; }
        [ForeignKey("Ingrediente")] public Ingredientes? _Ingrediente { get; set; }

        public int Compra { get; set; }
        [ForeignKey("Compra")] public Compras? _Compra { get; set; }

        public decimal Cantidad { get; set; }
        public decimal PrecioUni { get; set; }
        public decimal Subtotal { get; set; }
    }
}
