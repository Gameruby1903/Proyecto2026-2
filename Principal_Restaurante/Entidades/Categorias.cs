using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Categorias
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Disponible { get; set; }
        public int OrdenMenu { get; set; }

        public List<Productos> Productos { get; set; } = new List<Productos>();
    }
}
