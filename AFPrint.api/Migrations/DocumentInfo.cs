namespace AFPrint.api.Migrations;

public class DocumentInfo
{
    public int Id { get; set; }

    public string FileName { get; set; } = null!;

    public int PrintNumber { get; set; }

    public string StorgeLocation { get; set; } = null!;

    public int PageNumber { get; set; }

    public double FileSize { get; set; }

    public string PrintRange { get; set; } = null!;

    public DateTime UploadTime { get; set; }

    public bool PageDirection { get; set; }

    public bool IsPrinted { get; set; }

    public bool IsDoublePrint { get; set; }
}