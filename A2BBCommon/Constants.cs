namespace A2BBCommon
{
    /// <summary>
    /// Constants shared among Identity Server and actual A2BB API.
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
        /// The client id for A2BB API (for resource onwer/pass flow).
        /// </summary>
        public const string A2BB_API_RO_CLIENT_ID = "a2bb.ro_api";

        /// <summary>
        /// The client id for A2BB API (for client flow).
        /// </summary>
        public const string A2BB_API_CC_CLIENT_ID = "a2bb.cc_api";

        /// <summary>
        /// The client secret for A2BB API (for client flow).
        /// </summary>
        public const string A2BB_API_CC_CLIENT_SECRET = "a2bb.cc_api_secret";

        /// <summary>
        /// The client id for A2BB Identity Server (for resource onwer/pass flow).
        /// </summary>
        public const string A2BB_IDSRV_RO_CLIENT_ID = "a2bb.ro_id_srv";

        /// <summary>
        /// Name of identity server API resource.
        /// </summary>
        public const string A2BB_IDSRV_RESOURCE_NAME = "IDSRV_API";

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
            ERR_USER_NOT_FOUND,
            ERR_USER_DELETE,
            ERR_INVALID_PASS,
            ERR_USER_UPDATE,
            ERR_LINK,
            ERR_DEVICE_NOT_FOUND,
            ERR_DEVICE_DISABLED,
            ERR_INVALID_GRANTER,
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
                case Constants.RestReturn.ERR_USER_NOT_FOUND:
                    return 0x00000005;
                case Constants.RestReturn.ERR_USER_DELETE:
                    return 0x00000006;
                case Constants.RestReturn.ERR_INVALID_PASS:
                    return 0x00000007;
                case Constants.RestReturn.ERR_USER_UPDATE:
                    return 0x00000008;
                case Constants.RestReturn.ERR_LINK:
                    return 0x00000009;
                case Constants.RestReturn.ERR_DEVICE_NOT_FOUND:
                    return 0x0000000A;
                case Constants.RestReturn.ERR_DEVICE_DISABLED:
                    return 0x0000000B;
                case Constants.RestReturn.ERR_INVALID_GRANTER:
                    return 0x0000000C;
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
                case Constants.RestReturn.ERR_USER_NOT_FOUND:
                    return "Error user not found";
                case Constants.RestReturn.ERR_USER_DELETE:
                    return "Error during user deletion";
                case Constants.RestReturn.ERR_INVALID_PASS:
                    return "Error invalid password";
                case Constants.RestReturn.ERR_USER_UPDATE:
                    return "Error during user update";
                case Constants.RestReturn.ERR_LINK:
                    return "Error during linking/link refresh";
                case Constants.RestReturn.ERR_DEVICE_NOT_FOUND:
                    return "Error device not found";
                case Constants.RestReturn.ERR_DEVICE_DISABLED:
                    return "Error device disabled";
                case Constants.RestReturn.ERR_INVALID_GRANTER:
                    return "Error invalid granter";
                default:
                    return "Unknown erorr";
            }
        }
    }
    #endregion
}