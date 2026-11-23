using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class DetalleComprasModel : PageModel
    {
        private IDetalleComprasPresentacion IDetalleComprasPresentacion;
        private IIngredientesPresentacion IIngredientesComprasPresentacion;
        private IComprasPresentacion IComprasPresentacion;

        public DetalleComprasModel(IDetalleComprasPresentacion IDetalleComprasPresentacion,
            IIngredientesPresentacion IIngredientesComprasPresentacion, IComprasPresentacion IComprasPresentacion)
        {
            this.IDetalleComprasPresentacion = IDetalleComprasPresentacion;
            this.IIngredientesComprasPresentacion = IIngredientesComprasPresentacion;
            this.IComprasPresentacion = IComprasPresentacion;
            this.Filtro = new DetalleCompras();

            this.Lista = new List<DetalleCompras>();
            this.Ingredientes = new List<Ingredientes>();
            this.Compras = new List<Compras>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<DetalleCompras> Lista { get; set; }
        [BindProperty] public List<Ingredientes> Ingredientes { get; set; }
        [BindProperty] public List<Compras> Compras { get; set; }
        [BindProperty] public DetalleCompras? Filtro { get; set; }
        [BindProperty] public DetalleCompras? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        
        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Detalle de Compras", Lista);
            return File(pdf, "application/pdf", $"DetalleCompras_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private void CargarListas()
        {
            this.Ingredientes = this.IIngredientesComprasPresentacion.Consultar();
            this.Compras = this.IComprasPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro!.Cantidad = Filtro!.Cantidad;
                Lista = this.IDetalleComprasPresentacion.Buscar(Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new DetalleCompras();
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
                    this.Actual = this.IDetalleComprasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IDetalleComprasPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IDetalleComprasPresentacion.Borrar(this.Actual!);
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
