using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class DetallePedidosModel : PageModel
    {
        private IDetallePedidosPresentacion IDetallePedidosPresentacion;
        private IPedidosPresentacion IPedidosPresentacion;
        private IProductosPresentacion IProductosPresentacion;

        public DetallePedidosModel(IDetallePedidosPresentacion IDetallePedidosPresentacion,
            IPedidosPresentacion IPedidosPresentacion,
            IProductosPresentacion IProductosPresentacion)
        {
            this.IDetallePedidosPresentacion = IDetallePedidosPresentacion;
            this.IPedidosPresentacion = IPedidosPresentacion;
            this.IProductosPresentacion = IProductosPresentacion;
            this.Filtro = new DetallePedidos();

            this.Lista = new List<DetallePedidos>();
            this.Pedidos = new List<Pedidos>();
            this.Productos = new List<Productos>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<DetallePedidos> Lista { get; set; }
        [BindProperty] public List<Pedidos> Pedidos { get; set; }
        [BindProperty] public List<Productos> Productos { get; set; }
        [BindProperty] public DetallePedidos? Filtro { get; set; }
        [BindProperty] public DetallePedidos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;
        
        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Detalle de Pedidos", Lista);
            return File(pdf, "application/pdf", $"DetallePedidos_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        private void CargarListas()
        {
            this.Pedidos = this.IPedidosPresentacion.Consultar();
            this.Productos = this.IProductosPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro!.Cantidad = Filtro!.Cantidad;
                Lista = this.IDetallePedidosPresentacion.Buscar(Filtro!);
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
                this.Actual = new DetallePedidos();
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
                    this.Actual = this.IDetallePedidosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IDetallePedidosPresentacion.Actualizar(this.Actual!);
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
                this.Actual = this.IDetallePedidosPresentacion.Borrar(this.Actual!);
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
