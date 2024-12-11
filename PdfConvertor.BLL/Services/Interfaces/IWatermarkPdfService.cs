using PdfConvertor.BLL.DTO;

namespace PdfConvertor.BLL.Services.Interfaces;

public interface IWatermarkPdfService
{
    public byte[] WatermarkPdf(WatermarkPdfDto watermarkPdfDto);
}