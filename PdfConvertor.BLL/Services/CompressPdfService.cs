using PdfConvertor.BLL.DTO;
using PdfConvertor.BLL.Services.Interfaces;

namespace PdfConvertor.BLL.Services;

public class CompressPdfService : ICompressPdfService
{
    private readonly IPdfToBinaryConverter _iPdfToBinaryConverter;

   public CompressPdfService(IPdfToBinaryConverter iPdfToBinaryConverter)
    {
        _iPdfToBinaryConverter = iPdfToBinaryConverter;
    }
    
    public byte[] CompressPdf(CompressPdfDTO compressPdfDto)
    {
        int compressLevel = compressPdfDto.compressionLevel;
        byte[] pdfBytes = _iPdfToBinaryConverter.ConvertToByteArray(compressPdfDto.pdfFile);
        var pdf = new PdfDocument(pdfBytes);
        switch (compressLevel)
        {
            case 0: //Light Compression
                pdf.CompressImages(50);
                break;
            case 1: //Medium Compression
                pdf.CompressImages(30);
                break;
            case 2:
                pdf.CompressImages(5);
                break;
            default:
                pdf.CompressImages(50);
                break;
        }

        return pdf.BinaryData;

    }
}