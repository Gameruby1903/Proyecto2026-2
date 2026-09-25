using System;
using System.Collections.Generic;
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

        
        public List<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
        public List<Reservas> Reservas { get; set; } = new List<Reservas>();
    }
}
