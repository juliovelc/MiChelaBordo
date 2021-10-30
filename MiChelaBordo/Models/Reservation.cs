using System;
using System.Collections.Generic;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class Reservation
    {
        public int Id { get; set; }
        public string CompleteName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string RouteName { get; set; }
        public string TimeSchedule { get; set; }
    }
}
