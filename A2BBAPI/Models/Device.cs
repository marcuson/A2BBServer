using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    [JsonObject(IsReference = true)]
    public partial class Device
    {
        public Device()
        {
            InOut = new HashSet<InOut>();
        }

        public int Id { get; set; }
        public string UserId { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }

        public virtual Subject User { get; set; }

        [JsonIgnore]
        public virtual ICollection<InOut> InOut { get; set; }
    }
}
