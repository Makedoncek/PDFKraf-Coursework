using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.DTO;

public class WatermarkPdfDTO
{
   public IFormFile pdfFile { get; set; }
   public string watermarkText { get; set; }

}