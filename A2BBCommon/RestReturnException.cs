using System;

namespace A2BBCommon
{
    public class RestReturnException : Exception
    {
        public Constants.RestReturn Value { get; private set; } 

        public RestReturnException(Constants.RestReturn value)
        {
            this.Value = value;
        }
    }
}