using Ledbim.ApiExample.Domain.Entities;
using Ledbim.Core.Security;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ledbim.ApiExample.Application.Helpers
{
    public static class JwtSecurity
    {
        /// <summary>
        /// Access Token Üreten fonksiyondur. 
        /// </summary>
        /// <param name="_settings"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static JwtSecurityToken GetAccessToken(ITokenSettings _settings, User user) => CreateToken(_settings, user, false);

        /// <summary>
        /// Refresh Token Üreten fonksiyondur. 
        /// </summary>
        /// <param name="_settings"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public static JwtSecurityToken GetRefreshToken(ITokenSettings _settings, User user) => CreateToken(_settings, user, true);

        private static JwtSecurityToken CreateToken(ITokenSettings _settings, User user, bool isRefresh)
        {
            string SecurityKey = isRefresh ? _settings.RefreshTokenSecurityKey : _settings.AccessTokenSecurityKey;
            int Expiration = isRefresh ? _settings.RefreshTokenExpiration : _settings.AccessTokenExpiration;

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityKey));
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: new List<Claim> {
                    new Claim("UserId", user.Id.ToString()),
                    new Claim("UserName", user.FullName),
                    new Claim("UserMail", user.Mail)
                },
                notBefore: now,
                expires: now.Add(TimeSpan.FromDays(Expiration)),
                signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );
        }

        /// <summary>
        /// Access Token kontrol eden fonksiyondur.
        /// </summary>
        /// <param name="_settings"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidateAccessToken(ITokenSettings _settings, string token) => ValidateToken(_settings, token, false);

        /// <summary>
        /// Refresh Token kontrol eden fonksiyondur.
        /// </summary>
        /// <param name="_settings"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool ValidateRefreshToken(ITokenSettings _settings, string token) => ValidateToken(_settings, token, true);

        private static bool ValidateToken(ITokenSettings _settings, string token, bool isRefresh)
        {
            string SecurityKey = isRefresh ? _settings.RefreshTokenSecurityKey : _settings.AccessTokenSecurityKey;
            SymmetricSecurityKey symmetricSecurityKey = new(Encoding.ASCII.GetBytes(SecurityKey));
            JwtSecurityTokenHandler tokenHandler = new();
            TokenValidationParameters validationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuer = true,
                ValidIssuer = _settings.Issuer,
                ValidateAudience = true,
                ValidAudience = _settings.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true
            };


            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
