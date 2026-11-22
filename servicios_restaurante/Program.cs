
using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped<IAuditoriasAplicacion, AuditoriasAplicacion>();
builder.Services.AddScoped<ICategoriasAplicacion, CategoriasAplicacion>();
builder.Services.AddScoped<IClientesAplicacion, ClientesAplicacion>();
builder.Services.AddScoped<IComprasAplicacion, ComprasAplicacion>();
builder.Services.AddScoped<IDetalleComprasAplicacion, DetalleComprasAplicacion>();
builder.Services.AddScoped<IDetallePedidosAplicacion, DetallePedidosAplicacion>();
builder.Services.AddScoped<IEmpleadosAplicacion, EmpleadosAplicacion>();
builder.Services.AddScoped<IFacturasAplicacion, FacturasAplicacion>();
builder.Services.AddScoped<IIngredientesAplicacion, IngredientesAplicacion>();
builder.Services.AddScoped<IInventariosAplicacion, InventariosAplicacion>();
builder.Services.AddScoped<IMesasAplicacion, MesasAplicacion>();
builder.Services.AddScoped<IMetodoPagosAplicacion, MetodoPagosAplicacion>();
builder.Services.AddScoped<IPedidosAplicacion, PedidosAplicacion>();
builder.Services.AddScoped<IProductosAplicacion, ProductosAplicacion>();
builder.Services.AddScoped<IProveedoresAplicacion, ProveedoresAplicacion>();
builder.Services.AddScoped<IRecetaIngredientesAplicacion, RecetaIngredientesAplicacion>();
builder.Services.AddScoped<IResenasAplicacion, ResenasAplicacion>();
builder.Services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
builder.Services.AddScoped<ISucursalesAplicacion, SucursalesAplicacion>();
builder.Services.AddScoped<ITipoEmpleadosAplicacion, TipoEmpleadosAplicacion>();
builder.Services.AddScoped<ITurnosAplicacion, TurnosAplicacion>();
builder.Services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
builder.Services.AddScoped<IPermisosAplicacion, PermisosAplicacion>();
builder.Services.AddScoped<IRolesAplicacion, RolesAplicacion>();
builder.Services.AddScoped<IRolPermisosAplicacion, RolPermisosAplicacion>();



var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.Use(async (ctx, next) =>
{
    var conexion = ctx.RequestServices.GetRequiredService<IConexion>();
    var u = ctx.Request.Headers["Usuario"].FirstOrDefault();
    conexion.Usuario = string.IsNullOrEmpty(u) ? "desconocido" : Uri.UnescapeDataString(u);
    await next();
});
app.MapControllers();
app.MapGet("/", () => "");
app.Run();