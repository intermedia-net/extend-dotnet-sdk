namespace ConnectSDK.Common.Exceptions
{
    using System;

    public class NetworkErrorException : ConnectSdkTransientException
    {
        public NetworkErrorException()
            : base("Error while response Extend API that response 502 status. "
                   + "Network Error.")
        {
        }

        public NetworkErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
