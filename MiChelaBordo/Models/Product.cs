using System;
using System.Collections.Generic;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class Product
    {
        public Product()
        {
            Concepts = new HashSet<Concept>();
        }

        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Cost { get; set; }

        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
