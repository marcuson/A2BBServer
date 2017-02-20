using A2BBIdentityServer.Models;
using System;

namespace A2BBIdentityServer.DTO
{
    /// <summary>
    /// DTO to request new user creation.
    /// </summary>
    public class NewUserRequestDTO
    {
        #region Public properties
        /// <summary>
        /// The user to add.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// The password for the new user.
        /// </summary>
        public String Password { get; set; }
        #endregion
    }
}
