
using System.ComponentModel.DataAnnotations.Schema;


    namespace lib_aplicaciones.Entidades
    {
        public class Permisos
        {
            public int Id { get; set; }
            public string? Codigo { get; set; }
            public string? Descripcion { get; set; }

        [NotMapped] public List<RolPermisos>? RolPermisos { get; set; } 
        }
    }

