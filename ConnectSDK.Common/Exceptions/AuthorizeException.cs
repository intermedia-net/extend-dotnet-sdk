namespace ConnectSDK.Common.Exceptions
{
    public class AuthorizeException :  ConnectSdkBaseException
    {
        public AuthorizeException()
            : base("Error while response Extend API that response 401 status. "
                   + "Access Denied.")
        {
        }
    }
}
