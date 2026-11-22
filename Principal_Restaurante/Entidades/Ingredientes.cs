using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class Ingredientes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? UniMedida { get; set; }
        public decimal CostoUni { get; set; }

        public int Proveedor { get; set; }
        [ForeignKey("Proveedor")] public Proveedores? _Proveedor { get; set; }

        [NotMapped] public List<RecetaIngredientes>? Recetas { get; set; }
              
        [NotMapped] public List<Inventarios>? Inventarios { get; set; } 
        [NotMapped] public List<DetalleCompras>? DetalleCompras { get; set; } 
    }
}
