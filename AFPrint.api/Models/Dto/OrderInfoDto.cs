namespace AFPrint.api.Models.Dto;

public class OrderInfoDto
{
    /// <summary>
    ///     下单电话
    /// </summary>
    public string PhoneNumber { get; set; } = null!;
    
    /// <summary>
    /// 是否双面打印
    /// </summary>
    public bool IsDoublePrint { get; set; }

    /// <summary>
    ///     总费用
    /// </summary>
    public double Cost { get; set; }

    /// <summary>
    ///     文件名
    /// </summary>
    public string FileName { get; set; } = null!;
}