namespace AFPrint.api.Models.Get;

public class FileUploadDto
{
    /// <summary>
    ///     单文件上传
    /// </summary>
    public IFormFile? File { get; set; }
}