namespace ConnectSDK.Voice.Calls
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Voice.Models.V2.Calls;

    public interface INotificationHub
    {
        Task StartHubConnectionAsync(string uri, Action<CallEvent> eventHandler);

        Task<Hub> CreateSubscription();

        Task DeleteSubscription(string id);

        Task<Hub> RenewSubscription(string id);
    }
}
