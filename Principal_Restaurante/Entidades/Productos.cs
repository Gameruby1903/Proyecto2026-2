using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

        public int Categoria { get; set; }
        [ForeignKey("Categoria")] public Categorias? _Categoria { get; set; }

        [NotMapped] public List<Resenas>? Resenas { get; set; } 

        // Relaciones por lista añadidas
        [NotMapped] public List<DetallePedidos>? Detalles { get; set; } 
        [NotMapped] public List<RecetaIngredientes>? Recetas { get; set; } 
    }
}
