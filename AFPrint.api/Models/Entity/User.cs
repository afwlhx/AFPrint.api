using System;
using System.Collections.Generic;

namespace AFPrint.api.Models.Entity;

public partial class User
{
    /// <summary>
    /// 主键
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// 密码哈希
    /// </summary>
    public string? PasswordHash { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// 电话号码
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 账户属性
    /// </summary>
    public string? Role { get; set; }
}
