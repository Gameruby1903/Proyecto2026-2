using lib_aplicaciones.Entidades;
using presentacion_restaurante.Interfaces;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages.Ventanas
{
    public class TurnosModel : PageModel
    {
        private ITurnosPresentacion ITurnosPresentacion;
        private IEmpleadosPresentacion IEmpleadosPresentacion;

        public TurnosModel(ITurnosPresentacion ITurnosPresentacion, IEmpleadosPresentacion IEmpleadosPresentacion)
        {
            this.ITurnosPresentacion = ITurnosPresentacion;
            this.IEmpleadosPresentacion = IEmpleadosPresentacion;
            this.Filtro = new Turnos();

            this.Lista = new List<Turnos>();
            this.Empleados = new List<Empleados>();
        }
        public IFormFile? FormFile { get; set; }

        [BindProperty] public List<Turnos> Lista { get; set; }
        [BindProperty] public List<Empleados> Empleados { get; set; }
        [BindProperty] public Turnos? Filtro { get; set; }
        [BindProperty] public Turnos? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtConsultar(); }

        private void CargarListas()
        {
            this.Empleados = this.IEmpleadosPresentacion.Consultar();
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro ??= new Turnos();

                Lista = this.ITurnosPresentacion.Buscar(Filtro);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Turnos>();
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Turnos();

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
                    this.Actual = this.ITurnosPresentacion.Insertar(this.Actual!);
                else
                    this.Actual = this.ITurnosPresentacion.Actualizar(this.Actual!);
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

                this.Actual = this.ITurnosPresentacion.Borrar(this.Actual!);
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