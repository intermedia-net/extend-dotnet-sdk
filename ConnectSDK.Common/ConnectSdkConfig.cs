namespace ConnectSDK.Common
{
    public class ConnectSdkConfig
    {
        public string AppName { get; }

        public string AppVersion { get; }

        public ConnectSdkConfig(
            string appName = null,
            string appVersion = null)
        {
            this.AppName = appName;
            this.AppVersion = appVersion;
        }
    }
}
