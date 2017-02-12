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
        /// The OAuth client id.
        /// </summary>
        public const string OAUTH_CLIENT_ID = "oauth";

        /// <summary>
        /// Name of A2BB API resource.
        /// </summary>
        public const string A2BB_API_RESOURCE_NAME = "A2BB_API";
        #endregion
    }
}