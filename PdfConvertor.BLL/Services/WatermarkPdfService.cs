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
            float watermarkWidth = width / 1.3f;
            float xOffSet = 250;
            float yOffSet = -300;
            float x = (width - watermarkWidth) / 2 + xOffSet;
            float y = height / 2 + yOffSet;

            
            Paragraph watermark = new Paragraph(watermarkText)
                .SetFontColor(iText.Kernel.Colors.ColorConstants.LIGHT_GRAY, 0.2f) 
                .SetFontSize(75)
                .SetRotationAngle(Math.PI / 4);

            
            Canvas canvas = new Canvas(page, pageSize);
            canvas.Add(watermark.SetFixedPosition(x, y, watermarkWidth));
        }
    }
}