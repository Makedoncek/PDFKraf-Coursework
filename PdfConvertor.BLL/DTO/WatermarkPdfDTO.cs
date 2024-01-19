using Microsoft.AspNetCore.Http;

namespace PdfConverter.DTO;

public class WatermarkPdfDTO
{
   public IFormFile pdfFile { get; set; }
   public string watermarkText { get; set; }

}