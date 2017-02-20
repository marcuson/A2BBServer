using System;

namespace A2BBCommon
{
    /// <summary>
    /// Exception which can be easily interpretable for JSON response.
    /// </summary>
    public class RestReturnException : Exception
    {
        #region Public properties
        /// <summary>
        /// The return object (status, message) to be serialized on JSON response.
        /// </summary>
        public Constants.RestReturn Value { get; private set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="value">The return object (status, message) to be serialized on JSON response.</param>
        public RestReturnException(Constants.RestReturn value)
        {
            this.Value = value;
        }
        #endregion
    }
}