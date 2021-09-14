using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models.Response;
using MiChelaBordo.Models.Request;

namespace MiChelaBordo.Services
{
    public interface ICustomerService
    {
        AuthResponse Auth(AuthRequest model);
    }
}
