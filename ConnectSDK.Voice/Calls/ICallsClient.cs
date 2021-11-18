namespace ConnectSDK.Voice.Calls
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Voice.Models.V2.Calls;

    public interface ICallsClient
    {
        Task<Hub> CreateSubscription(Action<CallEvent> eventHandler);

        Task DeleteSubscription(string id);

        Task<Hub> RenewSubscription(string id);

        Task<MakeCallResponse> MakeCall(MakeCallBody body);

        Task<CallResponse> TerminateCall(string callId, string commandId = null);

        Task<CallResponse> CancelCall(string callId, CancelCallBody body = null);

        Task<CallResponse> MergeCall(string callId, MergeCallBody body);

        Task<CallResponse> TransferCall(string callId, TransferCallBody body);

        Task<DevicesResponse> GetDevices();
    }
}
