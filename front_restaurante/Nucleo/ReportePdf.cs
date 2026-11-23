using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace front_restaurante.Nucleo
{
    public static class ReportePdf
    {
        // Columnas que nunca se imprimen
        private static readonly HashSet<string> Excluidas = new() { "Imagen", "Clave" };

        public static byte[] Generar<T>(string titulo, IEnumerable<T>? lista)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var datos = (lista ?? Enumerable.Empty<T>()).ToList();

            // Solo columnas simples (texto, números, fechas, bool); se saltan objetos y listas
            var columnas = typeof(T).GetProperties()
                .Where(p => !Excluidas.Contains(p.Name) && EsSimple(p.PropertyType))
                .ToList();

            var tamano = columnas.Count > 9 ? 7 : 9;   // letra más pequeña si hay muchas columnas

            return Document.Create(documento =>
            {
                documento.Page(pagina =>
                {
                    pagina.Size(PageSizes.A4.Landscape());
                    pagina.Margin(30);
                    pagina.DefaultTextStyle(x => x.FontSize(tamano));

                    pagina.Header().Column(col =>
                    {
                        col.Item().Text($"Reporte de {titulo}").FontSize(18).Bold();
                        col.Item().Text($"Generado: {DateTime.Now:dd/MM/yyyy HH:mm}");
                        col.Item().Text($"Total de registros: {datos.Count}");
                    });

                    if (datos.Count == 0)
                    {
                        pagina.Content().PaddingVertical(10).Text("No hay registros para mostrar.");
                    }
                    else
                    {
                        pagina.Content().PaddingVertical(10).Table(tabla =>
                        {
                            tabla.ColumnsDefinition(c =>
                            {
                                foreach (var p in columnas)
                                {
                                    if (p.Name == "Id") c.ConstantColumn(30);
                                    else c.RelativeColumn();
                                }
                            });

                            tabla.Header(h =>
                            {
                                foreach (var p in columnas)
                                    h.Cell().Element(Encabezado).Text(p.Name).Bold();
                            });

                            foreach (var item in datos)
                            {
                                foreach (var p in columnas)
                                    tabla.Cell().Element(Celda).Text(Formato(p.GetValue(item)));
                            }
                        });
                    }

                    pagina.Footer().AlignCenter().Text(t =>
                    {
                        t.Span("Página ");
                        t.CurrentPageNumber();
                        t.Span(" de ");
                        t.TotalPages();
                    });
                });
            }).GeneratePdf();
        }

        private static bool EsSimple(Type tipo)
        {
            var t = Nullable.GetUnderlyingType(tipo) ?? tipo;
            return t.IsPrimitive || t.IsEnum
                || t == typeof(string) || t == typeof(decimal) || t == typeof(DateTime);
        }

        private static string Formato(object? valor)
        {
            return valor switch
            {
                null => "",
                bool b => b ? "Sí" : "No",
                DateTime f => f.TimeOfDay == TimeSpan.Zero
                    ? f.ToString("dd/MM/yyyy")
                    : f.ToString("dd/MM/yyyy HH:mm"),
                decimal d => d.ToString("N2"),
                _ => valor.ToString() ?? ""
            };
        }

        private static IContainer Encabezado(IContainer c) =>
            c.Background(Colors.Grey.Lighten2).Padding(4);

        private static IContainer Celda(IContainer c) =>
            c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(4);
    }
}