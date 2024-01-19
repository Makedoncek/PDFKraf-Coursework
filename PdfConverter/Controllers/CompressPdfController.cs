using PdfConverter.BLL.Services.Interfaces;
using PdfConverter.DTO;



using Microsoft.AspNetCore.Mvc;


namespace PdfConverter.Controllers
{
    [ApiController]
    [Route("api/pdf/compress")]
    public class CompressPdfController : ControllerBase
    {
        private readonly ICompressPdfService _iCompressPdfService;

        public CompressPdfController(ICompressPdfService iCompressPdfService)
        {
            _iCompressPdfService = iCompressPdfService;
        }

        [HttpPost]
        public IActionResult CompressPdf([FromForm] CompressPdfDTO compressPdfDto)
        {
            byte[] compressedPdf = _iCompressPdfService.CompressPdf(compressPdfDto);
            return File(compressedPdf, "application/pdf", "compressed.pdf");
        }
    }
}
