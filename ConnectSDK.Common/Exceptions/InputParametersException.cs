namespace ConnectSDK.Common.Exceptions
{
    public class InputParametersException :  ConnectSdkBaseException
    {
        public InputParametersException()
            : base("Error while response Extend API that response 400 status. "
                   + "Bad Request.")
        {
        }
    }
}
