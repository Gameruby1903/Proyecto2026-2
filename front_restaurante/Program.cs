
using front_restaurante.Nucleo;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using presentacion_restaurante.Implementaciones;
using presentacion_restaurante.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Validar el inicio de sesión
builder.Services.AddRazorPages(options =>
{
    options.Conventions.ConfigureFilter(
        new ValidarSesion()
    );
});

// Registrar las comunicaciones
builder.Services.AddScoped<IComunicaciones, Comunicaciones>();

// Registrar presentaciones
builder.Services.AddScoped<IAuditoriasPresentacion, AuditoriasPresentacion>();
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
builder.Services.AddScoped<IUsuariosPresentacion, UsuariosPresentacion>();
builder.Services.AddScoped<IRolesPresentacion, RolesPresentacion>();
builder.Services.AddScoped<IPermisosPresentacion, PermisosPresentacion>();
builder.Services.AddScoped<IRolPermisosPresentacion, RolPermisosPresentacion>();

builder.Services.AddEndpointsApiExplorer();

// Configuración de sesiones
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// La sesión debe ir antes de Razor Pages
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
