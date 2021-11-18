namespace ConnectSDK.Common
{
    using System.Threading.Tasks;

    public interface IAuthorizationTokenProvider
    {
        Task<string> GetToken(string scope);
    }
}
