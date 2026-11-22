using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class MetodoPagos
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }


        [NotMapped] public List<Facturas>? Facturas { get; set; } 
    }

}
