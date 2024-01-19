using PdfConverter.DTO;

namespace PdfConverter.BLL.Services.Interfaces;

public interface IWatermarkPdfService
{
    public byte[] WatermarkPdf(WatermarkPdfDTO watermarkPdfDto);
}