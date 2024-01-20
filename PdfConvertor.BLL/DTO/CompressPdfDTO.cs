using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.DTO;

public class CompressPdfDTO
{
   public IFormFile pdfFile { get; set; }
   public int compressionLevel { get; set; }
}