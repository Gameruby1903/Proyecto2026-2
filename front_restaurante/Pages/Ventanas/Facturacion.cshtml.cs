using lib_aplicaciones.Entidades;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentacion_restaurante.Interfaces;

namespace front_restaurante.Pages.Ventanas
{
    public class FacturacionModel : PageModel
    {
        private IFacturacionPresentacion IFacturacionPresentacion;
        private IMetodoPagosPresentacion IMetodoPagosPresentacion;

        public FacturacionModel(
            IFacturacionPresentacion IFacturacionPresentacion,
            IMetodoPagosPresentacion IMetodoPagosPresentacion)
        {
            this.IFacturacionPresentacion = IFacturacionPresentacion;
            this.IMetodoPagosPresentacion = IMetodoPagosPresentacion;

            this.Lista = new List<Pedidos>();
            this.MetodosPago = new List<MetodoPagos>();
        }

        [BindProperty] public List<Pedidos> Lista { get; set; }
        [BindProperty] public List<MetodoPagos> MetodosPago { get; set; }
        [BindProperty] public Facturas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;

        public void OnGet()
        {
            OnPostBtConsultar();
        }

        // Lista los pedidos que aun no tienen factura
        public void OnPostBtConsultar()
        {
            try
            {
                Formulario = false;
                Lista = this.IFacturacionPresentacion.PedidosPendientes();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Lista = new List<Pedidos>();
            }
        }

        // Muestra la vista previa de la factura del pedido seleccionado
        public void OnPostBtPreparar(string data)
        {
            try
            {
                this.Actual = this.IFacturacionPresentacion.Calcular(
                    new Facturas { Pedido = Convert.ToInt32(data) });

                this.MetodosPago = this.IMetodoPagosPresentacion.Consultar();
                Formulario = true;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                OnPostBtConsultar();
            }
        }

        // Genera la factura
        public void OnPostBtFacturar()
        {
            try
            {
                var factura = this.IFacturacionPresentacion.Facturar(this.Actual!);

                ViewData["Mensaje"] = "Factura generada para el pedido " + factura.Pedido +
                    ". Total: $" + factura.Total.ToString("N0");
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
