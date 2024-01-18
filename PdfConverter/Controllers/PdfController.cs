using Microsoft.AspNetCore.Mvc;
using PdfConverter.DTO;
using PdfConverter.Service;
namespace PdfConverter.Controllers;

    /// <summary>
    /// Controller for handling PDF-related actions.
    /// </summary>
    [ApiController]
    [Route("api/pdf")]
    public class PdfController : ControllerBase
    {
        private readonly PdfManipulationService _pdfManipulationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PdfController"/> class.
        /// </summary>
        /// <param name="pdfManipulationService">Service for manipulating PDFs.</param>
        public PdfController(PdfManipulationService pdfManipulationService)
        {
            _pdfManipulationService = pdfManipulationService;
        }
        /// <summary>
        /// Merges multiple PDFs into a single PDF.
        /// </summary>
        /// <param name="pdfFiles">List of PDF files to be merged.</param>
        /// <returns>Action result containing the merged PDF.</returns>
        [HttpPost("merge")]
        public IActionResult MergePdfs([FromForm]MergePdfDTO mergePdfDto)
        {
             var pdfs = mergePdfDto.pdfFiles.Select(file => _pdfManipulationService.ConvertToByteArray(file)).ToList();
             byte[] mergedPdf = _pdfManipulationService.MergePdfs(pdfs);
             return  File(mergedPdf, "application/pdf", "merged.pdf");
        }

        
        /// <summary>
        /// Adds a watermark to a PDF file.
        /// </summary>
        /// <param name="pdfFile">Input PDF file.</param>
        /// <param name="watermarkText">Text for the watermark.</param>
        /// <returns>Action result containing the watermarked PDF.</returns>
        [HttpPost("watermark")]
        public IActionResult WatermarkPdf([FromForm] WatermarkPdfDTO watermarkPdfDto)
        {
            byte[] pdfBytes = _pdfManipulationService.ConvertToByteArray(watermarkPdfDto.pdfFile);
            byte[] watermarkedPdf = _pdfManipulationService.WatermarkPdf(pdfBytes, watermarkPdfDto.watermarkText);
            return File(watermarkedPdf, "application/pdf", "watermarked.pdf");
        }

        
        
        /// <summary>
        /// Compresses a PDF file at the specified compression level.
        /// </summary>
        /// <param name="pdfFile">Input PDF file.</param>
        /// <param name="compressionLevel">Compression level (from 0-9) 9 high compression 0 without.</param>
        /// <returns>Action result containing the compressed PDF.</returns>
        [HttpPost("compress")]
        public IActionResult CompressPdf([FromForm] CompressPdfDTO compressPdfDto)
        {
            byte[] pdfBytes = _pdfManipulationService.ConvertToByteArray(compressPdfDto.pdfFile);
            byte[] compressedPdf = _pdfManipulationService.CompressPdf(pdfBytes, compressPdfDto.compressionLevel);
            return File(compressedPdf, "application/pdf", "compressed.pdf");
        }
        /// <summary>
        /// Extracts pages from a PDF file.
        /// </summary>
        /// <param name="pdfFile">Input PDF file.</param>
        /// <param name="startPage">Starting page number.</param>
        /// <param name="endPage">Ending page number.</param>
        /// <returns>Action result containing the extracted PDF.</returns>
        [HttpPost("split")]
        public IActionResult SplitPdf([FromForm] SplitPdfDto splitPdfDto)
        {
            Console.WriteLine();
            byte[] pdfBytes = _pdfManipulationService.ConvertToByteArray(splitPdfDto.pdfFile);
            byte[] extractedPdf = _pdfManipulationService.SplitPdf(pdfBytes, splitPdfDto.startPage, splitPdfDto.endPage);

            return File(extractedPdf, "application/pdf", "extracted.pdf");  
        }
        [HttpGet("test")]
        public ActionResult<int> Get()
        {
            Console.WriteLine();
            return 1;
        }
    }
