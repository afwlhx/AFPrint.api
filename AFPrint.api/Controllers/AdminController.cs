using AFPrint.api.Context;
using Microsoft.AspNetCore.Mvc;


namespace AFPrint.api.Controllers;

[Route("api/[action]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly AFPrintDbContext _context;

    public AdminController(AFPrintDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult SearchAll(string key)
    {
        if (key != "248655")
        {
            return BadRequest("key不正确");
        }

        var list = _context.OrderInfos.ToList();
        
        return Ok(list);
    }


}