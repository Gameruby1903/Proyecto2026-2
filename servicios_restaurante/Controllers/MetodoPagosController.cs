using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MetodoPagosController : ControllerBase
    {
        private IMetodoPagosAplicacion IMetodoPagosAplicacion;

        public MetodoPagosController(IMetodoPagosAplicacion IMetodoPagosAplicacion)
        {
            this.IMetodoPagosAplicacion = IMetodoPagosAplicacion;
        }

        [HttpPost]
        public List<MetodoPagos> Consultar()
        {
            return this.IMetodoPagosAplicacion.Consultar();
        }

        [HttpPost]
        public List<MetodoPagos> Buscar(MetodoPagos entidad)
        {
            return this.IMetodoPagosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public MetodoPagos Insertar(MetodoPagos entidad)
        {
            return this.IMetodoPagosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public MetodoPagos Actualizar(MetodoPagos entidad)
        {
            return this.IMetodoPagosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public MetodoPagos Borrar(MetodoPagos entidad)
        {
            return this.IMetodoPagosAplicacion.Borrar(entidad);
        }
    }
}
