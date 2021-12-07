# Intermedia Connect SDK for .NET
​
You can use this C# client library to integrate Intermedia APIs into your application. To do this, you will need an Intermedia account.
Before you start, we recommend that you visit our [Developer Portal](https://developer.intermedia.com/).
​
## Table of Contents

* [Targeted Frameworks](#targeted-frameworks)
* [Installation](#installation)
* [Supported APIs](#supported-apis)
* [Authorization](#authorization)
  * [Client Credentials Flow (S2S)](#client-credentials-flow-(s2s))
  * [Authorization Code Flow (Confidential client)](#authorization-code-flow-(confidential-client))
  * [Authorization Code Flow with PKCE (Public client)](#authorization-code-flow-with-pkce-(public-client))
  * [Authorization router](#authorization-router)
* [Examples](#examples)
  * [Preparation](#preparation)
  * [Meeting API](#meeting-api)
  * [Analytics API](#analytics-api)
  * [Address Book API](#address-book-api)
  * [Voice API](#voice-api)
* [License](#license)

## Targeted Frameworks

The SDK is compiled with .NET Standard 2.0 target. You can use the libraries with .NET 4.6.1 and above.
.NET Core is also supported starting from version 2.0. Learn more about the compatibilities at [Microsoft Docs](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

## Installation

The SDK consists of several packages, which are available at NuGet.

* Intermedia.SDK.Voice provides a client for [Voice API](https://developer.intermedia.com/api/spec/calling/index.html)
* Intermedia.SDK.Meetings provides a client for [Meetings API](https://developer.intermedia.com/api/spec/meeting/index.html)
* Intermedia.SDK.Analytics provides a client for [Analytics API](https://developer.intermedia.com/api/spec/analytics/index.html)
* Intermedia.SDK.Address Book provides a client for [Address Book API](https://developer.intermedia.com/api/spec/address_book/index.html)
* Intermedia.SDK provides all functionality

To install the C# client library with the full functionality using NuGet, run the following command from the terminal in your projects directory:

``` cmd
dotnet add package Intermedia.SDK
```

Or run the following command in the Package Manager Console:

``` cmd
Install-Package Intermedia.SDK
```

If you prefer to run commands directly from source:

* clone this repository `git clone https://github.com/intermedia-net/extend-dotnet-sdk`;
* add the extend-dotnet-sdk/ConnectSDK/ConnectSDK.csproj file to your .sln file;
* add the extend-dotnet-sdk/ConnectSDK/ConnectSDK.csproj file as a project dependency.

```xml
<ItemGroup>
    <ProjectReference Include="../extend-dotnet-sdk/ConnectSDK/ConnectSDK.csproj" />
</ItemGroup>
```

Our experienced clients are recommended to install only the required libraries to decrease the size of an application result bundle. For example, if you are going to use the Analytics API, install Intermedia.SDK.Analytics only.

## Supported APIs

Below is the list of Intermedia APIs and the information on their support by Intermedia .NET SDK.

| API                | API Release Status   |  Supported?   |
|--------------------|:--------------------:|:-------------:|
| Meeting API        | General Availability |      ✅       |
| Analytics API      | General Availability |      ✅       |
| Address Book API   | General Availability |      ✅       |
| Voice API          | General Availability |      ✅       |
| Contact Center API | In Development       |      ❌       |

## Authorization

Intermedia uses JWT tokens to get access to the API. If you are going to use a full SDK package, you will be provided with a generic `IAuthorizationTokenProvider` interface. 
If you want to use a specified package for an API, you should provide an authorization delegate.
``` csharp
var token = "YOUR_JWT_TOKEN";
Func<string, Task<string>> tokenProvider = scope => Task.FromResult(token);
var addressBookClient = new AddressBookClient(tokenProvider);
```
You can use our ConnectSDK.Auth package to get the target token provider. Our implementation is used for prototypes and examples. We strongly recommend that you create your own implementation of the interface.

### Client Credentials Flow (S2S)

First, you need to create an instance of the ClientCredentialsConfiguration class:
``` csharp
var clientCredsConfig = new ClientCredentialsConfiguration(
    authBaseUrl: "AUTH_BASE_URL",
    clientId: "CLIENT_ID",
    clientSecret: "CLIENT_SECRET");
```
After this step is completed, you will be able to get the token provider and pass it to the API client:
``` csharp
var clientCredsTokenProvider = Authorization.BuildAuthorizationTokenProvider(clientCredsConfig);
var uniteClient = new UnifiedClient(clientCredsTokenProvider);
```

### Authorization Code Flow (Confidential Client)

First, you need to create an instance of the AuthorizationCodeConfiguration class:
``` csharp
var authCodeConfig = new AuthorizationCodeConfiguration(
    authBaseUrl: "AUTH_BASE_URL",
    clientId: "CLIENT_ID",
    clientSecret: "CLIENT_SECRET",
    redirectUrl: "REDIRECT_URL",
    deviceId: "DEVICE_ID");
```
You also need to obtain the authorization code yourself using the "/authorize" endpoint.
After this step is completed, you will be able to get the token provider and pass it to the API client:
``` csharp
var authorizationCode = "YOUR_AUTHORIZATION_CODE";
var authCodeTokenProvider = await Authorization.BuildAuthorizationTokenProvider(authCodeConfig, authorizationCode);
var uniteClient = new UnifiedClient(authCodeTokenProvider);
```

### Authorization Code Flow With PKCE (Public Client)

The token provider implementation of the Authorization Code Flow with PKCE is not supported in the current version of the ConnectSdk.Auth package.

### Authorization Router

If you want to use several different OAuth flows in your project, refer to the AuthorizationRouterTokenProvider class by passing token providers to it:
``` csharp
var tokenRouter = new AuthorizationRouterTokenProvider(clientCredsTokenProvider, authCodeTokenProvider);   
```
Afterwards, you will be able to pass them to the API client.
``` csharp
var uniteClient = new UnifiedClient(tokenRouter);
```

## Examples

### Preparation

``` csharp
var token = "YOUR_JWT_TOKEN";
var tokenProvider = new SimpleAuthorizationTokenProvider(token);
var unifiedClient = new UnifiedClient(tokenProvider);
```

### Meeting API

#### Get User Details

``` csharp
var meetingsClient = unifiedClient.MeetingsClient;
var userDetails = await meetingsClient.GetUserDetails();
```

#### Start New Meeting With Details

``` csharp
var meetingInfo = await meetingsClient.StartNewMeetingWithDetails();
```

#### Get Meeting Details

``` csharp
var detailedMeetingInfo = await meetingsClient.GetMeetingDetails(meetingInfo.Data.MeetingCode);
```

Please visit our [Developer Portal](https://developer.intermedia.com/api/spec/meeting/index.html) for more information.

### Address Book API

#### Get User Details

``` csharp
var addressBookClient = unifiedClient.AddressBookClient;
var userDetails = await addressBookClient.GetUserDetails();
```

#### Get Contacts

``` csharp
var contacts = await addressBookClient.GetContacts();
```

#### Get Avatar

``` csharp
var avatarId = contacts.Results[0].AvatarId;
var avatar = await addressBookClient.GetAvatar(avatarId);
```

Please visit our [Developer Portal](https://developer.intermedia.com/api/spec/address_book/index.html) for more information.

### Analytics API

#### Get Detailed Calls

``` csharp
var analyticsClient = unifiedClient.AnalyticsClient;

var dateFrom = new DateTime(2021, 01, 01);
var dateTo = new DateTime(2021, 02, 01);
var detailedCalls = await analyticsClient.GetDetailedCalls(dateFrom, dateTo);
```

#### List Usage History With Optional Filters

``` csharp
var dateFrom = new DateTime(2021, 01, 01);
var dateTo = new DateTime(2021, 02, 01);
var filters = new UsageHistoryBody
{
    CallAttributes = new[] { "outbound" }
};

var usageHistory = await analyticsClient.GetUsageHistory(dateFrom, dateTo, filters);
```

Please visit our [Developer Portal](https://developer.intermedia.com/api/spec/analytics/index.html) for more information.

### Voice API

#### Make Call

``` csharp
var callsClient = unifiedClient.VoiceClient.CallsClient;

var devices = await callsClient.GetDevices();
var deviceId = devices.ClickToCallDevices[0].Id;
var phoneNumber = "5551000";

var makeCallBody = new MakeCallBody(deviceId, phoneNumber);
var makeCallResponse = await callsClient.MakeCall(makeCallBody);
```

#### Terminate Call

``` csharp
var callId = makeCallResponse.CallId;
var terminateResponse = await callsClient.TerminateCall(callId);
```

#### Getting Events About Calls From Server

``` csharp
Action<CallEvent> eventHandler = callEvent => 
    Console.WriteLine($"A call status for a call with {callEvent.CallId} was changed on {callEvent.EventType}");

var hub = callsClient.CreateSubscription(eventHandler);
```

Please visit our [Developer Portal](https://developer.intermedia.com/api/spec/calling/index.html) for more information about Voice API.

## License

These libraries are released under the MIT License.