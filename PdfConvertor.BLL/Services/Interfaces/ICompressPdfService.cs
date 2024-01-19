using PdfConverter.DTO;

namespace PdfConverter.BLL.Services.Interfaces;

public interface ICompressPdfService
{
    public byte[] CompressPdf(CompressPdfDTO compressPdfDto);
    
}