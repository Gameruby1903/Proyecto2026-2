using System;
using System.Collections.Generic;
using System.Text;

namespace presentacion_restaurante.Interfaces
{
    public interface IComunicaciones
    {
        string? Usuario { get; set; }
        Task<T> Ejecutar<T>(string url, object entidad);
    }
}
