#region

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Taste_Haven_API.Models;

#endregion

namespace Taste_Haven_API.Services;

public interface IJwtService
{
    string GenerateToken(ApplicationUser user, IList<string> roles);
}

public class JwtService(IConfiguration configuration) : IJwtService
{
    private readonly string _secretKey = configuration.GetValue<string>("ApiSettings:Secret");

    public string GenerateToken(ApplicationUser user, IList<string> roles)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_secretKey);

        var claims = new List<Claim>
        {
            new("fullName", user.Name),
            new("id", user.Id),
            new(ClaimTypes.Email, user.UserName),
            new(ClaimTypes.Role, roles.FirstOrDefault())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}