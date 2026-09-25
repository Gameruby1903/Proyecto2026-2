using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }

        public List<Resenas> Resenas { get; set; } = new List<Resenas>();

        
        public List<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
        public List<Reservas> Reservas { get; set; } = new List<Reservas>();
    }
}
