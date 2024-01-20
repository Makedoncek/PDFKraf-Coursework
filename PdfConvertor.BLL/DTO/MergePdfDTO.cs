using Microsoft.AspNetCore.Http;

namespace PdfConvertor.BLL.DTO;

public class MergePdfDTO
{
    public List<IFormFile> pdfFiles{ get; set; }
}