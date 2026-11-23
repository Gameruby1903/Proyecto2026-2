using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public string? Imagen { get; set; }

        [NotMapped] public List<Resenas>? Resenas { get; set; }
        [NotMapped] public List<Usuarios>? Usuarios { get; set; }

        [NotMapped] public List<Pedidos>? Pedidos { get; set; } 
        [NotMapped] public List<Reservas>? Reservas { get; set; } 
    }
}
