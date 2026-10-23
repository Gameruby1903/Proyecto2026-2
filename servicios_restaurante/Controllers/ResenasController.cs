using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ResenasController : ControllerBase
    {
        private IResenasAplicacion IResenasAplicacion;

        public ResenasController(IResenasAplicacion IResenasAplicacion)
        {
            this.IResenasAplicacion = IResenasAplicacion;
        }

        [HttpPost]
        public List<Resenas> Consultar()
        {
            return this.IResenasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Resenas> Buscar(Resenas entidad)
        {
            return this.IResenasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Resenas Insertar(Resenas entidad)
        {
            return this.IResenasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Resenas Actualizar(Resenas entidad)
        {
            return this.IResenasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Resenas Borrar(Resenas entidad)
        {
            return this.IResenasAplicacion.Borrar(entidad);
        }
    }
}
