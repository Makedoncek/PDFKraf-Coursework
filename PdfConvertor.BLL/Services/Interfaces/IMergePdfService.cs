using PdfConvertor.BLL.DTO;

namespace PdfConvertor.BLL.Services.Interfaces;

public interface IMergePdfService
{
    public byte[] MergePdfs(MergePdfDTO mergePdfDto);
}