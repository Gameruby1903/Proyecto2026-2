using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Facturas
    {
        public int Id { get; set; }

        public int Pedido { get; set; }
        public Pedidos? _Pedido { get; set; }

        public DateTime FechaEmi { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }

        public int MetodoPago { get; set; }
        [ForeignKey("MetodoPago")] public MetodoPagos? _MetodoPago { get; set; }
    }

}
