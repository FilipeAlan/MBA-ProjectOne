
using Blog.Data.Entidade;

namespace Blog.Data.Util
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;

    public static class GeradorToken
    {
        private static readonly string chavePrivada = "ASDF2134RF2#@#$%g5342qfm23"; // Secrets futuramente.

        public static string GerarToken(Autor autor)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, autor.Email),
                new Claim(JwtRegisteredClaimNames.Email, autor.Email),
                new Claim("Nome", autor.Nome),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chavePrivada));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiracao = DateTime.UtcNow.AddHours(3);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expiracao,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
