using PdfSharp.Pdf.IO;

namespace AFPrint.api;

public class PagesCount
{
    [Obsolete("Obsolete")]
    public static int GetPdfPageCount(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        using var pdf = PdfReader.Open(stream, PdfDocumentOpenMode.ReadOnly);
        return pdf.PageCount;
    }
}