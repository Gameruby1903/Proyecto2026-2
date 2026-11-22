
using lib_aplicaciones.Entidades;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentacion_restaurante.Interfaces;

namespace front_restaurante.Pages.Ventanas
{
    public class PedidosModel : PageModel
    {
        private IPedidosPresentacion IPedidosPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;
        private IMesasPresentacion IMesasPresentacion;
        private IClientesPresentacion IClientesPresentacion;

        public PedidosModel(
            IPedidosPresentacion IPedidosPresentacion,
            IEmpleadosPresentacion IEmpleadosPresentacion,
            IMesasPresentacion IMesasPresentacion,
            IClientesPresentacion IClientesPresentacion)
        {
            this.IPedidosPresentacion = IPedidosPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.IMesasPresentacion = IMesasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;

            this.Filtro = new Pedidos();
            

            this.Lista = new List<Pedidos>();
            this.Empleados = new List<Empleados>();
            this.Mesas = new List<Mesas>();
            this.Clientes = new List<Clientes>();
        }

        [BindProperty] public List<Pedidos> Lista { get; set; }
        [BindProperty] public List<Empleados> Empleados { get; set; }
        [BindProperty] public List<Mesas> Mesas { get; set; }
        [BindProperty] public List<Clientes> Clientes { get; set; }

        [BindProperty] public Pedidos? Filtro { get; set; }
        [BindProperty] public Pedidos? Actual { get; set; }

        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        private void CargarListas()
        {
            this.Empleados = this.IEmpleadosPresentacion.Consultar();
            this.Clientes = this.IClientesPresentacion.Consultar();
            this.Mesas = this.IMesasPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Pedidos();

                Lista = this.IPedidosPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Pedidos>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Pedidos();

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

                if (this.Actual!.Id == 0)
                    this.Actual = this.IPedidosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IPedidosPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.IPedidosPresentacion.Borrar(this.Actual!);
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
