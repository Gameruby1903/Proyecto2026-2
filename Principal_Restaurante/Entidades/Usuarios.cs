using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.Entidades
{
    public class Usuarios
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? Clave { get; set; }
        public bool Activo { get; set; }
        public DateTime? UltimoAcceso { get; set; }

        public int Rol { get; set; }
        [ForeignKey("Rol")] public Roles? _Rol { get; set; }

        public int? Empleado { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }

        public int? Cliente { get; set; }
        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
    }
}