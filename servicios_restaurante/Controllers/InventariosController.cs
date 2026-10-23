using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InventariosController : ControllerBase
    {
        private IInventariosAplicacion IInventariosAplicacion;

        public InventariosController(IInventariosAplicacion IInventariosAplicacion)
        {
            this.IInventariosAplicacion = IInventariosAplicacion;
        }

        [HttpPost]
        public List<Inventarios> Consultar()
        {
            return this.IInventariosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Inventarios> Buscar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Inventarios Insertar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Inventarios Actualizar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Inventarios Borrar(Inventarios entidad)
        {
            return this.IInventariosAplicacion.Borrar(entidad);
        }
    }
}
