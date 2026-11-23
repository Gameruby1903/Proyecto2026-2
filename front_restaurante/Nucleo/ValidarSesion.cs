//carpeta creada con esta clase para poder validar el inicio de sesion para si poder utilizar cada rol con sus permisos

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using presentacion_restaurante.Interfaces;

namespace front_restaurante.Nucleo
{
    public class ValidarSesion : IPageFilter
    {
        public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            var pagina = context.HttpContext.Request.Path.Value;
            if (pagina == "/" || pagina == "/Index") return;

            var usuario = context.HttpContext.Session.GetString("Usuario");
            if (string.IsNullOrEmpty(usuario))
            {
                context.Result = new RedirectToPageResult("/Index");
                return;
            }


            // Pasar el usuario a Comunicaciones (para enviarlo al API)

            // Entrega el usuario logueado a Comunicaciones (misma instancia scoped de la petición)

            var comunicaciones = context.HttpContext.RequestServices
                .GetRequiredService<IComunicaciones>();
            comunicaciones.Usuario = usuario;

            // Validar permiso según la pagina y el handler
            var permiso = ObtenerPermiso(context);
            if (permiso != null && !TienePermiso(context.HttpContext, permiso))
            {
                context.Result = new RedirectToPageResult("/Index"); // o una página "Sin acceso"
            }
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }

        private static string? ObtenerPermiso(PageHandlerExecutingContext ctx)
        {
            var entidad = ctx.ActionDescriptor.RelativePath; // ej. "/Pages/Clientes.cshtml"
            var nombre = Path.GetFileNameWithoutExtension(entidad);
            var handler = ctx.HandlerMethod?.Name ?? "";

            string? accion = handler switch
            {
                var h when h.Contains("Guardar") || h.Contains("Nuevo") => "Crear",
                var h when h.Contains("Modificar") || h.Contains("Actualizar") => "Actualizar",
                var h when h.Contains("Borrar") || h.Contains("Eliminar") => "Eliminar",
                _ => null
            };
            return accion == null ? null : $"{nombre}.{accion}";
        }

        private static bool TienePermiso(HttpContext ctx, string permiso)
        {
            var json = ctx.Session.GetString("Permisos");
            if (string.IsNullOrEmpty(json)) return false;
            var lista = Newtonsoft.Json.JsonConvert
                .DeserializeObject<List<string>>(json) ?? new();
            return lista.Contains(permiso);
        }
    }
}
