using PdfConvertor.BLL.DTO;

namespace PdfConvertor.BLL.Services.Interfaces;

public interface ICompressPdfService
{
    public byte[] CompressPdf(CompressPdfDto compressPdfDto);
    
}