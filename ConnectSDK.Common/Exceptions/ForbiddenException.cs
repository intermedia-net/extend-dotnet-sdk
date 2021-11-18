namespace ConnectSDK.Common.Exceptions
{
    public class ForbiddenException :  ConnectSdkBaseException
    {
        public ForbiddenException()
            : base("Error while response Extend API that response 403 status. "
                   + "Forbidden, unable to access data for the account.")
        {
        }
    }
}
