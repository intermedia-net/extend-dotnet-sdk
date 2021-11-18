
namespace ConnectSDK.Common.Extensions
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading.Tasks;
    using ConnectSDK.Common.Exceptions;

    internal static class HttpResponseMessageExtensions
    {
        public static async Task<TResponse> ProcessResponseMessage<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            switch(httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.Found:
                case HttpStatusCode.NotModified:
                    if (typeof(TResponse) == typeof(byte[]))
                    {
                        return (TResponse)(object)(await httpResponseMessage.ProcessSuccessByteResponse());
                    }
                    else
                    {
                        return await httpResponseMessage.ProcessSuccessResponse<TResponse>();
                    }
                        
                case HttpStatusCode.BadRequest:
                    return await httpResponseMessage.ProcessBadRequestResponse<TResponse>();

                case HttpStatusCode.Unauthorized:
                    return await httpResponseMessage.ProcessUnauthorizedResponse<TResponse>();

                case HttpStatusCode.Forbidden:
                    return await httpResponseMessage.ProcessForbiddenResponse<TResponse>();

                case HttpStatusCode.NotFound:
                    return await httpResponseMessage.ProcessNotFoundResponse<TResponse>();

                case HttpStatusCode.InternalServerError:
                    return await httpResponseMessage.ProcessInternalFaultResponse<TResponse>();

                case HttpStatusCode.BadGateway:
                    return await httpResponseMessage.ProcessNetworkErrorResponse<TResponse>();

                default:
                    return await httpResponseMessage.ProcessUnexpectedResponse<TResponse>();
            };
        }

        private static async Task<TResponse> ProcessSuccessResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var response = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(response, jsonSerializerOptions);
        }

        private static Task<byte[]> ProcessSuccessByteResponse(this HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.Content.ReadAsByteArrayAsync();
        }

        private static Task<TResponse> ProcessBadRequestResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            throw new InputParametersException();
        }

        private static Task<TResponse> ProcessUnauthorizedResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            throw new AuthorizeException();
        }

        private static Task<TResponse> ProcessForbiddenResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            throw new ForbiddenException();
        }


        private static async Task<TResponse> ProcessNotFoundResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.Content != null)
            {
                string content;
                try
                {
                    content = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    content = $"Can't read response content: {ex}";
                }

                if (content.Contains($"\"message\": \"Resource not found\""))
                {
                    throw new ItemNotFoundException();
                }
            }
            return (TResponse)(object)null;
        }

        private static Task<TResponse> ProcessInternalFaultResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            throw new InternalFaultException();
        }

        private static Task<TResponse> ProcessNetworkErrorResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            throw new NetworkErrorException();
        }

        private static async Task<TResponse> ProcessUnexpectedResponse<TResponse>(this HttpResponseMessage httpResponseMessage)
        {
            string content = null;
            if (httpResponseMessage.Content != null)
            {
                try
                {
                    content = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    content = $"Can't read response content: {ex}";
                }
            }

            throw new UnexpectedException(httpResponseMessage.StatusCode, content);
        }

        public static async Task ProcessResponseMessage(this HttpResponseMessage httpResponseMessage)
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.NoContent:
                case HttpStatusCode.Found:
                case HttpStatusCode.NotModified:
                    break;

                case HttpStatusCode.BadRequest: 
                    await httpResponseMessage.ProcessBadRequestResponse();
                    break;

                case HttpStatusCode.Unauthorized:
                    await httpResponseMessage.ProcessUnauthorizedResponse();
                    break;

                case HttpStatusCode.Forbidden:
                    await httpResponseMessage.ProcessForbiddenResponse();
                    break;

                case HttpStatusCode.NotFound:
                    await httpResponseMessage.ProcessNotFoundResponse();
                    break;

                case HttpStatusCode.InternalServerError:
                    await httpResponseMessage.ProcessInternalFaultResponse();
                    break;

                case HttpStatusCode.BadGateway:
                    await httpResponseMessage.ProcessNetworkErrorResponse();
                    break;

                default:
                    await httpResponseMessage.ProcessUnexpectedResponse();
                    break;
            };
        }

        private static Task ProcessBadRequestResponse(this HttpResponseMessage httpResponseMessage)
        {
            throw new InputParametersException();
        }

        private static Task ProcessUnauthorizedResponse(this HttpResponseMessage httpResponseMessage)
        {
            throw new AuthorizeException();
        }

        private static Task ProcessForbiddenResponse(this HttpResponseMessage httpResponseMessage)
        {
            throw new ForbiddenException();
        }

        private static async Task ProcessNotFoundResponse(this HttpResponseMessage httpResponseMessage)
        {
            if (httpResponseMessage.Content != null)
            {
                string content;
                try
                {
                    content = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    content = $"Can't read response content: {ex}";
                }

                if (content.Contains($"\"message\": \"Resource not found\""))
                {
                    throw new ItemNotFoundException();
                }
            }
        }

        private static Task ProcessInternalFaultResponse(this HttpResponseMessage httpResponseMessage)
        {
            throw new InternalFaultException();
        }

        private static Task ProcessNetworkErrorResponse(this HttpResponseMessage httpResponseMessage)
        {
            throw new NetworkErrorException();
        }

        private static async Task ProcessUnexpectedResponse(this HttpResponseMessage httpResponseMessage)
        {
            string content = null;
            if (httpResponseMessage.Content != null)
            {
                try
                {
                    content = await httpResponseMessage.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    content = $"Can't read response content: {ex}";
                }
            }

            throw new UnexpectedException(httpResponseMessage.StatusCode, content);
        }
    }
}
