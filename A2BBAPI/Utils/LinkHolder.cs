using A2BBAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2BBAPI.Utils
{
    /// <summary>
    /// Class holding info for device linking.
    /// </summary>
    public class LinkHolder
    {
        #region Public properties
        /// <summary>
        /// The device to link.
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// The user username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The user password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The user subject id.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Whether the link has been actually estabilished or not.
        /// </summary>
        public bool IsEstabilished { get; set; }
        #endregion
    }
}
