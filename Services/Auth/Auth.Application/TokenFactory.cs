namespace Auth.Application
{
    using Articles.Security;
    using Auth.Domain.Users;
    using Blocks.Core.Extensions;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;

    public class TokenFactory
    {
        private readonly IOptions<JwtOptions> _jwtOptions;

        public TokenFactory(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions;
        }

        public RefreshToken GenerateRefreshToken(string clientIpAddress)
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

        public string GenerateJwtToken(string userId, string fullName, string email, IEnumerable<string> roles,
            IEnumerable<Claim> additionalClaims)
        {
            var jwtSettings = _jwtOptions.Value;

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
}
