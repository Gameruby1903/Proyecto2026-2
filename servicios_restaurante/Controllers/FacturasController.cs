using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FacturasController : ControllerBase
    {
        private IFacturasAplicacion IFacturasAplicacion;

        public FacturasController(IFacturasAplicacion IFacturasAplicacion)
        {
            this.IFacturasAplicacion = IFacturasAplicacion;
        }

        [HttpPost]
        public List<Facturas> Consultar()
        {
            return this.IFacturasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Facturas> Buscar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Facturas Insertar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Facturas Actualizar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Facturas Borrar(Facturas entidad)
        {
            return this.IFacturasAplicacion.Borrar(entidad);
        }
    }
}
