using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Sucursales
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Ciudad { get; set; }

        public List<Empleados> Empleados { get; set; } = new List<Empleados>();
    }
}
