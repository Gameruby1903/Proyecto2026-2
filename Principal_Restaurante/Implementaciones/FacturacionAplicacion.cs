using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.Implementaciones
{
    // Aplicacion de negocio: facturar un pedido.
    // Calcula el subtotal con los detalles del pedido, aplica el IVA,
    // crea la factura y marca el pedido como "Facturado".
    public class FacturacionAplicacion : IFacturacionAplicacion
    {
        private const decimal IVA = 0.19m;

        private IConexion IConexion;

        public FacturacionAplicacion(IConexion IConexion)
        {
            this.IConexion = IConexion;
        }

        // Pedidos que todavia no tienen factura
        public List<Pedidos> PedidosPendientes()
        {
            var facturados = this.IConexion.Facturas!
                .Select(x => x.Pedido)
                .ToList();

            return this.IConexion.Pedidos!
                .Include(x => x._Cliente)
                .Include(x => x._Mesa)
                .Include(x => x._Empleado)
                .Where(x => !facturados.Contains(x.Id))
                .OrderBy(x => x.Fecha)
                .ToList();
        }

        // Calcula los valores de la factura SIN guardarla (vista previa)
        public Facturas Calcular(Facturas entidad)
        {
            if (entidad.Pedido <= 0)
                throw new Exception("Seleccione un pedido");

            if (!this.IConexion.Pedidos!.Any(x => x.Id == entidad.Pedido))
                throw new Exception("El pedido no existe");

            if (this.IConexion.Facturas!.Any(x => x.Pedido == entidad.Pedido))
                throw new Exception("El pedido ya fue facturado");

            var detalles = this.IConexion.DetallePedidos!
                .Where(x => x.Pedido == entidad.Pedido)
                .ToList();

            if (detalles.Count == 0)
                throw new Exception("El pedido no tiene productos, no se puede facturar");

            if (detalles.Any(x => x.Cantidad <= 0 || x.PrecioUni <= 0))
                throw new Exception("El pedido tiene productos con cantidad o precio invalido");

            var subtotal = detalles.Sum(x => x.Cantidad * x.PrecioUni);
            var impuesto = Math.Round(subtotal * IVA, 2);

            return new Facturas
            {
                Pedido = entidad.Pedido,
                MetodoPago = entidad.MetodoPago,
                FechaEmi = DateTime.Now,
                Subtotal = subtotal,
                Impuesto = impuesto,
                Total = subtotal + impuesto
            };
        }

        // Crea la factura y cambia el estado del pedido
        public Facturas Facturar(Facturas entidad)
        {
            if (entidad.MetodoPago <= 0)
                throw new Exception("Seleccione un metodo de pago");

            if (!this.IConexion.MetodoPagos!.Any(x => x.Id == entidad.MetodoPago))
                throw new Exception("El metodo de pago no existe");

            var factura = Calcular(entidad);

            var pedido = this.IConexion.Pedidos!
                .First(x => x.Id == entidad.Pedido);
            pedido.Estado = "Facturado";

            this.IConexion.Facturas!.Add(factura);
            this.IConexion.Entry(pedido).State = EntityState.Modified;

            // Un solo SaveChanges: se guardan la factura y el pedido juntos, o ninguno
            this.IConexion.SaveChanges();

            return factura;
        }
    }
}
