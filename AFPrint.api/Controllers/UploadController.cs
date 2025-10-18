using AFPrint.api.Context;
using AFPrint.api.Models.Entity;
using Microsoft.AspNetCore.Mvc;

namespace AFPrint.api.Controllers;

[Route("api/[action]")]
[ApiController]
public class UploadController : ControllerBase
{
    private readonly MyDbContext _context;
    private readonly IWebHostEnvironment _env;

    public UploadController(IWebHostEnvironment env, MyDbContext context)
    {
        _env = env;
        _context = context;
    }

    /// <summary>
    ///     上传单个文件
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file, string visitorUuid)
    {
        // 根据UUID搜索最后一个的时间
        var latestUploadTime = _context.DocumentInfos
            .Where(f => f.UploadVisitorUuid == visitorUuid) // 根据 uuid 查询
            .OrderByDescending(f => f.UploadTime) // 按上传时间倒序
            .Select(f => f.UploadTime) // 只取上传时间
            .FirstOrDefault(); // 最新一条

        // 检测时间是否间隔30秒
        var diff = DateTime.Now - latestUploadTime;
        if (diff.TotalSeconds < 30) return BadRequest("时间请间隔30秒");


        //检测是否有文件
        if (file == null || file.Length == 0)
            return BadRequest("未选择文件");

        //获取路径
        var uploadPath = Path.Combine(_env.WebRootPath, "uploads");

        // 检测创建uploadPath目录
        if (!Directory.Exists(uploadPath))
            Directory.CreateDirectory(uploadPath);

        //文件名
        var fileName = $"{DateTime.Now:yyyyMMddHHmmss}-{file.FileName}";
        //合并目录和文件名
        var filePath = Path.Combine(uploadPath, fileName);

        //写入
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var documentInfo = new DocumentInfo
        {
            UploadVisitorUuid = visitorUuid,
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

    // 查询是否能上传 ( 时间间隔是否满足 )
//     [HttpPost]
//     public IActionResult IsCanUpload(string visitorUUID)
//     {
//         // 根据UUID搜索时间
//         var latestUploadTime = _context.DocumentInfos
//             .Where(f => f.UploadVisitorUuid == visitorUUID)                // 根据 uuid 查询
//             .OrderByDescending(f => f.UploadTime)    // 按上传时间倒序
//             .Select(f => f.UploadTime)               // 只取上传时间
//             .FirstOrDefault();                       // 最新一条
//         
//         return Ok(latestUploadTime);
//     }
}