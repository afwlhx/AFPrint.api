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
    /// 是否彩印
    /// </summary>
    public bool IsColorPrint { get; set; }

    /// <summary>
    ///     总费用
    /// </summary>
    public double Cost { get; set; }
    
    /// <summary>
    /// 配送地址
    /// </summary>
    public string Address { get; set; } = null!;

    /// <summary>
    ///     文件名
    /// </summary>
    public string FileName { get; set; } = null!;
    
    /// <summary>
    /// 打印数量
    /// </summary>
    public int PrintNumber { get; set; }
}