using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string? Imagen { get; set; }

        [NotMapped] public List<Productos>? Productos { get; set; } 
    }
}
