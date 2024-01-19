using System.IO.Compression;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Extgstate;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.AspNetCore.Http;
using PdfConverter.BLL.Services.Interfaces;
using PdfConverter.DTO;
using IronPdfDoc = IronPdf.PdfDocument;
using iTextPdfDoc = iText.Kernel.Pdf.PdfDocument; 

namespace PdfConverter.Service;
/// <summary>
/// Service for manipulating PDF files.
/// </summary>
public class PdfToBinaryConverter : IPdfToBinaryConverter
{
    public byte[] ConvertToByteArray(IFormFile file)
    {
        using (var stream = new MemoryStream())
        {
            file.CopyTo(stream);
            return stream.ToArray();
        }
    }

   
}