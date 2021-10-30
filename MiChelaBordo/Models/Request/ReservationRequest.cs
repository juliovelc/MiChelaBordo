using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiChelaBordo.Models.Request
{
    public class ReservationRequest
    {
        public string CompleteName { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public string RouteName { get; set; }
        public string TimeSchedule { get; set; }

    }
}
