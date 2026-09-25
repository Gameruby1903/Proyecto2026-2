using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Empleados
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public DateTime FechaContratacion { get; set; }

        public int TipoEmpleado { get; set; }
        [ForeignKey("TipoEmpleado")] public TipoEmpleados? _TipoEmpleado { get; set; }

        public int Sucursal { get; set; }
        [ForeignKey("Sucursal")] public Sucursales? _Sucursal { get; set; }

        
        public List<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
        public List<Turnos> Turnos { get; set; } = new List<Turnos>();
    }
}
