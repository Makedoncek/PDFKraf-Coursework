using PdfConvertor.BLL.DTO;

namespace PdfConvertor.BLL.Services.Interfaces;

public interface ISplitPdfService
{
    public byte[] SplitPdf(SplitPdfDto splitPdfDto);
}