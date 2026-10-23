using lib_aplicaciones.Interfaces;
using lib_aplicaciones.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RecetaIngredientesController : ControllerBase
    {
        private IRecetaIngredientesAplicacion IRecetaIngredientesAplicacion;

        public RecetaIngredientesController(IRecetaIngredientesAplicacion IRecetaIngredientesAplicacion)
        {
            this.IRecetaIngredientesAplicacion = IRecetaIngredientesAplicacion;
        }

        [HttpPost]
        public List<RecetaIngredientes> Consultar()
        {
            return this.IRecetaIngredientesAplicacion.Consultar();
        }

        [HttpPost]
        public List<RecetaIngredientes> Buscar(RecetaIngredientes entidad)
        {
            return this.IRecetaIngredientesAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public RecetaIngredientes Insertar(RecetaIngredientes entidad)
        {
            return this.IRecetaIngredientesAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public RecetaIngredientes Actualizar(RecetaIngredientes entidad)
        {
            return this.IRecetaIngredientesAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public RecetaIngredientes Borrar(RecetaIngredientes entidad)
        {
            return this.IRecetaIngredientesAplicacion.Borrar(entidad);
        }
    }
}
