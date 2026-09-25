using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Pedidos
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }

        public int Cliente { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        public int Mesa { get; set; }
        [ForeignKey("Mesa")] public Mesas? _Mesa { get; set; }

        public int Empleado { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
        public string? Estado { get; set; }

        public List<DetallePedidos> Detalles { get; set; } = new List<DetallePedidos>();

        public List<Facturas> Facturas { get; set; } = new List<Facturas>();
    }
}
