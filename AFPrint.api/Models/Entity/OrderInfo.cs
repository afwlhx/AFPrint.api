namespace AFPrint.api.Models;

public class OrderInfo
{
    /// <summary>
    ///     id
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    ///     订单id
    /// </summary>
    public string OrderId { get; set; } = null!;

    /// <summary>
    ///     订单状态
    /// </summary>
    public string OrderStatus { get; set; } = null!;

    /// <summary>
    ///     下单电话
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    ///     是否支付
    /// </summary>
    public bool IsPay { get; set; }

    /// <summary>
    ///     总费用
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    ///     下单时间
    /// </summary>
    public DateTime OrderTime { get; set; }

    /// <summary>
    ///     文件名
    /// </summary>
    public string FileName { get; set; } = null!;
}