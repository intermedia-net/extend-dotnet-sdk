
namespace ConnectSDK.Common
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ConnectSDK.Common.Exceptions;
    using ConnectSDK.Common.Extensions;

    public class RequestFactory : IRequestFactory
    {
        private readonly JsonSerializerOptions jsonSerializerOptions;

        // `readonly` is omitted to be able change field during unit tests run.
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private readonly HttpClient httpClient;

        public RequestFactory()
        {
            this.jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            this.httpClient = this.CreateHttpClient();
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var httpResponseMessage = await this.httpClient.GetAsync(uri);
                return await httpResponseMessage.ProcessResponseMessage<T>();
            }
            catch (HttpRequestException e)
            {
                throw new NetworkErrorException("Error while response Extend API. An HttpRequestException was thrown.", e);
            }
        }

        public async Task<T> PostAsync<T>(string uri, object body, string contentType = "application/json")
        {
            try
            {
                var serializedBody = body == null ? string.Empty : JsonSerializer.Serialize(body, this.jsonSerializerOptions);
                var content = new StringContent(serializedBody, Encoding.UTF8, contentType);
                var httpResponseMessage = await this.httpClient.PostAsync(uri, content);
                return await httpResponseMessage.ProcessResponseMessage<T>();
            }
            catch (HttpRequestException e)
            {
                throw new NetworkErrorException("Error while response Extend API. An HttpRequestException was thrown.", e);
            }
        }

        public async Task PostAsync(string uri, object body, string contentType = "application/json")
        {
            try
            {
                var serializedBody = body == null ? string.Empty : JsonSerializer.Serialize(body, this.jsonSerializerOptions);
                var content = new StringContent(serializedBody, Encoding.UTF8, contentType);
                var httpResponseMessage = await this.httpClient.PostAsync(uri, content);
                await httpResponseMessage.ProcessResponseMessage();
            }
            catch (HttpRequestException e)
            {
                throw new NetworkErrorException("Error while response Extend API. An HttpRequestException was thrown.", e);
            }
        }

        public async Task DeleteAsync(string uri, object body)
        {
            try
            {
                var content = JsonSerializer.Serialize(body, this.jsonSerializerOptions);

                var httpResponseMessage = await this.httpClient.SendAsync(
                        new HttpRequestMessage(HttpMethod.Delete, uri)
                        {
                            Content = new StringContent(content, Encoding.UTF8, "application/json")
                        })
                    ;
                await httpResponseMessage.ProcessResponseMessage();
            }
            catch (HttpRequestException e)
            {
                throw new NetworkErrorException("Error while response Extend API. An HttpRequestException was thrown.", e);
            }
        }

        public async Task DeleteAsync(string uri)
        {
            try
            {
                var httpResponseMessage = await this.httpClient.DeleteAsync(uri);
                await httpResponseMessage.ProcessResponseMessage();
            }
            catch (HttpRequestException e)
            {
                throw new NetworkErrorException("Error while response Extend API. An HttpRequestException was thrown.", e);
            }
        }

        public async Task<T> DeleteAsync<T>(string uri)
        {
            try
            {
                var httpResponseMessage = await this.httpClient.DeleteAsync(uri);
                return await httpResponseMessage.ProcessResponseMessage<T>();
            }
            catch (HttpRequestException e)
            {
                throw new NetworkErrorException("Error while response Extend API. An HttpRequestException was thrown.", e);
            }
        }

        public void SetToken(string token)
        {
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void SetUserAgent(string appName, string appVersion)
        {
            if (!string.IsNullOrEmpty(appName))
            {
                this.httpClient.DefaultRequestHeaders.UserAgent.ParseAdd($"{appName}/{appVersion}");
            }
        }

        private HttpClient CreateHttpClient(HttpMessageHandler handler = null)
        {
            var httpClient = handler is null ? new HttpClient() : new HttpClient(handler);
            httpClient.BaseAddress = new Uri("https://api.intermedia.net/");

            return httpClient;
        }
    }
}
