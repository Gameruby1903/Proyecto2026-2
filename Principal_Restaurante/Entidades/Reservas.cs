using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Reservas
    {
        public int Id { get; set; }

        public int Cliente { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }

        public int Mesa { get; set; }
        [ForeignKey("Mesa")] public Mesas? _Mesa { get; set; }

        public DateTime FechaHora { get; set; }
        public int NPersonas { get; set; }
        public string? Estado { get; set; }
    }
}
