using BackIngE_N.BD;
using BackIngE_N.Config.Messages;
using BackIngE_N.DTO.UserrDto;
using BackIngE_N.Models;
using _bCrypt = BCrypt.Net.BCrypt;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace BackIngE_N.Config.Jwt {
    public class JwtConfig {

        public IConfiguration _config;


        public JwtConfig(IConfiguration config) {
            _config = config;
        }

        /// <summary>
        /// Generate a JWT token. If the user already has a token, it will return it.
        /// </summary>
        /// <param name="user">The User to validate</param>
        /// <param name="u">The User from bd</param>
        /// <returns>A response object with the token if the credentials are correct</returns>
        public Response generateToken(UserBase user, Userr u) {

            if (user == null || !_bCrypt.Verify(user.Password, u.Password)) throw new Exception(UserrMessages.ErrorMessages.LoginError);

            if (u.Token != null && ValidateToken(u.Token)) return new Response(UserrMessages.SuccessMessages.LoginSuccess, true, u.Token);

            var jwt = _config.GetSection("JWT").Get<Jwt>() ?? throw new Exception(GeneralMessages.Error);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("email", user.Email.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.UtcNow.AddDays(jwt.ExpireTime),
                signingCredentials: singIn
            );

            return new Response(GeneralMessages.TokenGenerated, true, new JwtSecurityTokenHandler().WriteToken(token));

        }

        /// <summary>
        /// Validates a JWT token.
        /// </summary>
        /// <param name="token">The JWT token to validate.</param>
        /// <returns>True if the token is valid, false otherwise.</returns>
        public bool ValidateToken(string token) {

            if (string.IsNullOrEmpty(token)) return false;

            var jwt = _config.GetSection("JWT").Get<Jwt>() ?? throw new Exception(GeneralMessages.Error);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwt.Key);
            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                return true;
            } catch (Exception) {
                return false;
            }
        }

    }
}
