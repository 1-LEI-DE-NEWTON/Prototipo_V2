using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackEnd_NET6.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd_NET6.Services
{
    public class JWT_Service : I_JWT_Service
    {
        private readonly IConfiguration _config;

        public JWT_Service(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(string username)
        {            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };
            
            // Validação do tamanho da chave
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            if (key.Length < 16)
            {
                throw new ArgumentOutOfRangeException("Jwt:Key", "A chave deve ter pelo menos 16 caracteres.");
            }

            var symmmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            
            if (symmmetricKey == null)
            {
                throw new Exception("Chave de segurança inválida");
            }
            
            if (symmmetricKey.KeySize < 128)
            {
                throw new ArgumentOutOfRangeException("Tamanho da chave inválido");
            }

            var creds = new SigningCredentials(symmmetricKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }        
    }
}

