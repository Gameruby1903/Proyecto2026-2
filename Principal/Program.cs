using lib_aplicaciones.Entidades;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

IConexion conexion = new Conexion();
conexion.StringConexion = "server=localhost;database=Restaurante;Integrated Security=True;TrustServerCertificate=true;";




var lista = conexion.TipoEmpleados!.ToList();




foreach (var tipo in lista)
{
    
    Console.WriteLine($"ID: {tipo.Id} - Descripción: {tipo.Nombre}");
}

