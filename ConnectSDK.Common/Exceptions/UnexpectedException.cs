namespace ConnectSDK.Common.Exceptions
{
    using System.Net;

    public class UnexpectedException :  ConnectSdkBaseException
    {
        public UnexpectedException(HttpStatusCode httpStatusCode, string content)
            : base($"Error while response Extend API that response {httpStatusCode:D} status. "
                   + $"See content for details. Content: {content}")
        {
        }
    }
}
