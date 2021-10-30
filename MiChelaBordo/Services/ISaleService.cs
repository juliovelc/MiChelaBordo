using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiChelaBordo.Models.Request;

namespace MiChelaBordo.Services
{
    public interface ISaleService
    {
        public void Add(SalesRequest model);
    }
}
