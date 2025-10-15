using System.Security.Claims;
using AFPrint.api.Context;
using AFPrint.api.Models.Dto;
using AFPrint.api.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AFPrint.api.Controllers;

[Route("api/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly MyDbContext _context;

    public OrderController(MyDbContext context)
    {
        _context = context;
    }

    // 下单
    [HttpPost]
    [Authorize]
    public IActionResult Order(OrderInfoDto orderInfoDto)
    {
        var orderInfo = new OrderInfo();
        
        orderInfo.OrderId = $"{DateTime.Now:yyyyMMddHHmmss}-{User.FindFirstValue(ClaimTypes.NameIdentifier)}";
        orderInfo.OrderStatus = "waiting";
        orderInfo.IsDoublePrint = orderInfoDto.IsDoublePrint;
        orderInfo.IsColorPrint = orderInfoDto.IsColorPrint;
        orderInfo.IsPay = false;
        orderInfo.Cost = orderInfoDto.Cost;
        orderInfo.OrderTime = DateTime.Now;
        orderInfo.Address = orderInfoDto.Address;
        orderInfo.FileName = orderInfoDto.FileName;
        orderInfo.PrintNumber = orderInfoDto.PrintNumber;
        orderInfo.UserId = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier)); //通过JWT获得ID

        _context.OrderInfos.Add(orderInfo);

        _context.SaveChanges();

        return Ok();
    }
}