using Microsoft.AspNetCore.Mvc;
using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;


namespace PdfConverter.Controllers
{
    [ApiController]
    [Route("api/pdf/watermark")]
    public class WatermarkPdfController : ControllerBase
    {
        private readonly IWatermarkPdfService _iWatermarkPdfService;

        public WatermarkPdfController(IWatermarkPdfService iWatermarkPdfService)
        {
            _iWatermarkPdfService = iWatermarkPdfService;
        }

        [HttpPost]
        public IActionResult WatermarkPdf([FromForm] WatermarkPdfDTO watermarkPdfDto)
        {
            byte[] watermarkedPdf = _iWatermarkPdfService.WatermarkPdf(watermarkPdfDto);
            return File(watermarkedPdf, "application/pdf", "watermarked.pdf");
        }
    }
}
