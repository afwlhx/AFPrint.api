using AFPrint.api.Context;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public IActionResult SearchAll()
    {
        // if (key != "248655") return BadRequest("key不正确");

        var list = _context.OrderInfos.ToList();

        return Ok(list);
    }
}