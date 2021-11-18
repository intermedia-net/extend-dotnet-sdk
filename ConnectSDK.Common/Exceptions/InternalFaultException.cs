namespace ConnectSDK.Common.Exceptions
{
    public class InternalFaultException : ConnectSdkTransientException
    {
        public InternalFaultException()
            : base("Error while response Extend API that response 500 status. "
                   + "Internal Server Error.")
        {
        }
    }
}
