using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SucursalesController : ControllerBase
    {
        private ISucursalesAplicacion ISucursalesAplicacion;

        public SucursalesController(ISucursalesAplicacion ISucursalesAplicacion)
        {
            this.ISucursalesAplicacion = ISucursalesAplicacion;
        }

        [HttpPost]
        public List<Sucursales> Consultar()
        {
            return this.ISucursalesAplicacion.Consultar();
        }

        [HttpPost]
        public List<Sucursales> Buscar(Sucursales entidad)
        {
            return this.ISucursalesAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Sucursales Insertar(Sucursales entidad)
        {
            return this.ISucursalesAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Sucursales Actualizar(Sucursales entidad)
        {
            return this.ISucursalesAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Sucursales Borrar(Sucursales entidad)
        {
            return this.ISucursalesAplicacion.Borrar(entidad);
        }
    }
}
