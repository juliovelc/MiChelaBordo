using MiChelaBordo.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models;
using MiChelaBordo.Models.Request;

namespace MiChelaBordo.Services
{
    public class CustomerService : ICustomerService
    {
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
            }
            return res;
        }
    }
}
