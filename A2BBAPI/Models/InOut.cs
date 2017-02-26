using System;
using System.Collections.Generic;

namespace A2BBAPI.Models
{
    /// <summary>
    /// In/Out POCO.
    /// </summary>
    public partial class InOut
    {
        #region Public static fields
        /// <summary>
        /// Type of record (in or out).
        /// </summary>
        public enum InOutType
        {
            In, Out
        }
        #endregion

        #region Public properties
        /// <summary>
        /// The record id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The record type.
        /// </summary>
        public InOutType Type { get; set; }

        /// <summary>
        /// The id of the device which performed this action.
        /// </summary>
        public int DeviceId { get; set; }

        /// <summary>
        /// The date on which the action was performed.
        /// </summary>
        public DateTime? OnDate { get; set; }

        /// <summary>
        /// The device which performed this action.
        /// </summary>
        public virtual Device Device { get; set; }
        #endregion
    }
}
