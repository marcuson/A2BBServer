using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    public partial class InOut
    {
        public enum InOutType
        {
            In, Out
        }

        public int Id { get; set; }
        public InOutType Type { get; set; }
        public int DeviceId { get; set; }
        public DateTime? OnDate { get; set; }

        public virtual Device Device { get; set; }
    }
}
