using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Mesas
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }
        public bool Estado { get; set; }
        public string? Ubicacion { get; set; }

        public string? Imagen { get; set; }

        [NotMapped] public List<Pedidos>? Pedidos { get; set; } 
        [NotMapped] public List<Reservas>? Reservas { get; set; } 
    }
}
