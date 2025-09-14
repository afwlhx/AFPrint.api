using System;
using System.Collections.Generic;
using AFPrint.api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace AFPrint.api.Context;

public partial class AFPrintDbContext : DbContext
{
    public AFPrintDbContext()
    {
    }

    public AFPrintDbContext(DbContextOptions<AFPrintDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DocumentInfo> DocumentInfos { get; set; }

    public virtual DbSet<OrderInfo> OrderInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=8.156.71.102;port=3306;database=afprint;user=afprint;password=TrHwJKn8QdKj2nEX", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.4.6-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<DocumentInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("document_info");

            entity.Property(e => e.Id)
                .HasComment("文件编号")
                .HasColumnName("id");
            entity.Property(e => e.FileName)
                .HasMaxLength(256)
                .HasComment("文件名")
                .HasColumnName("file_name");
            entity.Property(e => e.FileSize)
                .HasComment("文件大小")
                .HasColumnName("file_size");
            entity.Property(e => e.IsDoublePrint)
                .HasComment("是否双面")
                .HasColumnName("is_double_print");
            entity.Property(e => e.IsPrinted)
                .HasComment("打印状态")
                .HasColumnName("is_printed");
            entity.Property(e => e.PageDirection)
                .HasComment("页面方向")
                .HasColumnName("page_direction");
            entity.Property(e => e.PageNumber)
                .HasComment("文档页数")
                .HasColumnName("page_number");
            entity.Property(e => e.PrintNumber)
                .HasComment("打印数量")
                .HasColumnName("print_number");
            entity.Property(e => e.PrintRange)
                .HasMaxLength(8)
                .HasComment("打印范围")
                .HasColumnName("print_range");
            entity.Property(e => e.StorgeLocation)
                .HasMaxLength(256)
                .HasComment("存储位置")
                .HasColumnName("storge_location");
            entity.Property(e => e.UploadTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("上传时间")
                .HasColumnType("timestamp")
                .HasColumnName("upload_time");
            entity.Property(e => e.UploadVisitorUuid)
                .HasMaxLength(64)
                .HasComment("文件上传者UUID")
                .HasColumnName("upload_visitor_uuid");
        });

        modelBuilder.Entity<OrderInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("order_info");

            entity.Property(e => e.Id)
                .HasComment("id")
                .HasColumnName("id");
            entity.Property(e => e.Cost)
                .HasComment("总费用")
                .HasColumnName("cost");
            entity.Property(e => e.FileName)
                .HasMaxLength(256)
                .HasComment("文件名")
                .HasColumnName("file_name");
            entity.Property(e => e.IsDoublePrint)
                .HasComment("是否双面打印")
                .HasColumnName("is_double_print");
            entity.Property(e => e.IsPay)
                .HasComment("是否支付")
                .HasColumnName("is_pay");
            entity.Property(e => e.OrderId)
                .HasMaxLength(64)
                .HasComment("订单id")
                .HasColumnName("order_id");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(16)
                .HasComment("订单状态")
                .HasColumnName("order_status");
            entity.Property(e => e.OrderTime)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasComment("下单时间")
                .HasColumnType("timestamp")
                .HasColumnName("order_time");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(16)
                .HasComment("下单电话")
                .HasColumnName("phone_number");
            entity.Property(e => e.UploadVisitorUuid)
                .HasMaxLength(64)
                .HasComment("上传者UUID")
                .HasColumnName("upload_visitor_uuid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
