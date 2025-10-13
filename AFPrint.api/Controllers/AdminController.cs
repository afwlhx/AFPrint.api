using AFPrint.api.Context;
using Microsoft.AspNetCore.Mvc;

namespace AFPrint.api.Controllers;

[Route("api/[action]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly MyDbContext _context;

    public AdminController(MyDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult SearchAll(string key)
    {
        if (key != "248655") return BadRequest("key不正确");

        var list = _context.OrderInfos.ToList();

        return Ok(list);
    }
}