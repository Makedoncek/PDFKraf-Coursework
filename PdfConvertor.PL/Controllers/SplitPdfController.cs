using Microsoft.AspNetCore.Mvc;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;


namespace PdfConverter.Controllers
{
    [ApiController]
    [Route("api/pdf/split")]
    public class SplitPdfController : ControllerBase
    {
        private readonly ISplitPdfService _iSplitPdfService;

        public SplitPdfController(ISplitPdfService iSplitPdfService)
        {
            _iSplitPdfService = iSplitPdfService;
        }

        [HttpPost]
        public IActionResult SplitPdf([FromForm] SplitPdfDto splitPdfDto)
        {
            byte[] extractedPdf = _iSplitPdfService.SplitPdf(splitPdfDto);
            return File(extractedPdf, "application/pdf", "splited.pdf");
        }
    }
}
