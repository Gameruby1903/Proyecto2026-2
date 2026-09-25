using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using lib_aplicaciones.Entidades;

namespace lib_aplicaciones.Interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<TipoEmpleados>? TipoEmpleados { get; set; }
        
    }
}

