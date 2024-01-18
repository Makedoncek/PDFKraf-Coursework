namespace PdfConverter.DTO;

public class CompressPdfDTO
{
   public IFormFile pdfFile { get; set; }
   public int compressionLevel { get; set; }
}