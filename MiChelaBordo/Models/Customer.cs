using System;
using System.Collections.Generic;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Purchases = new HashSet<Purchase>();
        }

        public string IdMail { get; set; }
        public string Pass { get; set; }
        public string CompleteName { get; set; }
        public string TelNumber { get; set; }
        public string Rfc { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
    }
}
