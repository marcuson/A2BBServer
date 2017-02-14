using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    [JsonObject(IsReference = true)]
    public partial class Device
    {
        public int Id { get; set; }
        public string RefreshToken { get; set; }
        public string UserId { get; set; }

        public virtual Subject User { get; set; }
    }
}
