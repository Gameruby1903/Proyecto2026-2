using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoriasController : ControllerBase
    {
        private ICategoriasAplicacion ICategoriasAplicacion;

        public CategoriasController(ICategoriasAplicacion ICategoriasAplicacion)
        {
            this.ICategoriasAplicacion = ICategoriasAplicacion;
        }

        [HttpPost]
        public List<Categorias> Consultar()
        {
            return this.ICategoriasAplicacion.Consultar();
        }

        [HttpPost]
        public List<Categorias> Buscar(Categorias entidad)
        {
            return this.ICategoriasAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Categorias Insertar(Categorias entidad)
        {
            return this.ICategoriasAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Categorias Actualizar(Categorias entidad)
        {
            return this.ICategoriasAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Categorias Borrar(Categorias entidad)
        {
            return this.ICategoriasAplicacion.Borrar(entidad);
        }
    }
}
