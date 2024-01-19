using PdfConverter.DTO;

namespace PdfConverter.BLL.Services.Interfaces;

public interface ISplitPdfService
{
    public byte[] SplitPdf(SplitPdfDto splitPdfDto);
}