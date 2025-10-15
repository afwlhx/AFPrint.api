using System;
using System.Collections.Generic;

namespace AFPrint.api.Models.Entity;

public partial class OrderInfo
{
    /// <summary>
    /// id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 订单id
    /// </summary>
    public string OrderId { get; set; } = null!;

    /// <summary>
    /// 订单状态
    /// </summary>
    public string OrderStatus { get; set; } = null!;

    /// <summary>
    /// 是否双面打印
    /// </summary>
    public bool IsDoublePrint { get; set; }

    /// <summary>
    /// 是否彩印
    /// </summary>
    public bool IsColorPrint { get; set; }

    /// <summary>
    /// 是否支付
    /// </summary>
    public bool IsPay { get; set; }

    /// <summary>
    /// 总费用
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    /// 下单时间
    /// </summary>
    public DateTime OrderTime { get; set; }

    /// <summary>
    /// 配送地址
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// 打印数量
    /// </summary>
    public int? PrintNumber { get; set; }

    /// <summary>
    /// 用户id
    /// </summary>
    public int UserId { get; set; }
}
