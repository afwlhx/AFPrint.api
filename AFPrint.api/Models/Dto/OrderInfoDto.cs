namespace AFPrint.api.Models.Get;

public class OrderInfoDto
{
    /// <summary>
    ///     下单电话
    /// </summary>
    public string PhoneNumber { get; set; } = null!;

    /// <summary>
    ///     总费用
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    ///     文件名
    /// </summary>
    public string FileName { get; set; } = null!;
}