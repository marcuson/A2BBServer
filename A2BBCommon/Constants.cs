namespace A2BBCommon
{
    /// <summary>
    /// Constants shared among Identity Server and API.
    /// </summary>
    public static class Constants
    {
        #region Constants
        /// <summary>
        /// The identity server endpoint.
        /// </summary>
        public const string IDENTITY_SERVER_ENDPOINT = "http://localhost:5000";

        /// <summary>
        /// The API endpoint.
        /// </summary>
        public const string API_ENDPOINT = "http://localhost:5001";

        /// <summary>
        /// The client id for A2BB API.
        /// </summary>
        public const string A2BB_API_CLIENT_ID = "a2bb_api";

        /// <summary>
        /// The client secret for A2BB API.
        /// </summary>
        public const string A2BB_API_CLIENT_SECRET = "a2bb_api_secret";

        /// <summary>
        /// The client id for A2BB Identity Server (resource owner).
        /// </summary>
        public const string A2BB_IDSRV_RO_CLIENT_ID = "a2bb.ro_id_srv";

        /// <summary>
        /// Name of A2BB API resource.
        /// </summary>
        public const string A2BB_API_RESOURCE_NAME = "A2BB_API";

        /// <summary>
        /// REST return codes.
        /// </summary>
        public enum RestReturn
        {
            OK,
            ERR_USER_CREATE,
            ERR_USER_ROLE_ASSIGN,
            ERR_INVALID_SUB_CLAIM,
            ERR_INVALID_NAME_CLAIM,
            ERR_UNKNOWN
        }
        #endregion
    }

    #region Extensions classes
    /// <summary>
    /// Extension methods for <see cref="Constants.RestReturn"./>
    /// </summary>
    public static class RestReturnExtensionMethods
    {
        /// <summary>
        /// Get return code.
        /// </summary>
        /// <param name="restReturn">The object from which we get the code.</param>
        /// <returns>The return code.</returns>
        public static uint GetCode(this Constants.RestReturn restReturn)
        {
            switch (restReturn)
            {
                case Constants.RestReturn.OK:
                    return 0x00000000;
                case Constants.RestReturn.ERR_USER_CREATE:
                    return 0x00000001;
                case Constants.RestReturn.ERR_USER_ROLE_ASSIGN:
                    return 0x00000002;
                case Constants.RestReturn.ERR_INVALID_SUB_CLAIM:
                    return 0x00000003;
                case Constants.RestReturn.ERR_INVALID_NAME_CLAIM:
                    return 0x00000004;
                default:
                    return 0xFFFFFFFF;
            }
        }

        /// <summary>
        /// Get return message.
        /// </summary>
        /// <param name="restReturn">The object from which we get the message.</param>
        /// <returns>The return message.</returns>
        public static string GetMessage(this Constants.RestReturn restReturn)
        {
            switch (restReturn)
            {
                case Constants.RestReturn.OK:
                    return "Success";
                case Constants.RestReturn.ERR_USER_CREATE:
                    return "Error during user creation";
                case Constants.RestReturn.ERR_USER_ROLE_ASSIGN:
                    return "Error during user role assignation";
                case Constants.RestReturn.ERR_INVALID_SUB_CLAIM:
                    return "Error invalid sub claim";
                case Constants.RestReturn.ERR_INVALID_NAME_CLAIM:
                    return "Error invalid name claim";
                default:
                    return "Unknown erorr";
            }
        }
    }
    #endregion
}