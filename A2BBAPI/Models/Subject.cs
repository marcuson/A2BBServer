using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    [JsonObject(IsReference = true)]
    public partial class Subject
    {
        public Subject()
        {
            Device = new HashSet<Device>();
        }

        public string Id { get; set; }

        [JsonIgnore]
        public virtual ICollection<Device> Device { get; set; }
    }
}
