using System;
using System.Collections.Generic;
using System.Text;

namespace lib_aplicaciones.Entidades
{
    public class TipoEmpleados
    {
        public int Id { get; set; }
        public string? nombre { get; set; }
        public decimal SalarioBase { get; set; }

     
        public List<Empleados>? Empleados { get; set; } = new List<Empleados>();
        /*En resumen, la segunda opción (`= new List<T>()`) evita **4 problemas principales**:
1. **Evita el error `NullReferenceException`:** El programa jamás se romperá por intentar leer o modificar una lista inexistente.
2. **Evita verificaciones de código innecesarias:** Te ahorra tener que escribir bloques de `if (lista != null)` antes de hacer un `foreach`, un `.Add()`, o un `.Count()`.
3. **Evita fallos al agregar relaciones en el ORM:** Puedes añadir elementos directamente a la colección (`objeto.Lista.Add(nuevo)`) sin tener que crear o instanciar la lista primero.*/
    }
}
