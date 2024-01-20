using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.Services.Interfaces;

public interface IPdfToBinaryConverter
{
    public byte[] ConvertToByteArray(IFormFile file);
}