using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBCommon.DTO
{
    /// <summary>
    /// DTO used to request an user password change.
    /// </summary>
    public class ChangePassRequestDTO
    {
        #region Public properties
        /// <summary>
        /// The old password.
        /// </summary>
        public string OldPassword { get; set; }

        /// <summary>
        /// The new password.
        /// </summary>
        public string NewPassword { get; set; }
        #endregion
    }
}
