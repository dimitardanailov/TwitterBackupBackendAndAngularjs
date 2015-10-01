namespace DataStructure
{
    public static class TwitterSettings
    {
        public readonly static string ConsumerKey = ClientConfigurations.AppSettings.Setting<string>("TwitterConsumerKey");
        public readonly static string ConsumerSecret = ClientConfigurations.AppSettings.Setting<string>("TwitterConsumerSecret");

        public const string AccessTokenClaimType = "urn:tokens:twitter:accesstoken";
        public const string AccessTokenSecretClaimType = "urn:tokens:twitter:accesstokensecret";
    }
}
