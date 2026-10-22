using System;
using System.Collections.Generic;
using System.Text;

namespace presentacion_restaurante.Interfaces
{
    public interface IComunicaciones
    {
        Task<T> Ejecutar<T>(string url, object entidad);
    }
}
