using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;




    namespace lib_aplicaciones.Entidades
    {
        public class Roles
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Descripcion { get; set; }

            [NotMapped] public List<Usuarios>? Usuarios { get; set; } 
            [NotMapped] public List<RolPermisos>? RolPermisos { get; set; } 
        }
    }


