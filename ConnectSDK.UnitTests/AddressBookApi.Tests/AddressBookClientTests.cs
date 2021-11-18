namespace ConnectSDK.UnitTests.AddressBookApi.Tests
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using ConnectSDK.AddressBook;
    using ConnectSDK.AddressBook.Models.V3;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.UnitTests.Common;
    
    public class AddressBookClientTests: TestBase
    {
        private readonly IAddressBookClient addressBookClient;

        public AddressBookClientTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.addressBookClient = new AddressBookClient(tokenProvider.GetToken);
        }
        
        [Theory, AutoData]
        public async Task ShouldBeAbleToGetContacts(Fields fields, Scope scope, string query, Contacts response)
        {
            // Arrange
            MockHttpResponse(this.addressBookClient,JsonSerializer.Serialize(response));

            // Act
            var result =  await this.addressBookClient.GetContacts(fields, scope, query);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Results[0].Id, result.Results[0].Id);
        }
        
        [Theory, AutoData]
        public async Task ShouldBeAbleToGetUserDetails(Fields fields, Contact response)
        {
            // Arrange
            MockHttpResponse(this.addressBookClient,JsonSerializer.Serialize(response));

            // Act
            var result =  await this.addressBookClient.GetUserDetails(fields);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Id, result.Id);
        }
        
        [Theory, AutoData]
        public async Task ShouldBeAbleToGetContactsByJIDs(string[] jids, Fields fields, Contacts response)
        {
            // Arrange
            MockHttpResponse(this.addressBookClient,JsonSerializer.Serialize(response));

            // Act
            var result =  await this.addressBookClient.GetContactsByJIDs(jids, fields);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.NextPageOffsetToken, result.NextPageOffsetToken);
        }
        
        
        [Theory, AutoData]
        public async Task ShouldBeAbleToGetSingleContact(string id, Fields fields, Contact response)
        {
            // Arrange
            MockHttpResponse(this.addressBookClient,JsonSerializer.Serialize(response));

            // Act
            var result =  await this.addressBookClient.GetSingleContact(id, fields);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Id, result.Id);
        }
        
        [Theory, AutoData]
        public async Task ShouldBeAbleToGetAvatar(string avatarId, Avatar response)
        {
            // Arrange
            MockHttpResponse(this.addressBookClient,JsonSerializer.Serialize(response));

            // Act
            var result =  await this.addressBookClient.GetAvatar(avatarId);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.AvatarId, result.AvatarId);
        }
        
        [Theory, AutoData]
        public async Task ShouldBeAbleToGetMultipleAvatars(string[] avatarIds, Avatars response)
        {
            // Arrange
            MockHttpResponse(this.addressBookClient,JsonSerializer.Serialize(response));

            // Act
            var result =  await this.addressBookClient.GetMultipleAvatars(avatarIds);

            // Assert
            // Check that httpClient was called
            Assert.NotNull(this.LatestHttpRequestFromMock);
            // Check that hardcoded response was deserialized
            Assert.Equal(response.Results[0].AvatarId, result.Results[0].AvatarId);
        }
    }
}
