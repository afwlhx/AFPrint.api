using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AFPrint.api.Context;
using AFPrint.api.Models.Dto;
using AFPrint.api.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AFPrint.api.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly MyDbContext  _context;
    
    public AuthController(IConfiguration config , MyDbContext context)
    {
        _config = config;
        _context = context;
    }
    
    // 模拟用户表（实际用 EF Core + MySQL）
    // private static List<User> Users = new List<User>
    // {
    //     new User { Id = 1, UserName = "alice", PasswordHash = BCrypt.Net.BCrypt.HashPassword("P@ssw0rd"), Role="Admin" },
    //     new User { Id = 2, UserName = "bob",   PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"), Role="User" }
    // };
    
    // ✅ 注册接口
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest( new { message = "用户名和密码不能为空" });

        var exist = _context.Users.FirstOrDefault(x => x.Username == dto.Username);
        if (exist != null)
            return Conflict( new { message ="该用户名已被注册" });

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User"
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "注册成功", user = new { user.Id, user.Username } });
    }

    
    [HttpPost("login")]
    public IActionResult Login(LoginDto dto)
    {
        // ✅ 改成从 MySQL 查询用户
        var user = _context.Users.FirstOrDefault(x => x.Username == dto.Username);
        if (user == null) return Unauthorized();
    
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return Unauthorized(new { message = "密码错误" });
    
        // 生成 Token
        var token = GenerateJwtToken(user);
        return Ok(new { message = "登录成功", accessToken = token, tokenType = "Bearer" });
    }
    
    // ✅ 当前用户信息
    [HttpGet("profile")]
    [Authorize]
    public IActionResult Profile()
    {
        var id = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var name = User.Identity?.Name;
        var role = User.FindFirstValue(ClaimTypes.Role);
        
        // 使用LINQ语法查询关键字phoneNumber为某值下的所有数据
        var orders = _context.OrderInfos
            .Where(o => o.UserId == id)
            .ToList();
        
        return Ok(new { id, name, role, orders });
    }
    
    // 用户状态检测
    [HttpGet("user-status")]
    [Authorize]
    public IActionResult UserStatus()
    {
        return Ok();
    }

    [HttpGet("admin-only")]
    [Authorize(Roles = "Admin")]
    public IActionResult AdminOnly()
    {
        return Ok(new { message = "You Are Admin" });
    }



    private string GenerateJwtToken(User user)
    {
        var jwtSection = _config.GetSection("Jwt");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];
        var key = jwtSection["Key"];
        var expiresMinutes = int.Parse(jwtSection["ExpiresMinutes"] ?? "60");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var creds = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

    

    