using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class InventariosModel : PageModel
    {
        private IInventariosPresentacion IInventariosPresentacion;
        private IIngredientesPresentacion IIngredientesPresentacion;

        public InventariosModel(IInventariosPresentacion IInventariosPresentacion, IIngredientesPresentacion IIngredientesPresentacion)
        {
            this.IInventariosPresentacion = IInventariosPresentacion;
            this.IIngredientesPresentacion = IIngredientesPresentacion;
            this.Filtro = new Inventarios();
            
            this.Lista = new List<Inventarios>();
            this.Ingredientes = new List<Ingredientes>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Inventarios> Lista { get; set; }
        [BindProperty] public List<Ingredientes> Ingredientes  { get; set; }
        [BindProperty] public Inventarios? Filtro { get; set; }
        [BindProperty] public Inventarios? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        
        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Inventarios", Lista);
            return File(pdf, "application/pdf", $"Inventarios_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private void CargarListas()
        {
            this.Ingredientes = this.IIngredientesPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Inventarios();

                Lista = this.IInventariosPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Inventarios>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Inventarios();

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
                    this.Actual = this.IInventariosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IInventariosPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.IInventariosPresentacion.Borrar(this.Actual!);
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
