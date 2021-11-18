namespace ConnectSDK.Common.Exceptions
{
    using System;

    public class ConnectSdkBaseException : Exception
    {
        public ConnectSdkBaseException(string message) : base(message)
        {
        }

        public ConnectSdkBaseException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
