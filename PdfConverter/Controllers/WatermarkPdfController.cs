using PdfConverter.BLL.Services.Interfaces;
using PdfConverter.DTO;
using PdfConverter.Service;



using Microsoft.AspNetCore.Mvc;


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
