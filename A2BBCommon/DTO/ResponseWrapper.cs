using A2BBCommon;

namespace A2BBCommon.DTO
{
    /// <summary>
    /// A wrapper for REST responses.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResponseWrapper<T>
    {
        #region Public properties
        /// <summary>
        /// The return code.
        /// </summary>
        public uint Code { get; set; }

        /// <summary>
        /// The return message. If <see cref="Status"/> is not <c>"OK"</c>, here we can find a description of the error.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The actual payload.
        /// </summary>
        public T Payload { get; set; }
        #endregion

        #region Public methods
        /// <summary>
        /// Default constructor, used for JSOn deserialization.
        /// </summary>
        private ResponseWrapper()
        {
        }

        /// <summary>
        /// Create a new instance of this class.
        /// </summary>
        /// <param name="payload">The actual payload.</param>
        /// <param name="restReturn">The Rest return from which we retrieve the code and message.</param>
        public ResponseWrapper(T payload, Constants.RestReturn? restReturn = null)
        {
            Constants.RestReturn actRet = restReturn != null ? restReturn.Value : Constants.RestReturn.OK;
            this.Code = actRet.GetCode();
            this.Message = actRet.GetMessage();
            this.Payload = payload;
        }

        /// <summary>
        /// Create a new instance of this class with <c>null</c> payload.
        /// </summary>
        /// <param name="restReturn">The Rest return from which we retrieve the code and message.</param>
        public ResponseWrapper(Constants.RestReturn restReturn) : this(default(T), restReturn)
        {
        }

        /// <summary>
        /// Create a new instance of this class with successful return.
        /// </summary>
        /// <param name="payload">The actual payload.</param>
        public ResponseWrapper(T payload) : this(payload, Constants.RestReturn.OK)
        {
        }
        #endregion
    }
}