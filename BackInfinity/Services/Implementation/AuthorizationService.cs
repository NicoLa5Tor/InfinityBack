

using BackInfinity.Models;
using BackInfinity.Models.Custom;
using BackInfinity.Models.Encrypt;
using BackInfinity.Services.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackInfinity.Services.Implementation
{
    public class AuthorizationService : IAuthorizationService
        

    {
        private readonly DbinfinityContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationService(DbinfinityContext _context, IConfiguration _configuration)
        {
            this._context = _context;
            this._configuration = _configuration;
        }
        private string GenerateToken(string idUser)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            if (key is null) return null;
            var keyBites = Encoding.ASCII.GetBytes(key);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUser));
            var credentialsToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBites),
                SecurityAlgorithms.HmacSha256Signature
                );
            var tokenDetails = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = credentialsToken
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDetails);
            string tokenC = tokenHandler.WriteToken(tokenConfig);
            return tokenC;
        }
    
        public async Task<AuthorizationResponse> ReturnToken(AuthorizationRequest aut)
        {
            try
            {
             
                var user = await _context.Accesses.FirstOrDefaultAsync(em => em.UserName == aut.userName);
                if (user is null) return await Task.FromResult<AuthorizationResponse>(null);

                user.Password = Encryption.Decrypt(user.Password);
                if (aut.password == user.Password)
                {
                    string token = GenerateToken(user.IdAcces.ToString());
                     return new AuthorizationResponse() { Token = token, Result = true, Message = "OK" };
                }
                else
                {
                    return new AuthorizationResponse() { Token = null, Result = false, Message = "Error" };
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }
}
