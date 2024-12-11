using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.DTO;

public class WatermarkPdfDto
{
   public IFormFile pdfFile { get; set; }
   public string watermarkText { get; set; }

}