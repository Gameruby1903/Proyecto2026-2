using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class ResenasModel : PageModel
    {
        private IResenasPresentacion IResenasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IProductosPresentacion IProductosPresentacion;

        public ResenasModel(IResenasPresentacion IResenasPresentacion, IClientesPresentacion IClientesPresentacion, IProductosPresentacion IProductosPresentacion   )
        {
            this.IResenasPresentacion = IResenasPresentacion;

            this.IClientesPresentacion = IClientesPresentacion;
            this.IProductosPresentacion = IProductosPresentacion;
            this.Filtro = new Resenas();

            this.Lista = new List<Resenas>();
            this.Clientes = new List<Clientes>();
            this.Productos = new List<Productos>();

        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Resenas> Lista { get; set; }
        [BindProperty] public List<Clientes> Clientes { get; set; }
        [BindProperty] public List<Productos> Productos { get; set; }
        [BindProperty] public Resenas? Filtro { get; set; }
        [BindProperty] public Resenas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtConsultar(); }

        private void CargarListas()
        {
            this.Clientes = this.IClientesPresentacion.Consultar();
            this.Productos = this.IProductosPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Resenas();

                Lista = this.IResenasPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Resenas>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Resenas();

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
                    this.Actual = this.IResenasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IResenasPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.IResenasPresentacion.Borrar(this.Actual!);
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