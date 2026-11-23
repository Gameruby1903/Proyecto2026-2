using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class IngredientesModel : PageModel
    {
        private IIngredientesPresentacion IIngredientesPresentacion;
        private IProveedoresPresentacion IProveedoresPresentacion;

        public IngredientesModel(IIngredientesPresentacion IIngredientesPresentacion, IProveedoresPresentacion IProveedoresPresentacion)
        {
            this.IIngredientesPresentacion = IIngredientesPresentacion;
            this.IProveedoresPresentacion = IProveedoresPresentacion;
            this.Filtro = new Ingredientes();

            this.Lista = new List<Ingredientes>();
            this.Proveedores = new List<Proveedores>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Ingredientes> Lista { get; set; }
        
        [BindProperty] public List<Proveedores> Proveedores { get; set; }

        [BindProperty] public Ingredientes? Filtro { get; set; }
        [BindProperty] public Ingredientes? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        
        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Ingredientes", Lista);
            return File(pdf, "application/pdf", $"Ingredientes_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private void CargarListas()
        {
            this.Proveedores = this.IProveedoresPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Ingredientes();

                Lista = this.IIngredientesPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Ingredientes>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Ingredientes();

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
                    this.Actual = this.IIngredientesPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IIngredientesPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.IIngredientesPresentacion.Borrar(this.Actual!);
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
