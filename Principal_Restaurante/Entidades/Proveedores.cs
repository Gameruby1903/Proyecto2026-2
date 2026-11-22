using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped] public List<Ingredientes>? Ingredientes { get; set; }


        [NotMapped] public List<Compras>? Compras { get; set; } 
    }
}
