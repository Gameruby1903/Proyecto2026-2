using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ReservasController : ControllerBase
    {
        private IReservasAplicacion IReservasAplicacion;

        public ReservasController(IReservasAplicacion IReservasAplicacion)
        {
            this.IReservasAplicacion = IReservasAplicacion;
        }

        [HttpPost]
        public List<Reservas> Consultar()
        {
            return this.IReservasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Reservas> Buscar(Reservas entidad)
        {
            return this.IReservasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Reservas Insertar(Reservas entidad)
        {
            return this.IReservasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Reservas Actualizar(Reservas entidad)
        {
            return this.IReservasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Reservas Borrar(Reservas entidad)
        {
            return this.IReservasAplicacion.Borrar(entidad);
        }
    }
}
