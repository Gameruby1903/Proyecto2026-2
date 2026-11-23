using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class ComprasModel : PageModel
    {
        private IComprasPresentacion IComprasPresentacion;
        private IProveedoresPresentacion IProveedoresPresentacion;

        public ComprasModel(IComprasPresentacion IComprasPresentacion, 
            IProveedoresPresentacion IProveedoresPresentacion)

        {
            this.IComprasPresentacion = IComprasPresentacion;
            this.IProveedoresPresentacion = IProveedoresPresentacion;
            this.Filtro = new Compras();



            this.Lista = new List<Compras>();
            this.Proveedores = new List<Proveedores>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Compras> Lista { get; set; }
        [BindProperty] public List<Proveedores> Proveedores { get; set; }
        [BindProperty] public Compras? Filtro { get; set; }
        [BindProperty] public Compras? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        
        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Compras", Lista);
            return File(pdf, "application/pdf", $"Compras_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private void CargarListas()
        {
            this.Proveedores = this.IProveedoresPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro!.Estado = Filtro!.Estado ?? ""; 
                Lista = this.IComprasPresentacion.Buscar(Filtro!);
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
                this.Actual = new Compras();
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
                    this.Actual = this.IComprasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IComprasPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
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
                this.Actual = this.IComprasPresentacion.Borrar(this.Actual!);
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
