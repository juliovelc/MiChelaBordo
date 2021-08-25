using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiChelaBordo.Models.Response
{
    public class ResponseTemplate
    {

        public int Success { get; set; } = 0;
        public string Message { get; set; }
        public object Data { get; set; }
        

    }
}
