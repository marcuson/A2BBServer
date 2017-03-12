using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    /// <summary>
    /// Granter POCO.
    /// </summary>
    public partial class Granter
    {
        #region Public properties
        /// <summary>
        /// The granter id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The subject id linked to this granter.
        /// </summary>
        public string SubId { get; set; }

        /// <summary>
        /// The subject linked to this granter.
        /// </summary>
        public virtual Subject Sub { get; set; }
        #endregion
    }
}
