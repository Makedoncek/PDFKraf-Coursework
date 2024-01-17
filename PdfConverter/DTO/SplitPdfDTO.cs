namespace PdfConverter.DTO;

public class SplitPdfDto
{
    public IFormFile PdfFile { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
}
