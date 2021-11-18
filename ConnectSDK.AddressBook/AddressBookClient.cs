namespace ConnectSDK.AddressBook
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Threading.Tasks;
    using ConnectSDK.AddressBook.Models.V3;
    using ConnectSDK.Common;
    using System.Web;
    
    public class AddressBookClient : IAddressBookClient
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public AddressBookClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public AddressBookClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
        }

        private NameValueCollection AddUrlParams(Fields fields, Scope? scope = null, string query = null, string phone = null)
        {

            var urlParams = HttpUtility.ParseQueryString(string.Empty);
            
            var fieldsList = new List<string>();

            if(fields.HasFlag(Fields.Id)) {
                fieldsList.Add("id");
            }

            if(fields.HasFlag(Fields.All)) {
                fieldsList.Add("_all");
            }

            if(fields.HasFlag(Fields.LegacyId)) {
                fieldsList.Add("legacyId");
            }

            if(fieldsList.Count > 0)
            {
                urlParams.Add("fields", string.Join(",", fieldsList));
            }

            if (scope == Scope.Public)
            {
                urlParams.Add("scope", "public");
            }
            else if (scope == Scope.All)
            {
                urlParams.Add("scope", "all");
            }

            if (query != null)
            {
                urlParams.Add("query", query);
            }

            if (phone != null)
            {
                urlParams.Add("phone", phone);
            }

            return urlParams;
        }
        public async Task<Contacts> GetContacts(Fields fields = Fields.All, Scope scope = Scope.Public, string query = null, string phone = null)
        {
            if(fields == Fields.LegacyId){ throw new ArgumentException("legacyId value should be accompanied with _all or id", nameof(fields)); }
            if(query != null && phone != null){ throw new ArgumentException("Both 'query' and 'phone' are not allowed."); }

            var token = await this.getToken(Constants.AddressBookScopes.ApiUserAddressBook);
            this.SetToken(token);

            var url = "/address-book/v3/contacts";
            var urlParams = AddUrlParams(fields,scope,query,phone);
            
            var fullUrl = url + "?" + urlParams;
            return await this.requestFactory.GetAsync<Contacts>(fullUrl);
        }
        
        public async Task<Contact> GetUserDetails(Fields fields)
        {
            if(fields == Fields.LegacyId){ throw new ArgumentException("legacyId value should be accompanied with _all or id", nameof(fields)); }

            var token = await this.getToken(Constants.AddressBookScopes.ApiUserAddressBook);
            this.SetToken(token);

            var url = "/address-book/v3/contacts/_me";

            var urlParams = AddUrlParams(fields);
            
            var fullUrl = url + "?" + urlParams;
            return await this.requestFactory.GetAsync<Contact>(fullUrl);
        }

        public async Task<Contacts> GetContactsByJIDs(string[] jids, Fields fields = Fields.All)
        {
            if(fields == Fields.LegacyId){ throw new ArgumentException("legacyId value should be accompanied with _all or id", nameof(fields)); }
            if(jids == null){ throw new ArgumentNullException(nameof(jids)); }
            
            var token = await this.getToken(Constants.AddressBookScopes.ApiUserAddressBook);
            this.SetToken(token);

            var url = "/address-book/v3/contacts/_search";

            var content = new GetContactsByJIDsBody
            {
                JIDs = jids
            };
            
            var urlParams = AddUrlParams(fields);
            var fullUrl = url + "?" + urlParams;
            return await this.requestFactory.PostAsync<Contacts>(fullUrl, content);
        }

        public async Task<Contact> GetSingleContact(string id, Fields fields = Fields.All)
        {
            if(fields == Fields.LegacyId){ throw new ArgumentException("legacyId value should be accompanied with _all or id", nameof(fields)); }
            if(id == null){ throw new ArgumentNullException(nameof(id)); }
            
            var token = await this.getToken(Constants.AddressBookScopes.ApiUserAddressBook);
            this.SetToken(token);

            var url = $"/address-book/v3/contacts/{id}";
            var urlParams = AddUrlParams(fields);
            var fullUrl = url + "?" + urlParams;
            return await this.requestFactory.GetAsync<Contact>(fullUrl);
        }

        public async Task<Avatar> GetAvatar(string avatarId)
        {
            var token = await this.getToken(Constants.AddressBookScopes.ApiUserAddressBook);
            this.SetToken(token);

            var url = $"/address-book/v3/avatars/{avatarId}";

            return await this.requestFactory.GetAsync<Avatar>(url);
        }

        public async Task<Avatars> GetMultipleAvatars(string[] avatarIds)
        {
            if(avatarIds == null){ throw new ArgumentNullException(nameof(avatarIds)); }
            var token = await this.getToken(Constants.AddressBookScopes.ApiUserAddressBook);
            this.SetToken(token);

            var url = "/address-book/v3/avatars/_search";

            var content = new AvatarIdsBody
            {
                AvatarIds = avatarIds
            };

            return await this.requestFactory.PostAsync<Avatars>(url, content);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
