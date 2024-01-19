using iText.Layout;
using PdfConverter.DTO;

namespace PdfConverter.BLL.Services.Interfaces;

public interface IMergePdfService
{
    public byte[] MergePdfs(MergePdfDTO mergePdfDto);
}