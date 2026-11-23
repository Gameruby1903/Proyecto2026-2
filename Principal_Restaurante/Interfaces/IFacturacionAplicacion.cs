using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IFacturacionAplicacion
    {
        List<Pedidos> PedidosPendientes();
        Facturas Calcular(Facturas entidad);
        Facturas Facturar(Facturas entidad);
    }
}
