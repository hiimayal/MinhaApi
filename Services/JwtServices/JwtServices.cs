using MinhaApi.Models;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace MinhaApi.Services;
public class JwtService
{
    public string GerarToken(Usuario usuario)
    {
        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes("uma-chave-secreta-bem-grande-1234")
        );
        var credenciais = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256
        );
        var claims = new[]
        {
            new Claim("Id", usuario.Id.ToString()),
            new Claim(ClaimTypes.Role, usuario.Role)
        };
        var token = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.UtcNow.AddHours(2),
        signingCredentials: credenciais
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}