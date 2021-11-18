namespace ConnectSDK.Meetings
{
    using System.Threading.Tasks;
    using ConnectSDK.Meetings.Models.V1;

    public interface IMeetingsClient
    {
        Task<UserDetails> GetUserDetails();

        Task<MeetingInfo> StartNewMeetingWithDetails();

        Task<DetailedMeetingInfo> GetMeetingDetails(string meetingCode);
    }
}
