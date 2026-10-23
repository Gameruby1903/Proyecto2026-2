using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class IngredientesController : ControllerBase
    {
        private IIngredientesAplicacion IIngredientesAplicacion;

        public IngredientesController(IIngredientesAplicacion IIngredientesAplicacion)
        {
            this.IIngredientesAplicacion = IIngredientesAplicacion;
        }

        [HttpPost]
        public List<Ingredientes> Consultar()
        {
            return this.IIngredientesAplicacion.Consultar();
        }

        [HttpPost]
        public List<Ingredientes> Buscar(Ingredientes entidad)
        {
            return this.IIngredientesAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Ingredientes Insertar(Ingredientes entidad)
        {
            return this.IIngredientesAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Ingredientes Actualizar(Ingredientes entidad)
        {
            return this.IIngredientesAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Ingredientes Borrar(Ingredientes entidad)
        {
            return this.IIngredientesAplicacion.Borrar(entidad);
        }
    }
}
