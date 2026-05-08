using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoodPet.Domain.Entities;
using MoodPet.Domain.Interfaces;
using MoodPet.Domain.Entities;

namespace MoodPet.Infraestructure.Security
{
    public class JwtService : IJwtService
    { 
        
       
            IConfiguration _configuration;
            public JwtService(IConfiguration configuration)
            {

                _configuration = configuration;
            }

            public async Task<string> GenerateJwtToken(Usuario usuario, IList<string> roles)
            {
                //reclamaciones o cracteristicas que identifican al usuario
                var claims = new List<Claim>() {
                new Claim(ClaimTypes.NameIdentifier, usuario.id.ToString()),
                new Claim(ClaimTypes.Name, usuario.email),
                new Claim(ClaimTypes.GivenName, usuario.fullName),
            };

                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwtkey"]));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                    issuer: _configuration["JwtIssuer"],
                    audience: _configuration["JwtAudience"],
                    claims: claims,

                    // Tiempo de duracion de Token
                    expires: DateTime.Now.AddDays(int.Parse(_configuration["jwtLifeTime"])),
                    signingCredentials: creds
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }






            // Revisar cual de los dos metodos es mejor (Creo que son iguales)
            public Task<string> generateJwtToken(Usuario user, List<string> list)
            {
                var claims = new List<Claim>()
{
              new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
              new Claim(ClaimTypes.Email, user.email),
};

                foreach (var role in list)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtIssuer"],
                    audience: _configuration["JwtAudience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds
                );

                return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
            }

        }
    }

