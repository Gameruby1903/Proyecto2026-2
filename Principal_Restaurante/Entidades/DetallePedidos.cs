using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class DetallePedidos
    {
        public int Id { get; set; }

        public int Pedido { get; set; }
        [ForeignKey("Pedido")] public Pedidos? _Pedido { get; set; }

        public int Producto { get; set; }
        [ForeignKey("Producto")] public Productos? _Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUni { get; set; }
        public decimal Subtotal { get; set; }
    }
}
