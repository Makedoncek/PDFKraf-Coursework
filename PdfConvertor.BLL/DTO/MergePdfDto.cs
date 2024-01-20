using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.DTO;

public class MergePdfDto
{
    public List<IFormFile> pdfFiles{ get; set; }
}