using System.Text;
using AFPrint.api.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 跨域 CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin() // 允许所有来源（生产环境建议限制）
            .AllowAnyMethod() // 允许所有请求方法
            .AllowAnyHeader(); // 允许所有请求头
    });
});

// JWT认证
// 读取 JWT 配置
var jwtSection = builder.Configuration.GetSection("Jwt");
var issuer = jwtSection["Issuer"]; // 签发着
var audience = jwtSection["Audience"]; // 接收方
var key = jwtSection["Key"]; // 密钥
var signingKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)); // 生成对称加密的签名密钥 这里使用 SymmetricSecurityKey（对称加密密钥），意思是：
// 签发和验证 JWT 都用同一把 key（不是公钥私钥的非对称方式）。
// 注册 JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // 要验证 token 的签发者（Issuer）是否匹配配置的 ValidIssuer
            ValidateAudience = true, // 要验证 token 的接收者（Audience）是否匹配配置的 ValidAudience
            ValidateLifetime = true, // 要验证 token 是否过期（过期就不能用了）
            ValidateIssuerSigningKey = true, // 要验证 token 的签名是否正确（防止伪造）
            ValidIssuer = issuer, // 指定合法的签发者
            ValidAudience = audience, // 指定合法的接收方,
            IssuerSigningKey = signingKey // 用来验证签名的密钥（必须和签发时用的 key 一致）
        };
    });

// 配置 MySQL 数据库上下文
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// 使用 CORS（必须在 MapControllers 之前）
app.UseCors("AllowAll");

// 启用身份验证功能
app.UseAuthentication();

app.UseAuthorization();

// 启用 wwwroot 静态文件访问
app.UseStaticFiles();

// 如果需要目录浏览（可选）
// app.UseDirectoryBrowser();

app.MapControllers();

app.Run();