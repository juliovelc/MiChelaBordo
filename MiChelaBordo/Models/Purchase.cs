using System;
using System.Collections.Generic;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class Purchase
    {
        public Purchase()
        {
            Concepts = new HashSet<Concept>();
        }

        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }
        public DateTime? PurchaseTime { get; set; }
        public string UserMail { get; set; }
        public int ConceptId { get; set; }

        public virtual Customer UserMailNavigation { get; set; }
        public virtual ICollection<Concept> Concepts { get; set; }
    }
}
