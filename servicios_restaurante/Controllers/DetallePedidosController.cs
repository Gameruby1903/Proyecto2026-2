using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DetallePedidosController : ControllerBase
    {
        private IDetallePedidosAplicacion IDetallePedidosAplicacion;

        public DetallePedidosController(IDetallePedidosAplicacion IDetallePedidosAplicacion)
        {
            this.IDetallePedidosAplicacion = IDetallePedidosAplicacion;
        }

        [HttpPost]
        public List<DetallePedidos> Consultar()
        {
            return this.IDetallePedidosAplicacion.Consultar();
        }

        [HttpPost]
        public List<DetallePedidos> Buscar(DetallePedidos entidad)
        {
            return this.IDetallePedidosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public DetallePedidos Insertar(DetallePedidos entidad)
        {
            return this.IDetallePedidosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public DetallePedidos Actualizar(DetallePedidos entidad)
        {
            return this.IDetallePedidosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public DetallePedidos Borrar(DetallePedidos entidad)
        {
            return this.IDetallePedidosAplicacion.Borrar(entidad);
        }
    }
}
