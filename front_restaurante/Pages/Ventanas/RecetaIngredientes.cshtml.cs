using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace front_restaurante.Pages.Ventanas
{
    public class RecetaIngredientesModel : PageModel
    {
        private IRecetaIngredientesPresentacion IRecetaIngredientesPresentacion;
        private IProductosPresentacion IProductosPresentacion;
        private IIngredientesPresentacion IIngredientesPresentacion;

        public RecetaIngredientesModel(IRecetaIngredientesPresentacion IRecetaIngredientesPresentacion,IProductosPresentacion IProductosPresentacion, IIngredientesPresentacion IIngredientesPresentacion)
        {
            this.IRecetaIngredientesPresentacion = IRecetaIngredientesPresentacion;
            this.IProductosPresentacion = IProductosPresentacion;
            this.IIngredientesPresentacion = IIngredientesPresentacion;
            this.Filtro = new RecetaIngredientes();

            this.Lista = new List<RecetaIngredientes>();
            this.Productos = new List<Productos>();
            this.Ingredientes = new List<Ingredientes>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<RecetaIngredientes> Lista { get; set; }
        [BindProperty] public List<Productos> Productos { get; set; }
        [BindProperty] public List<Ingredientes> Ingredientes { get; set; }
        [BindProperty] public RecetaIngredientes? Filtro { get; set; }
        [BindProperty] public RecetaIngredientes? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtConsultar(); }

        private void CargarListas()
        {
            this.Productos = this.IProductosPresentacion.Consultar();
            this.Ingredientes = this.IIngredientesPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new RecetaIngredientes();

                Lista = this.IRecetaIngredientesPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<RecetaIngredientes>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new RecetaIngredientes();

                CargarListas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {

                if (FormFile != null)
                {
                    var memoryStream = new MemoryStream();
                    FormFile.CopyToAsync(memoryStream).Wait();
                    Actual!.Imagen = Convert.ToBase64String(memoryStream.ToArray());
                    memoryStream.Dispose();
                }
                if (this.Actual!.Id == 0)
                    this.Actual = this.IRecetaIngredientesPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IRecetaIngredientesPresentacion.Actualizar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);

                Formulario = true;
                CargarListas();
            }
            finally
            {
                OnPostBtConsultar();
            }
        }

        public void OnPostBtEditar(string data)
        {
            try
            {
                Formulario = true;

                OnPostBtConsultar();

                this.Actual = this.Lista.FirstOrDefault(
                    x => x.Id == Convert.ToInt32(data));

                CargarListas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPregBorrar(string data)
        {
            try
            {
                Borrar = true;

                OnPostBtConsultar();

                this.Actual = this.Lista.FirstOrDefault(
                    x => x.Id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (this.Actual!.Id == 0)
                    return;

                this.Actual = this.IRecetaIngredientesPresentacion.Borrar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtConsultar();
            }
        }
    }
}