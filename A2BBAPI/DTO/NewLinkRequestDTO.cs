using A2BBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBAPI.DTO
{
    /// <summary>
    /// DTO for new device link request.
    /// </summary>
    public class NewLinkRequestDTO
    {
        #region Public properties
        /// <summary>
        /// The device to add.
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// The user password.
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}
