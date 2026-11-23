using front_restaurante.Nucleo;
using lib_aplicaciones.Entidades;
using lib_aplicaciones.Nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using presentacion_restaurante.Interfaces;

namespace front_restaurante.Pages.Ventanas
{
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasPresentacion IAuditoriasPresentacion;

        public AuditoriasModel(IAuditoriasPresentacion IAuditoriasPresentacion)
        {
            this.IAuditoriasPresentacion = IAuditoriasPresentacion;
            this.Filtro = new Auditorias();
        }

        public List<Auditorias>? Lista { get; set; }
        [BindProperty] public Auditorias? Filtro { get; set; }

        public void OnGet() { OnPostBtConsultar(); }

        public IActionResult OnPostBtPdf()
        {
            OnPostBtConsultar();   // vuelve a consultar respetando los filtros escritos
            var pdf = ReportePdf.Generar("Auditorias", Lista);
            return File(pdf, "application/pdf", $"Auditorias_{DateTime.Now:yyyyMMdd_HHmm}.pdf");
        }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro!.Usuario = Filtro!.Usuario ?? "";
                Filtro!.Entidad = Filtro!.Entidad ?? "";
                Lista = this.IAuditoriasPresentacion.Buscar(Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}