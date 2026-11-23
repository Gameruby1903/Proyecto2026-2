using lib_aplicaciones.Entidades;

namespace presentacion_restaurante.Interfaces
{
    public interface IFacturacionPresentacion
    {
        List<Pedidos> PedidosPendientes();
        Facturas Calcular(Facturas entidad);
        Facturas Facturar(Facturas entidad);
    }
}
