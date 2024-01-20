using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;

namespace PdfConvertor.BLL.Services;

public class WatermarkPdfService : IWatermarkPdfService
{
    private readonly IPdfToBinaryConverter _iPdfToBinaryConverter;

    public WatermarkPdfService(IPdfToBinaryConverter iPdfToBinaryConverter)
    {
        _iPdfToBinaryConverter = iPdfToBinaryConverter;
    }
     public byte[] WatermarkPdf(WatermarkPdfDto watermarkPdfDto)
    {
        byte[] pdfBytes = _iPdfToBinaryConverter.ConvertToByteArray(watermarkPdfDto.pdfFile);
        MemoryStream inputStream = new MemoryStream(pdfBytes);
        MemoryStream outputStream = new MemoryStream();
        try
        {
            using (PdfReader pdfReader = new PdfReader(inputStream))
            using (PdfWriter pdfWriter = new PdfWriter(outputStream))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
            {
                AddWatermarkToPdf(pdfDocument, watermarkPdfDto.watermarkText);
            }
            return outputStream.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while trying to add watermark: {ex.Message}");
            throw;
        }
        finally
        {
            inputStream.Close();
            outputStream.Close();
        }
    }

    private void AddWatermarkToPdf(PdfDocument pdfDocument, string watermarkText)
    {
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            PdfPage page = pdfDocument.GetPage(i);
            iText.Kernel.Geom.Rectangle pageSize = page.GetPageSize();

            // Настройка параметров водяного знака
            float width = pageSize.GetWidth();
            float height = pageSize.GetHeight();
            float watermarkWidth = width / 2;
            float xOffSet = 100;
            float yOffSet = -110;
            float x = (width - watermarkWidth) / 2 + xOffSet;
            float y = height / 2 + yOffSet;

            // Создание водяного знака
            Paragraph watermark = new Paragraph(watermarkText)
                .SetFontColor(iText.Kernel.Colors.ColorConstants.RED, 0.2f) // 20% прозрачность
                .SetFontSize(100)
                .SetRotationAngle(Math.PI / 4);

            // Добавление водяного знака на страницу
            Canvas canvas = new Canvas(page, pageSize);
            canvas.Add(watermark.SetFixedPosition(x, y, watermarkWidth));
        }
    }
}