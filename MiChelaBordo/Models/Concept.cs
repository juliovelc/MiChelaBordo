using System;
using System.Collections.Generic;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class Concept
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int PurchaseId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}
