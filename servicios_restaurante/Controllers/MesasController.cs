using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MesasController : ControllerBase
    {
        private IMesasAplicacion IMesasAplicacion;

        public MesasController(IMesasAplicacion IMesasAplicacion)
        {
            this.IMesasAplicacion = IMesasAplicacion;
        }

        [HttpPost]
        public List<Mesas> Consultar()
        {
            return this.IMesasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Mesas> Buscar(Mesas entidad)
        {
            return this.IMesasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Mesas Insertar(Mesas entidad)
        {
            return this.IMesasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Mesas Actualizar(Mesas entidad)
        {
            return this.IMesasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Mesas Borrar(Mesas entidad)
        {
            return this.IMesasAplicacion.Borrar(entidad);
        }
    }
}
