using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturacionController : ControllerBase
    {
        private IFacturacionAplicacion IFacturacionAplicacion;

        public FacturacionController(IFacturacionAplicacion IFacturacionAplicacion)
        {
            this.IFacturacionAplicacion = IFacturacionAplicacion;
        }

        [HttpPost]
        public List<Pedidos> PedidosPendientes()
        {
            return this.IFacturacionAplicacion.PedidosPendientes();
        }

        [HttpPost]
        public Facturas Calcular(Facturas entidad)
        {
            return this.IFacturacionAplicacion.Calcular(entidad);
        }

        [HttpPost]
        public Facturas Facturar(Facturas entidad)
        {
            return this.IFacturacionAplicacion.Facturar(entidad);
        }
    }
}
