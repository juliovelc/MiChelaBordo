using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MiChelaBordo.Models.Request
{
    public class CustomerRequest
    {
        public string IdMail { get; set; }
        public string Pass { get; set; }
        public string CompleteName { get; set; }
        public string TelNumber { get; set; }
        public string Rfc { get; set; }

    }
}
