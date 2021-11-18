namespace ConnectSDK.Voice.VoiceMails
{
    using System.Threading.Tasks;
    using ConnectSDK.Voice.Models.V2.VoiceMails;
    using ConnectSDK.Voice.Models.V2.VoiceMailsSettings;

    public interface IVoiceMailsClient
    {
        Task<VoiceMails> GetUserVoiceMails(int offset = 0, int countOnList = 20);

        Task DeleteVoicemailRecords(Status? status = null);

        Task UpdateVoicemailRecordsStatus(SelectedVoiceMailsBody statusBody);

        Task DeleteSelectedVoicemailRecords(SelectedIdsBody selectedIdsBody);

        Task UpdateSelectedVoicemailRecordsStatus(SelectedVoiceMailsBody statusBody);

        Task<VoiceMailTotal> GetUserVoicemailsTotal(Status? status = null);

        Task<VoiceMail> GetVoicemailRecord(string id);

        Task<Transcript> GetVoicemailTranscript(string id);

        Task<byte[]> GetVoicemailContent(string id, Format format);
    }
}
