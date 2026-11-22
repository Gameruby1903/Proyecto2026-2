using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PermisosController : ControllerBase
    {
        private IPermisosAplicacion IPermisosAplicacion;

        public PermisosController(IPermisosAplicacion IPermisosAplicacion)
        {
            this.IPermisosAplicacion = IPermisosAplicacion;
        }

        [HttpPost]
        public List<Permisos> Consultar()
        {
            return this.IPermisosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Permisos> Buscar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Permisos Insertar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Permisos Actualizar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Permisos Borrar(Permisos entidad)
        {
            return this.IPermisosAplicacion.Borrar(entidad);
        }
    }
}