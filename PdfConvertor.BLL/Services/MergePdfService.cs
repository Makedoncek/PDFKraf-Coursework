using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;
using PdfDocument = iText.Kernel.Pdf.PdfDocument;

namespace PdfConvertor.BLL.Services;

public class MergePdfService : IMergePdfService
{
    private readonly IPdfToBinaryConverter _iPdfToBinaryConverter;

    public MergePdfService(IPdfToBinaryConverter iPdfToBinaryConverter)
    {
        _iPdfToBinaryConverter = iPdfToBinaryConverter;
    }
    
    public byte[] MergePdfs(MergePdfDto mergePdfDto)
    {
        var pdfs = mergePdfDto.pdfFiles.Select(file => _iPdfToBinaryConverter.ConvertToByteArray(file)).ToList();
        MemoryStream mergedPdfStream = new MemoryStream();
        using (PdfWriter writer = new PdfWriter(mergedPdfStream))
        using (PdfDocument mergedPdf = new PdfDocument(writer))
        using (Document document = new Document(mergedPdf))
        {
            foreach (var pdfBytes in pdfs)
            {
                MergeSinglePdf(document, pdfBytes);
            }
        }

        return mergedPdfStream.ToArray();
    }
    private void MergeSinglePdf(Document document, byte[] pdfBytes)
    {
        MemoryStream pdfStream = new MemoryStream(pdfBytes);
        using (PdfDocument sourcePdf = new PdfDocument(new PdfReader(pdfStream)))
        {
            for (int pageNum = 1; pageNum <= sourcePdf.GetNumberOfPages(); pageNum++)
            {
                PdfPage page = sourcePdf.GetPage(pageNum);
                PdfFormXObject formXObject = page.CopyAsFormXObject(document.GetPdfDocument());
                document.Add(new iText.Layout.Element.Image(formXObject));
            }
        }
    }

}