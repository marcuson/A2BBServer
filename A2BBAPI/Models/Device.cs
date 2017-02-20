using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    [JsonObject(IsReference = true)]
    /// <summary>
    /// Device POCO.
    /// </summary>
    public partial class Device
    {
        #region Public properties
        /// <summary>
        /// The id of this device.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The user id to which this device is connected.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Whether this device is enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// The device name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get the user to which this device is connected.
        /// </summary>
        public virtual Subject User { get; set; }

        /// <summary>
        /// Get the in/outs made by this device.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<InOut> InOut { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        public Device()
        {
            InOut = new HashSet<InOut>();
        }
        #endregion

    }
}
