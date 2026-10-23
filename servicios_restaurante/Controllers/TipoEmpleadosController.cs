using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TipoEmpleadosController : ControllerBase
    {
        private ITipoEmpleadosAplicacion ITipoEmpleadosAplicacion;

        public TipoEmpleadosController(ITipoEmpleadosAplicacion ITipoEmpleadosAplicacion)
        {
            this.ITipoEmpleadosAplicacion = ITipoEmpleadosAplicacion;
        }

        [HttpPost]
        public List<TipoEmpleados> Consultar()
        {
            return this.ITipoEmpleadosAplicacion.Consultar();
        }

        [HttpPost]
        public List<TipoEmpleados> Buscar(TipoEmpleados entidad)
        {
            return this.ITipoEmpleadosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public TipoEmpleados Insertar(TipoEmpleados entidad)
        {
            return this.ITipoEmpleadosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public TipoEmpleados Actualizar(TipoEmpleados entidad)
        {
            return this.ITipoEmpleadosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public TipoEmpleados Borrar(TipoEmpleados entidad)
        {
            return this.ITipoEmpleadosAplicacion.Borrar(entidad);
        }
    }
}
