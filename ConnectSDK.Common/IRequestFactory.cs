namespace ConnectSDK.Common
{
    using System.Threading.Tasks;

    public interface IRequestFactory
    {
        Task<T> GetAsync<T>(string uri);

        Task<T> PostAsync<T>(string uri, object body, string contentType = "application/json");

        Task PostAsync(string uri, object body, string contentType = "application/json");

        Task DeleteAsync(string uri, object body);

        Task DeleteAsync(string uri);

        Task<T> DeleteAsync<T>(string uri);

        void SetToken(string token);

        void SetUserAgent(string appName, string appVersion);
    }
}
