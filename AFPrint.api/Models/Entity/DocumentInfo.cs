using System;
using System.Collections.Generic;

namespace AFPrint.api.Models.Entity;

public partial class DocumentInfo
{
    /// <summary>
    /// 文件编号
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 文件上传者UUID
    /// </summary>
    public string UploadVisitorUuid { get; set; } = null!;

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName { get; set; } = null!;

    /// <summary>
    /// 打印数量
    /// </summary>
    public int PrintNumber { get; set; }

    /// <summary>
    /// 存储位置
    /// </summary>
    public string StorgeLocation { get; set; } = null!;

    /// <summary>
    /// 文档页数
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// 文件大小
    /// </summary>
    public double FileSize { get; set; }

    /// <summary>
    /// 打印范围
    /// </summary>
    public string PrintRange { get; set; } = null!;

    /// <summary>
    /// 上传时间
    /// </summary>
    public DateTime UploadTime { get; set; }

    /// <summary>
    /// 页面方向
    /// </summary>
    public bool PageDirection { get; set; }

    /// <summary>
    /// 打印状态
    /// </summary>
    public bool IsPrinted { get; set; }

    /// <summary>
    /// 是否双面
    /// </summary>
    public bool IsDoublePrint { get; set; }
}
