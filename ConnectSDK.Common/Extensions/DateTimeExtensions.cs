namespace ConnectSDK.Common.Extensions
{
    using System;

    public static class DateTimeExtension
    {
        public static string ToUtcIso8601String(this DateTime @this)
        {
            return @this.ToString("yyyy-MM-dd'T'HH:mm:ss.Z");
        }
    }
}
