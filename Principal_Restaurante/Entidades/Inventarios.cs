using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Inventarios
    {
        public int Id { get; set; }

        public int Ingrediente { get; set; }
        [ForeignKey("Ingrediente")] public Ingredientes? _Ingrediente { get; set; }

        public decimal CantDisponible { get; set; }
        public decimal CantMinima { get; set; }
        public DateTime UltimaActu { get; set; }
    }
}
