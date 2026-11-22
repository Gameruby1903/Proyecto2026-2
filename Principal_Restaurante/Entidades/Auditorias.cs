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
        public string? Entidad { get; set; }
        public int IdRegistro { get; set; }
        public string? Detalle { get; set; }
        public DateTime Fecha { get; set; }
    }
}
