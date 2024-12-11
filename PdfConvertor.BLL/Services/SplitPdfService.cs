using iText.Kernel.Pdf;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;

namespace PdfConvertor.BLL.Services;

public class SplitPdfService : ISplitPdfService
{
    private readonly IPdfToBinaryConverter _iPdfToBinaryConverter;

    public SplitPdfService(IPdfToBinaryConverter iPdfToBinaryConverter)
    {
        _iPdfToBinaryConverter = iPdfToBinaryConverter;
    }
    public byte[] SplitPdf(SplitPdfDto splitPdfDto)
    {
        byte[] pdfBytes = _iPdfToBinaryConverter.ConvertToByteArray(splitPdfDto.pdfFile);
        ValidatePageRange(splitPdfDto.startPage, splitPdfDto.endPage);

        MemoryStream pdfStream = new MemoryStream(pdfBytes);
        MemoryStream extractedPdfStream = new MemoryStream();
        try
        {
            using (PdfReader pdfReader = new PdfReader(pdfStream))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            using (PdfWriter writer = new PdfWriter(extractedPdfStream))
            using (PdfDocument extractedPdf = new PdfDocument(writer))
            {
                for (int pageNum = splitPdfDto.startPage; pageNum <= Math.Min(splitPdfDto.endPage, pdfDocument.GetNumberOfPages()); pageNum++)
                {
                    extractedPdf.AddPage(pdfDocument.GetPage(pageNum).CopyTo(extractedPdf));
                }
            }
            return extractedPdfStream.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting pages: {ex.Message}");
            throw;
        }
        finally
        {
            pdfStream.Close();
            extractedPdfStream.Close();
        }
    }

    private void ValidatePageRange(int startPage, int endPage)
    {
        if (startPage < 1 || endPage < startPage)
        {
            throw new ArgumentException("Invalid page parameters.");
        }
    }
    
}