using System;
using System.Collections.Generic;

#nullable disable

namespace MiChelaBordo.Models
{
    public partial class Road
    {
        public int Id { get; set; }
        public string RoadName { get; set; }
        public string RoadDescription { get; set; }
        public int AvailableCapacity { get; set; }
    }
}
