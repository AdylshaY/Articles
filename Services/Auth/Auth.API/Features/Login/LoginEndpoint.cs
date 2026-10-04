using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Articles.Security;
using Auth.Domain.Users;
using Blocks.AspNetCore.Extensions;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Identity;
using Blocks.Core.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Auth.API.Features.Login;

[HttpPost("login")]
public class LoginEndpoint(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtOptions>  jwtOptions)
    : Endpoint<LoginCommand, LoginResponse>
{
    public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
    {
        var user = await userManager.FindByEmailAsync(command.Email);
        if (user is null)
            throw new BadRequestException($"User {command.Email} not found");

        var result = await signInManager.CheckPasswordSignInAsync(user, command.Password, false);
        if (!result.Succeeded)
            throw new BadRequestException($"Invalid username or password");

        var userRoles = await userManager.GetRolesAsync(user);

        //Generate JWT Token
        var jwtToken = GenerateJwtToken(user.Id.ToString(), user.FullName, command.Email, userRoles, Array.Empty<Claim>());
        var refreshToken = GenerateRefreshToken(HttpContext.GetClientIpAddress());
        user.RefreshTokens.Add(refreshToken);
        await userManager.UpdateAsync(user);

        await Send.OkAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token), ct);
    }

    private RefreshToken GenerateRefreshToken(string clientIpAddress)
    {
        using (var rng = RandomNumberGenerator.Create())
        {
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                CreatedOn = DateTime.UtcNow,
                CreatedByIp = clientIpAddress,
            };
        }
    }
    
    private string GenerateJwtToken(string userId, string fullName, string email, IEnumerable<string> roles,
        IEnumerable<Claim> additionalClaims)
    {
        var jwtSettings = jwtOptions.Value;
        
        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToUnixEpochDate().ToString(),
                    ClaimValueTypes.Integer64),

                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, fullName),
                new Claim(ClaimTypes.Email, email)
            }
            .Concat(roles.Select(r => new Claim(ClaimTypes.Role, r)))
            .Concat(additionalClaims);

        var secretKey = new SymmetricSecurityKey(Encoding.Default.GetBytes("this-is-a-secret-key"));
        var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            jwtSettings.Issuer, 
            jwtSettings.Audience,
            notBefore: DateTime.UtcNow,
            expires: jwtSettings.Expiration,
            claims: claims,
            signingCredentials: signInCredentials);
        
        var encodedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return encodedJwtToken;
    }
}