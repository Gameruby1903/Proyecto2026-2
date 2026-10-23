using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PedidosController : ControllerBase
    {
        private IPedidosAplicacion IPedidosAplicacion;

        public PedidosController(IPedidosAplicacion IPedidosAplicacion)
        {
            this.IPedidosAplicacion = IPedidosAplicacion;
        }

        [HttpPost]
        public List<Pedidos> Consultar()
        {
            return this.IPedidosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Pedidos> Buscar(Pedidos entidad)
        {
            return this.IPedidosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Pedidos Insertar(Pedidos entidad)
        {
            return this.IPedidosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Pedidos Actualizar(Pedidos entidad)
        {
            return this.IPedidosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Pedidos Borrar(Pedidos entidad)
        {
            return this.IPedidosAplicacion.Borrar(entidad);
        }
    }
}
