using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductosController : ControllerBase
    {
        private IProductosAplicacion IProductosAplicacion;

        public ProductosController(IProductosAplicacion IProductosAplicacion)
        {
            this.IProductosAplicacion = IProductosAplicacion;
        }

        [HttpPost]
        public List<Productos> Consultar()
        {
            return this.IProductosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Productos> Buscar(Productos entidad)
        {
            return this.IProductosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Productos Insertar(Productos entidad)
        {
            return this.IProductosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Productos Actualizar(Productos entidad)
        {
            return this.IProductosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Productos Borrar(Productos entidad)
        {
            return this.IProductosAplicacion.Borrar(entidad);
        }
    }
}
