using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private IReservasPresentacion IReservasPresentacion;
        private IClientesPresentacion IClientesPresentacion;
        private IMesasPresentacion IMesasPresentacion;

        public ReservasModel(IReservasPresentacion IReservasPresentacion, IClientesPresentacion IClientesPresentacion, IMesasPresentacion IMesasPresentacion)
        {
            this.IReservasPresentacion = IReservasPresentacion;
            this.IClientesPresentacion = IClientesPresentacion;
            this.IMesasPresentacion = IMesasPresentacion;
            this.Filtro = new Reservas();

            this.Lista = new List<Reservas>();

            this.Clientes = new List<Clientes>();
            this.Mesas = new List<Mesas>();

        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Reservas> Lista { get; set; }
        [BindProperty] public List<Clientes> Clientes { get; set; }
        [BindProperty] public List<Mesas> Mesas { get; set; }
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtConsultar(); }

        private void CargarListas()
        {
            this.Clientes = this.IClientesPresentacion.Consultar();
            this.Mesas = this.IMesasPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Reservas();

                Lista = this.IReservasPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Reservas>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Reservas();

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
                    this.Actual = this.IReservasPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IReservasPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.IReservasPresentacion.Borrar(this.Actual!);
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