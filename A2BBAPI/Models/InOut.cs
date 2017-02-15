using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    public partial class InOut
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public int DeviceId { get; set; }
        public DateTime? OnDate { get; set; }
    }
}
