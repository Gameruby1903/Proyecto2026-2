using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TurnosController : ControllerBase
    {
        private ITurnosAplicacion ITurnosAplicacion;

        public TurnosController(ITurnosAplicacion ITurnosAplicacion)
        {
            this.ITurnosAplicacion = ITurnosAplicacion;
        }

        [HttpPost]
        public List<Turnos> Consultar()
        {
            return this.ITurnosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Turnos> Buscar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Turnos Insertar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Turnos Actualizar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Turnos Borrar(Turnos entidad)
        {
            return this.ITurnosAplicacion.Borrar(entidad);
        }
    }
}
