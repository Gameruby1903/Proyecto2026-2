using presentacion_restaurante.Interfaces;
using presentacion_restaurante.Implementaciones;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<IComunicaciones, Comunicaciones>();
builder.Services.AddScoped<ICategoriasPresentacion, CategoriasPresentacion>();
builder.Services.AddScoped<IClientesPresentacion, ClientesPresentacion>();
builder.Services.AddScoped<IComprasPresentacion, ComprasPresentacion>();
builder.Services.AddScoped<IDetalleComprasPresentacion, DetalleComprasPresentacion>();
builder.Services.AddScoped<IDetallePedidosPresentacion, DetallePedidosPresentacion>();
builder.Services.AddScoped<IEmpleadosPresentacion, EmpleadosPresentacion>();
builder.Services.AddScoped<IFacturasPresentacion, FacturasPresentacion>();
builder.Services.AddScoped<IIngredientesPresentacion, IngredientesPresentacion>();
builder.Services.AddScoped<IInventariosPresentacion, InventariosPresentacion>();
builder.Services.AddScoped<IMesasPresentacion, MesasPresentacion>();
builder.Services.AddScoped<IMetodoPagosPresentacion, MetodoPagosPresentacion>();
builder.Services.AddScoped<IPedidosPresentacion, PedidosPresentacion>();
builder.Services.AddScoped<IProductosPresentacion, ProductosPresentacion>();
builder.Services.AddScoped<IProveedoresPresentacion, ProveedoresPresentacion>();
builder.Services.AddScoped<IRecetaIngredientesPresentacion, RecetaIngredientesPresentacion>();
builder.Services.AddScoped<IResenasPresentacion, ResenasPresentacion>();
builder.Services.AddScoped<IReservasPresentacion, ReservasPresentacion>();
builder.Services.AddScoped<ISucursalesPresentacion, SucursalesPresentacion>();
builder.Services.AddScoped<ITipoEmpleadosPresentacion, TipoEmpleadosPresentacion>();
builder.Services.AddScoped<ITurnosPresentacion, TurnosPresentacion>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
