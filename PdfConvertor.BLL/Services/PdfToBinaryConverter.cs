using Microsoft.AspNetCore.Http;
using PdfConvertor.BLL.Services.Interfaces;

namespace PdfConvertor.BLL.Services;

public class PdfToBinaryConverter : IPdfToBinaryConverter
{
    public byte[] ConvertToByteArray(IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            return stream.ToArray();
        }
    }

   
}