using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace servicios_restaurante.Nucleo
{
    public class PermisoAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _permiso;
        public PermisoAttribute(string permiso) { _permiso = permiso; }

        public Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue("Usuario", out var valor)
                || string.IsNullOrEmpty(valor))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
                return Task.CompletedTask;
            }

            var nombre = Uri.UnescapeDataString(valor.ToString());

            var usuarios = context.HttpContext.RequestServices
                .GetRequiredService<IUsuariosAplicacion>();

            var permisos = usuarios.ConsultarPermisosPorNombre(nombre);

            if (permisos == null || !permisos.Contains(_permiso))
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);

            return Task.CompletedTask;
        }
    }
}