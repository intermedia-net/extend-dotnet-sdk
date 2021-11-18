namespace ConnectSDK.Analytics
{
    using System;
    using System.Threading.Tasks;
    using ConnectSDK.Analytics.Models;

    public interface IAnalyticsClient
    {
        Task<DetailedCalls> GetDetailedCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? offset = null,
            int? size = null,
            int? accountId = null,
            GetDetailedCallsBody filters = null);

        Task<DetailedCalls> GetUserCalls(
            DateTime dateFrom,
            DateTime dateTo,
            int? accountId = null,
            GetUserCallsBody getUserCallsBody = null);

        Task<UserFilters[]> GetUserFilters(
            DateTime dateFrom,
            DateTime dateTo,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistory(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistory(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistoryForAllCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistoryForAllCalls(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistoryForDirectCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistoryForDirectCalls(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistoryForGroupCalls(
            DateTime dateFrom,
            DateTime dateTo,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);

        Task<DetailedCalls> GetUsageHistoryForGroupCalls(
            DateTime dateFrom,
            DateTime dateTo,
            UsageHistoryBody filters,
            string sortColumn = null,
            bool? descending = null,
            int? accountId = null);
    }
}
