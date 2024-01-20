using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.DTO;

public class SplitPdfDto
{
    public IFormFile pdfFile { get; set; }
    public int startPage { get; set; }
    public int endPage { get; set; }
}
