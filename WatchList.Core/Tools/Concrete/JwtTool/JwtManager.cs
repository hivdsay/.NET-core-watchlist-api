using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WatchList.Core.Tools.Abstract.JwtTool;
using WatchList.Entities.Concrete;

namespace WatchList.Core.Tools.Concrete.JwtTool;

public class JwtManager : IJwtService
{
    private readonly IOptions<JwtInfo> _optionsJwt;
    public JwtManager(IOptions<JwtInfo> optionsJwt)
    {
        _optionsJwt = optionsJwt;
    }

    public async Task<string> GenerateJwt(User user, List<UserRole> role)
    {
        string jwtToken = "";
        await Task.Run(() =>
        {
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_optionsJwt.Value.SecurityKey));
            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _optionsJwt.Value.Issuer,
                audience: _optionsJwt.Value.Audience, notBefore: DateTime.Now,
                expires: DateTime.Now.AddMinutes(_optionsJwt.Value.Expires),
                signingCredentials: signingCredentials, claims: SetClaims(user, role));
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            jwtToken = handler.WriteToken(jwtSecurityToken);
        });
        return jwtToken;
    }


    private List<Claim> SetClaims(User user, List<UserRole> roles)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Name, user.Email));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        claims.Add(new Claim("UserId", user.Id.ToString()));

        if (roles?.Count > 0)
        {
            // foreach (var item in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, item.Role.RoleName.ToString()));
            // }
            claims.Add(new Claim(ClaimTypes.Role, "Admin"));
        }
        return claims;
    }
}