using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Proveedores
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Direccion { get; set; }

        public List<Ingredientes> Ingredientes { get; set; } = new List<Ingredientes>();

       
        public List<Compras> Compras { get; set; } = new List<Compras>();
    }
}
