using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Auditorias
    {
        public int Id { get; set; }
        public string? Usuario { get; set; }
        public string? Accion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
