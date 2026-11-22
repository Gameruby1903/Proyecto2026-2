using lib_aplicaciones.Entidades;
using lib_aplicaciones.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicios_restaurante.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsuariosController : ControllerBase
    {
        private IUsuariosAplicacion IUsuariosAplicacion;

        public UsuariosController(IUsuariosAplicacion IUsuariosAplicacion)
        {
            this.IUsuariosAplicacion = IUsuariosAplicacion;
        }

        [HttpPost]
        public List<Usuarios> Consultar()
        {
            return this.IUsuariosAplicacion.Consultar();
        }

        [HttpPost]
        public List<Usuarios> Buscar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Buscar(entidad);
        }

        [HttpPost]
        public Usuarios Insertar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Insertar(entidad);
        }

        [HttpPost]
        public Usuarios Actualizar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Actualizar(entidad);
        }

        [HttpPost]
        public Usuarios Borrar(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.Borrar(entidad);
        }

        [HttpPost]
        public Usuarios? IniciarSesion(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.IniciarSesion(entidad);
        }
        [HttpPost]
        public List<string> ConsultarPermisos(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.ConsultarPermisos(entidad);
        }
        [HttpPost]
        public List<Usuarios> ConsultarPorRol(Usuarios entidad)
        {
            return this.IUsuariosAplicacion.ConsultarPorRol(entidad);
        }
    }
}