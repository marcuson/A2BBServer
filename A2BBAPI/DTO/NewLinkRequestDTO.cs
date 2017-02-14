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
        /// The user password.
        /// </summary>
        public string Password { get; set; }
        #endregion
    }
}
