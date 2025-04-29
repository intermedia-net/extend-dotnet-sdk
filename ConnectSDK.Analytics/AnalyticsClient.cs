
namespace ConnectSDK.Analytics
{
    using System;
    using System.Threading.Tasks;
    using System.Web;
    using ConnectSDK.Analytics.Models;
    using ConnectSDK.Common;
    using ConnectSDK.Common.Extensions;

    public class AnalyticsClient : IAnalyticsClient
    {
        private readonly IRequestFactory requestFactory = new RequestFactory();
        private readonly Func<string, Task<string>> getToken;

        public AnalyticsClient(Func<string, Task<string>> getToken)
        {
            this.getToken = getToken;
        }

        public AnalyticsClient(Func<string, Task<string>> getToken, ConnectSdkConfig config)
        {
            this.getToken = getToken;
            this.requestFactory.SetUserAgent(config.AppName, config.AppVersion);
        }

        public async Task<DetailedCalls> GetDetailedCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? offset = null,
            int? size = null,
            int? accountId = null,
            GetDetailedCallsBody filters = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/calls/call/detail";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (offset != null)
            {
                urlParams.Add("offset", offset.ToString());
            }

            if (size != null)
            {
                urlParams.Add("size", size.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<DetailedCalls>(fullUrl, filters);
        }

        public async Task<DetailedCalls> GetUserCalls(
            DateTime dateFrom,
            DateTime dateTo,
            int? accountId = null,
            GetUserCallsBody getUserCallsBody = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/calls/user";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<DetailedCalls>(fullUrl, getUserCallsBody);
        }

        public async Task<UserFilters[]> GetUserFilters(
            DateTime dateFrom,
            DateTime dateTo,
            int? accountId = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/calls/user/filters";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<UserFilters[]>(fullUrl, null);
        }

        public async Task<DetailedCalls> GetUsageHistory(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<DetailedCalls>(fullUrl);
        }

        public async Task<DetailedCalls> GetUsageHistory(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            if(filters == null){ throw new ArgumentNullException(nameof(filters)); }
            
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<DetailedCalls>(fullUrl, filters);
        }

        public async Task<DetailedCalls> GetUsageHistoryForAllCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory/calls";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<DetailedCalls>(fullUrl);
        }

        public async Task<DetailedCalls> GetUsageHistoryForAllCalls(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            if(filters == null){ throw new ArgumentNullException(nameof(filters)); }
            
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory/calls";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<DetailedCalls>(fullUrl, filters);
        }

        public async Task<DetailedCalls> GetUsageHistoryForDirectCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory/calls/direct";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<DetailedCalls>(fullUrl);
        }

        public async Task<DetailedCalls> GetUsageHistoryForDirectCalls(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            if(filters == null){ throw new ArgumentNullException(nameof(filters)); }
            
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory/calls/direct";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<DetailedCalls>(fullUrl, filters);
        }

        public async Task<DetailedCalls> GetUsageHistoryForGroupCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory/calls/group";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.GetAsync<DetailedCalls>(fullUrl);
        }

        public async Task<DetailedCalls> GetUsageHistoryForGroupCalls(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null)
        {
            if(filters == null){ throw new ArgumentNullException(nameof(filters)); }
            
            var token = await this.getToken(Constants.AnalyticScopes.ApiServiceAnalyticsMain);
            this.SetToken(token);

            var url = "/analytics/usageHistory/calls/group";

            var urlParams = HttpUtility.ParseQueryString(string.Empty);

            urlParams.Add("dateFrom", dateFrom.ToUtcIso8601String());
            urlParams.Add("dateTo", dateTo.ToUtcIso8601String());

            if (sortColumn != null)
            {
                urlParams.Add("sortColumn", sortColumn);
            }

            if (descending != null)
            {
                urlParams.Add("descending", descending.ToString());
            }

            if (accountId != null)
            {
                urlParams.Add("accountId", accountId.ToString());
            }

            var fullUrl = url + "?" + urlParams;

            return await this.requestFactory.PostAsync<DetailedCalls>(fullUrl, filters);
        }

        private void SetToken(string token)
        {
            this.requestFactory.SetToken(token);
        }
    }
}
