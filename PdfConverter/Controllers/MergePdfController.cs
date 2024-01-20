using Microsoft.AspNetCore.Mvc;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;


namespace PdfConverter.Controllers
{
    [ApiController]
    [Route("api/pdf/merge")]
    public class MergePdfController : ControllerBase
    {
        private readonly IMergePdfService _iMergePdfService;

        public MergePdfController(IMergePdfService iMergePdfService)
        {
            _iMergePdfService = iMergePdfService;
        }

        [HttpPost]
        public IActionResult MergePdfs([FromForm]MergePdfDTO mergePdfDto)
        {
            byte[] mergedPdf = _iMergePdfService.MergePdfs(mergePdfDto);
            return File(mergedPdf, "application/pdf", "merged.pdf");
        }
    }
}
