using PdfConverter.BLL.Services.Interfaces;
using PdfConverter.DTO;



using Microsoft.AspNetCore.Mvc;


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
