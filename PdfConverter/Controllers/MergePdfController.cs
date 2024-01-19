using PdfConverter.BLL.Services.Interfaces;
using PdfConverter.DTO;
using PdfConverter.Service;


using Microsoft.AspNetCore.Mvc;


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
