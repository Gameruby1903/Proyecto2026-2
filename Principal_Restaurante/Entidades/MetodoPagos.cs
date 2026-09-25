using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class MetodoPagos
    {
        public int Id { get; set; }
        public string? Tipo { get; set; }
        public string? Descripcion { get; set; }

    
        public List<Facturas> Facturas { get; set; } = new List<Facturas>();
    }

}
