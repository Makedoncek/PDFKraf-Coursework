using Microsoft.AspNetCore.Http;

namespace PdfConverter.BLL.Services.Interfaces;

public interface IPdfToBinaryConverter
{
    public byte[] ConvertToByteArray(IFormFile file);
}