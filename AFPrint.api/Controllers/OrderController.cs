using AFPrint.api.Context;
using AFPrint.api.Models;
using AFPrint.api.Models.Get;
using Microsoft.AspNetCore.Mvc;

namespace AFPrint.api.Controllers;

[Route("api/[action]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly AFPrintDbContext _context;

    public OrderController(AFPrintDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public IActionResult Order(OrderInfoDto orderInfoDto)
    {
        var orderInfo = new OrderInfo();

        orderInfo.OrderId = $"{DateTime.Now:yyyyMMddHHmmss}_{orderInfoDto.PhoneNumber}";
        orderInfo.OrderStatus = "waiting";
        orderInfo.PhoneNumber = orderInfoDto.PhoneNumber;
        orderInfo.IsPay = true;
        orderInfo.Cost = orderInfoDto.Cost;
        orderInfo.OrderTime = DateTime.Now;
        orderInfo.FileName = orderInfoDto.FileName;


        _context.OrderInfos.Add(orderInfo);

        _context.SaveChanges();

        return Ok();
    }

    [HttpPost]
    public IActionResult OrderSearch(string phoneNumber)
    {
        // 使用LINQ语法查询关键字phoneNumber为某值下的所有数据
        var orders = _context.OrderInfos
            .Where(o => o.PhoneNumber == phoneNumber)
            .ToList();

        return Ok(orders);
    }
}