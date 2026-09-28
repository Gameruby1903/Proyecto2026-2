using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;

try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = "server=localhost;database=Restaurante;Integrated Security=True;TrustServerCertificate=true;";
    var lista = conexion.TipoEmpleados!.ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("csl_presentacion");