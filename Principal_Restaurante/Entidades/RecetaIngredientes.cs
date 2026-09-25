using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class RecetaIngredientes
    {
        public int Id { get; set; }

        public int Producto { get; set; }
        [ForeignKey("Producto")] public Productos? _Producto { get; set; }

        public int Ingrediente { get; set; }
        [ForeignKey("Ingrediente")] public Ingredientes? _Ingrediente { get; set; }
        public decimal CantiReque { get; set; }
        public string? Notas { get; set; }
    }
}
