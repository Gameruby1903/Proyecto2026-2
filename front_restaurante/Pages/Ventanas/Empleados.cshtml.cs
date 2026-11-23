using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class EmpleadosModel : PageModel
    {
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private ITipoEmpleadosPresentacion ITipoEmpleadosPresentacion;
        private ISucursalesPresentacion ISucursalesPresentacion;

        public EmpleadosModel(IEmpleadosPresentacion IEmpleadosPresentacion, ITipoEmpleadosPresentacion ITipoEmpleadosPresentacion, ISucursalesPresentacion ISucursalesPresentacion)
        {
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.ITipoEmpleadosPresentacion = ITipoEmpleadosPresentacion;
            this.ISucursalesPresentacion = ISucursalesPresentacion;
            this.Filtro = new Empleados();

            this.Lista = new List<Empleados>();
            this.TiposEmpleados = new List<TipoEmpleados>();
            this.Sucursales = new List<Sucursales>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Empleados> Lista { get; set; }
        [BindProperty] public List<TipoEmpleados> TiposEmpleados { get; set; }
        [BindProperty] public List<Sucursales> Sucursales { get; set; }
        [BindProperty] public Empleados? Filtro { get; set; }
        [BindProperty] public Empleados? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        
        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Empleados", Lista);
            return File(pdf, "application/pdf", $"Empleados_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private void CargarListas()
        {
            this.TiposEmpleados = this.ITipoEmpleadosPresentacion.Consultar();
            this.Sucursales = this.ISucursalesPresentacion.Consultar();
        }
        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Empleados();

                Lista = this.IEmpleadosPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Empleados>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Empleados();

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
                    this.Actual = this.IEmpleadosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IEmpleadosPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.IEmpleadosPresentacion.Borrar(this.Actual!);
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