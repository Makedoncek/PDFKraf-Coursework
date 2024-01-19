using System.IO.Compression;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using PdfConverter.DTO;

namespace PdfConverter.Service;
/// <summary>
/// Service for manipulating PDF files.
/// </summary>
public class PdfManipulationService
{
    /// <summary>
    /// Merges multiple PDFs into a single PDF.
    /// </summary>
    /// <param name="pdfs">List of PDF byte arrays to be merged.</param>
    /// <returns>Merged PDF byte array.</returns>
    

    public byte[] MergePdfs(MergePdfDTO mergePdfDto)
    {
        var pdfs = mergePdfDto.pdfFiles.Select(file => ConvertToByteArray(file)).ToList();
        MemoryStream mergedPdfStream = new MemoryStream();
        using (PdfWriter writer = new PdfWriter(mergedPdfStream))
        using (PdfDocument mergedPdf = new PdfDocument(writer))
        using (Document document = new Document(mergedPdf))
        {
            foreach (var pdfBytes in pdfs)
            {
                MergeSinglePdf(document, pdfBytes);
            }
        }

        return mergedPdfStream.ToArray();
    }
    private void MergeSinglePdf(Document document, byte[] pdfBytes)
    {
        MemoryStream pdfStream = new MemoryStream(pdfBytes);
        using (PdfDocument sourcePdf = new PdfDocument(new PdfReader(pdfStream)))
        {
            for (int pageNum = 1; pageNum <= sourcePdf.GetNumberOfPages(); pageNum++)
            {
                PdfPage page = sourcePdf.GetPage(pageNum);
                PdfFormXObject formXObject = page.CopyAsFormXObject(document.GetPdfDocument());
                document.Add(new Image(formXObject));
            }
        }
    }

    
    /// <summary>
    /// Adds a watermark to each page of a PDF.
    /// </summary>
    /// <param name="pdfBytes">Input PDF byte array.</param>
    /// <param name="watermarkText">Text for the watermark.</param>
    /// <returns>PDF byte array with watermark added.</returns>
    public byte[] WatermarkPdf(WatermarkPdfDTO watermarkPdfDto)
    {
        byte[] pdfBytes = ConvertToByteArray(watermarkPdfDto.pdfFile);
        MemoryStream inputStream = new MemoryStream(pdfBytes);
        MemoryStream outputStream = new MemoryStream();
        try
        {
            using (PdfReader pdfReader = new PdfReader(inputStream))
            using (PdfWriter pdfWriter = new PdfWriter(outputStream))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
            {
                AddWatermarkToPdf(pdfDocument, watermarkPdfDto.watermarkText);
            }
            return outputStream.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while trying to add watermark: {ex.Message}");
            throw;
        }
        finally
        {
            inputStream.Close();
            outputStream.Close();
        }
    }

    private void AddWatermarkToPdf(PdfDocument pdfDocument, string watermarkText)
    {
        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
        {
            PdfPage page = pdfDocument.GetPage(i);
            Rectangle pageSize = page.GetPageSize();

            // Настройка параметров водяного знака
            float width = pageSize.GetWidth();
            float height = pageSize.GetHeight();
            float watermarkWidth = width / 2;
            float xOffSet = 100;
            float yOffSet = -110;
            float x = (width - watermarkWidth) / 2 + xOffSet;
            float y = height / 2 + yOffSet;

            // Создание водяного знака
            Paragraph watermark = new Paragraph(watermarkText)
                .SetFontColor(iText.Kernel.Colors.ColorConstants.RED, 0.2f) // 20% прозрачность
                .SetFontSize(100)
                .SetRotationAngle(Math.PI / 4);

            // Добавление водяного знака на страницу
            Canvas canvas = new Canvas(page, pageSize);
            canvas.Add(watermark.SetFixedPosition(x, y, watermarkWidth));
        }
    }

    /// <summary>
    /// Compresses a PDF using the specified compression level.
    /// </summary>
    /// <param name="pdfBytes">Input PDF byte array.</param>
    /// <param name="compressionLevel">Compression level (from 0-9) 9 high compression 0 without.</param>
    /// <returns>Compressed PDF byte array.</returns>
    public byte[] CompressPdf(CompressPdfDTO compressPdfDto)
    {
        byte[] pdfBytes = ConvertToByteArray(compressPdfDto.pdfFile);
        MemoryStream inputStream = new MemoryStream(pdfBytes);
        MemoryStream outputStream = new MemoryStream();
        try
        {
            using (PdfReader pdfReader = new PdfReader(inputStream))
            {
                PdfWriter pdfWriter = new PdfWriter(outputStream);
                switch (compressPdfDto.compressionLevel)
                {
                    case 0: // Light Compression
                        pdfWriter.SetCompressionLevel(CompressionConstants.DEFAULT_COMPRESSION);
                        break;
                    case 1: // Strong Compression
                        pdfWriter.SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);
                        break;
                    case 2: 
                        pdfWriter.SetCompressionLevel(CompressionConstants.BEST_COMPRESSION);
                        break;
                    default:
                        throw new ArgumentException("Invalid compression level");
                }

                using (PdfDocument pdfDocument = new PdfDocument(pdfReader, pdfWriter))
                {
                    // Process the document as required
                }
                return outputStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while trying to compress PDF: {ex.Message}");
            throw;
        }
        finally
        {
            inputStream.Close();
            outputStream.Close();
        }
    }
    /// <summary>
    /// Extracts specific pages from a PDF.
    /// </summary>
    /// <param name="pdfBytes">Input PDF byte array.</param>
    /// <param name="startPage">Starting page number.</param>
    /// <param name="endPage">Ending page number.</param>
    /// <returns>Extracted PDF byte array.</returns>
    public byte[] SplitPdf(SplitPdfDto splitPdfDto)
    {
        byte[] pdfBytes = ConvertToByteArray(splitPdfDto.pdfFile);
        ValidatePageRange(splitPdfDto.startPage, splitPdfDto.endPage);

        MemoryStream pdfStream = new MemoryStream(pdfBytes);
        MemoryStream extractedPdfStream = new MemoryStream();
        try
        {
            using (PdfReader pdfReader = new PdfReader(pdfStream))
            using (PdfDocument pdfDocument = new PdfDocument(pdfReader))
            using (PdfWriter writer = new PdfWriter(extractedPdfStream))
            using (PdfDocument extractedPdf = new PdfDocument(writer))
            {
                for (int pageNum = splitPdfDto.startPage; pageNum <= Math.Min(splitPdfDto.endPage, pdfDocument.GetNumberOfPages()); pageNum++)
                {
                    extractedPdf.AddPage(pdfDocument.GetPage(pageNum).CopyTo(extractedPdf));
                }
            }
            return extractedPdfStream.ToArray();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error extracting pages: {ex.Message}");
            throw;
        }
        finally
        {
            pdfStream.Close();
            extractedPdfStream.Close();
        }
    }

    private void ValidatePageRange(int startPage, int endPage)
    {
        if (startPage < 1 || endPage < startPage)
        {
            throw new ArgumentException("Invalid page parameters.");
        }
    }
    public byte[] ConvertToByteArray(IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            return stream.ToArray();
        }
    }

}