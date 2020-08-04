namespace Aiia.Sample
{
    public class SampleOptions
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public HumioOptions Humio { get; set; }
        public string SampleAppUrl { get; set; }
        public SecurityOptions Security { get; set; }
        public ViiaOptions Viia { get; set; }
    }

    public class ConnectionStrings
    {
        public string Main { get; set; }
    }

    public class ViiaOptions
    {
        public string BaseApiUrl { get; set; }
        public string BaseAppUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string LoginCallbackUrl { get; set; }
        public string WebHookSecret { get; set; }
    }

    public class HumioOptions
    {
        public string IngestToken { get; set; }
        public string IngestUrl { get; set; }
    }

    public class SecurityOptions
    {
        public string RefreshToken { get; set; }
        public string TokenKey { get; set; }
    }
}
