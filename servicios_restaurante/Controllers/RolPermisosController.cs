using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RolPermisosController : ControllerBase
    {
        private IRolPermisosAplicacion IRolPermisosAplicacion;

        public RolPermisosController(
            IRolPermisosAplicacion IRolPermisosAplicacion)
        {
            this.IRolPermisosAplicacion = IRolPermisosAplicacion;
        }

        [HttpPost]
        public List<RolPermisos> Consultar()
        {
            return this.IRolPermisosAplicacion.Consultar();
        }

        [HttpPost]
        public List<RolPermisos> Buscar(RolPermisos entidad)
        {
            return this.IRolPermisosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public RolPermisos Insertar(RolPermisos entidad)
        {
            return this.IRolPermisosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public RolPermisos Actualizar(RolPermisos entidad)
        {
            return this.IRolPermisosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public RolPermisos Borrar(RolPermisos entidad)
        {
            return this.IRolPermisosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public List<RolPermisos> ConsultarPorRol(RolPermisos entidad)
        {
            return this.IRolPermisosAplicacion.ConsultarPorRol(entidad);
        }
    }
}