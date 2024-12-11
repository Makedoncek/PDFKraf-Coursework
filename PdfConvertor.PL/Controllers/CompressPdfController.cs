using Microsoft.AspNetCore.Mvc;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;


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
        public IActionResult CompressPdf([FromForm] CompressPdfDto compressPdfDto)
        {
            byte[] compressedPdf = _iCompressPdfService.CompressPdf(compressPdfDto);
            return File(compressedPdf, "application/pdf", "compressed.pdf");
        }
    }
}
