using AFPrint.api.Context;
using AFPrint.api.Models;
using AFPrint.api.Models.Get;
using Microsoft.AspNetCore.Mvc;

namespace AFPrint.api.Controllers;

[Route("api/[action]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly AFPrintDbContext _context;
    private readonly IWebHostEnvironment _env;

    public UploadController(IWebHostEnvironment env, AFPrintDbContext context)
    {
        _env = env;
        _context = context;
    }

    /// <summary>
    ///     上传单个文件
    /// </summary>
    [HttpPost]
    [Consumes("multipart/form-data")] // ✅ swagger 识别文件上传
    public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
    {
        //检测是否有文件
        if (dto.File == null || dto.File.Length == 0)
            return BadRequest("未选择文件");

        //获取路径
        var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

        // 检测创建uploadPath目录
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        //文件名
        var fileName = $"{DateTime.Now:yyyyMMddHHmmss}_{dto.File.FileName}";
        //合并目录和文件名
        var filePath = Path.Combine(uploadPath, fileName);

        //写入
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await dto.File.CopyToAsync(stream);
        }

        var documentInfo = new DocumentInfo
        {
            FileName = fileName,
            PrintNumber = 1,
            StorgeLocation = uploadPath,
            PageNumber = 1,
            FileSize = 1.233,
            PrintRange = "1-10",
            UploadTime = DateTime.Now,
            PageDirection = true,
            IsPrinted = false,
            IsDoublePrint = true
        };

        // 添加到 DbSet
        _context.DocumentInfos.Add(documentInfo);

        // // 或者更新已有对象
        // db.DocumentInfos.Update(documentInfo);

        _context.SaveChanges(); // 才会写入数据库

        return Ok(new
        {
            fileName,
            filePath = $"/uploads/{fileName}"
        });
    }
}