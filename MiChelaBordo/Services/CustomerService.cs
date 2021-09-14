using MiChelaBordo.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models;
using MiChelaBordo.Models.Request;
using MiChelaBordo.Models.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MiChelaBordo.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppSettings _appSettings;
        
        public CustomerService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }



        public AuthResponse Auth(AuthRequest model)
        {
            AuthResponse res = new AuthResponse();
            using (MiChelaBordoContext db = new MiChelaBordoContext())
            {
                string encryptedPass = MiChelaBordo.Tools.Encrypt.GetSHA252(model.Pass);
                var customer = db.Customers.Where(
                    v => v.IdMail == model.Mail && v.Pass == encryptedPass).FirstOrDefault();
                if (customer == null)
                    return null;

                res.Mail = customer.IdMail;
                res.Token = GetToken(model);
            }
            return res;
        }

        private string GetToken(AuthRequest authRequest)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.NameIdentifier, authRequest.Mail)
                    }),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
