namespace ConnectSDK.Common.Exceptions
{
    using System;

    public class ConnectSdkTransientException :  ConnectSdkBaseException
    {
        public  ConnectSdkTransientException(string message) : base(message)
        {
        }

        public  ConnectSdkTransientException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
