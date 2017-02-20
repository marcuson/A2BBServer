using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    /// <summary>
    /// Subject (user) POCO.
    /// </summary>
    [JsonObject(IsReference = false)]
    public partial class Subject
    {
        #region Public properties
        /// <summary>
        /// The subject id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// List of devices linked to this subject.
        /// </summary>
        [JsonIgnore]
        public virtual ICollection<Device> Device { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        public Subject()
        {
            Device = new HashSet<Device>();
        }
        #endregion
    }
}
