namespace ConnectSDK.Common.Exceptions
{
    public class ItemNotFoundException :  ConnectSdkBaseException
    {
        public ItemNotFoundException()
            : base("Error while response Extend API that response 404 status. "
                   + "Resource not found.")
        {
        }
    }
}
