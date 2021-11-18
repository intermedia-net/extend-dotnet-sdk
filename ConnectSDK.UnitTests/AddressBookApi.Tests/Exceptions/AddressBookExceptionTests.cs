namespace ConnectSDK.UnitTests.AddressBookApi.Tests.Exceptions
{
    using System.Net;
    using System.Threading.Tasks;
    using AutoFixture.Xunit2;
    using ConnectSDK.AddressBook;
    using ConnectSDK.AddressBook.Models.V3;
    using ConnectSDK.Common.Exceptions;
    using Xunit;
    using Xunit.Abstractions;
    using ConnectSDK.UnitTests.Common;

    public class AddressBookExceptionTests : TestBase
    {
        private readonly IAddressBookClient addressBookClient;

        public AddressBookExceptionTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            var tokenProvider = new AuthMock();
            this.addressBookClient = new AddressBookClient(tokenProvider.GetToken);
        }
        
        [Theory, AutoData]
        internal async Task ShouldBeAbleToHandleUnauthorizedExceptionWhenGetContacts(Fields fields)
        {
            // Arrange
            var expectedMessage = new AuthorizeException().Message;
            this.MockHttpResponse(this.addressBookClient, "", HttpStatusCode.Unauthorized);

            // Act
            try
            {
                var result = await this.addressBookClient.GetContacts(fields);

                // Assert
                Assert.True(false);
            }
            catch (AuthorizeException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }
        [Theory, AutoData]
        internal async Task ShouldBeAbleToHandleForbiddenExceptionWhenGetContacts(Fields fields)
        {
            // Arrange
            var expectedMessage = new ForbiddenException().Message;
            this.MockHttpResponse(this.addressBookClient, "", HttpStatusCode.Forbidden);

            // Act
            try
            {
                var result = await this.addressBookClient.GetContacts(fields);

                // Assert
                Assert.True(false);
            }
            catch (ForbiddenException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }
        
        [Theory, AutoData]
        internal async Task ShouldBeAbleToHandleInternalFaultExceptionWhenGetContacts(Fields fields)
        {
            // Arrange
            var expectedMessage = new InternalFaultException().Message;
            this.MockHttpResponse(this.addressBookClient, "", HttpStatusCode.InternalServerError);

            // Act
            try
            {
                var result = await this.addressBookClient.GetContacts(fields);

                // Assert
                Assert.True(false);
            }
            catch (InternalFaultException e)
            {
                // Assert
                Assert.Equal(expectedMessage, e.Message);
            }
        }
    }
}
