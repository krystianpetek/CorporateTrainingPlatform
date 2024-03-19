using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

namespace GarageGenius.Modules.Notifications.Core.Services;

public class PdfGeneratorService : IPdfGeneratorService
{
    public Task<byte[]> GeneratePdfAsync(string content)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(text => text.FontSize(20));

                page.Header()
                    .Text("GarageGenius")
                    .SemiBold()
                    .FontSize(30)
                    .FontColor(Colors.Blue.Medium);
                
                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);
                        x.Item().Text("Generated reservation");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        }).GeneratePdf();

        return Task.FromResult(document);
    }
}