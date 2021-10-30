using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MiChelaBordo.Models.Request
{
    public class SalesRequest
    {
        [Required] public string IdMail { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        [MinLength(1)] public List<Concept> Concepts { get; set; }

        public SalesRequest() 
        {
            this.Concepts = new List<Concept>();
        
        }

    }


    public class Concept
    {
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }
        public int IdProduct { get; set; }

    }
}
