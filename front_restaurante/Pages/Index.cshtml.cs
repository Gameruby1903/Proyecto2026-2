
using lib_aplicaciones.Entidades;
using lib_aplicaciones.Nucleo;
using presentacion_restaurante.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace front_restaurante.Pages
{
    public class IndexModel : PageModel
    {
        private IUsuariosPresentacion IUsuariosPresentacion;

        public bool EstaLogueado = false;

        [BindProperty]
        public string? NombreUsuario { get; set; }

        [BindProperty]
        public string? Clave { get; set; }

        public string? Mensaje { get; set; }

        public IndexModel(IUsuariosPresentacion IUsuariosPresentacion)
        {
            this.IUsuariosPresentacion = IUsuariosPresentacion;
        }

        public void OnGet()
        {
            var variable_session =
                HttpContext.Session.GetString("Usuario");

            if (!String.IsNullOrEmpty(variable_session))
            {
                EstaLogueado = true;
                return;
            }
        }

        public void OnPostBtClean()
        {
            try
            {
                NombreUsuario = string.Empty;
                Clave = string.Empty;
                Mensaje = string.Empty;
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public IActionResult OnPostBtEnter()
        {
            try
            {
                if (string.IsNullOrEmpty(NombreUsuario) ||
                    string.IsNullOrEmpty(Clave))
                {
                    Mensaje = "Ingrese el usuario y la clave";
                    return Page();
                }

                var entidad = new Usuarios
                {
                    NombreUsuario = NombreUsuario,
                    Clave = Clave
                };

                var usuario =
                    this.IUsuariosPresentacion.IniciarSesion(entidad);

                if (usuario == null)
                {
                    Mensaje = "Usuario o clave incorrectos, o usuario inactivo";
                    return Page();
                }

                HttpContext.Session.SetString(
                    "Usuario", usuario.NombreUsuario!);

                HttpContext.Session.SetInt32(
                    "UsuarioId", usuario.Id);

                HttpContext.Session.SetInt32(
                    "RolId", usuario.Rol);

                var permisos = IUsuariosPresentacion.ConsultarPermisos(
                    new Usuarios { Id = usuario.Id }
                );

                HttpContext.Session.SetString(
                    "Permisos",
                    Newtonsoft.Json.JsonConvert.SerializeObject(
                        permisos ?? new List<string>()
                    )
                );

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Mensaje = "Ocurrió un error al iniciar sesión";
                return Page();
            }
        }

        public IActionResult OnPostBtClose()
        {
            try
            {
                HttpContext.Session.Clear();

                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
                Mensaje = "No se pudo cerrar la sesión";
                return Page();
            }
        }
    }
}
