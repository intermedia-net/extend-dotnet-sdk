namespace ConnectSDK.AddressBook
{
    using System.Threading.Tasks;
    using ConnectSDK.AddressBook.Models.V3;

    public interface IAddressBookClient
    {
        Task<Contacts> GetContacts(Fields fields = Fields.All, Scope scope = Scope.Public, string query = null, string phone = null);

        Task<Contact> GetUserDetails(Fields fields = Fields.All);

        Task<Contacts> GetContactsByJIDs(string[] ids, Fields fields = Fields.All);

        Task<Contact> GetSingleContact(string id, Fields fields = Fields.All);

        Task<Avatar> GetAvatar(string avatarId);

        Task<Avatars> GetMultipleAvatars(string[] avatarIds);
    }
}
