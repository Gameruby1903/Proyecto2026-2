//carpeta creada con esta clase para poder validar el inicio de sesion para si poder utilizar cada rol con sus permisos

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace front_restaurante.Nucleo
{
    public class ValidarSesion : IPageFilter
    {
        public void OnPageHandlerSelected(
            PageHandlerSelectedContext context)
        {
        }

        public void OnPageHandlerExecuting(
            PageHandlerExecutingContext context)
        {
            var pagina = context.HttpContext.Request.Path.Value;

            // Permitir el acceso al Login
            if (pagina == "/" || pagina == "/Index")
                return;

            var usuario = context.HttpContext.Session.GetString("Usuario");

            if (string.IsNullOrEmpty(usuario))
            {
                context.Result = new RedirectToPageResult("/Index");
            }
        }

        public void OnPageHandlerExecuted(
            PageHandlerExecutedContext context)
        {
        }
    }
}
