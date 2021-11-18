namespace ConnectSDK.Voice.CallRecordings
{
    using System.Threading.Tasks;
    using ConnectSDK.Voice.Models.V2.CallRecordings;

    public interface ICallRecordingsClient
    {
        Task<CallRecordings> GetCallRecordings(
            string uuid,
            int? offset = null,
            int? count = null);

        Task<byte[]> GetCallRecordingContent(
            string uuid,
            int id);

        Task<byte[]> GetCallRecordingArchive(
            string uuid,
            int[] ids,
            string format = "zip");
    }
}
