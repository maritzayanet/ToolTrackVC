/*
using Microsoft.AspNetCore.Mvc;
using ToolTrack.DTOs;
using ToolTrack.Security;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ToolTrack.Controllers;




[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly JwtService _jwtService;

    public AuthController(JwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto login)
    {
        // Credenciales temporales solo para aprendizaje
        if (login.Username != "admin" ||
            login.Password != "ToolTrack123!")
        {
            return Unauthorized(new
            {
                message = "Usuario o contraseña incorrectos"
            });
        }

        var token = _jwtService.GenerateToken(login.Username);

        return Ok(new
        {
            token,
            tokenType = "Bearer",
            expiresInMinutes = 60
        });
    }
}
*/

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ToolTrack.DTOs;

namespace ToolTrack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto login)
    {
        var username = _configuration["Jwt:Username"];
        var password = _configuration["Jwt:Password"];

        if (login.Username != username ||
            login.Password != password)
        {
            return Unauthorized(new
            {
                message = "Usuario o contraseña incorrectos"
            });
        }

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, login.Username)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: credentials
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler()
                .WriteToken(token)
        });
    }
}
