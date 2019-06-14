namespace ViiaSample
{
    public class SiteOptions
    {
        public ViiaOptions Viia { get; set; }
        public SendGridOptions SendGrid { get; set; }
    }

    public class ViiaOptions
    {
        public string BaseAppUrl { get; set; }
        public string BaseApiUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string LoginCallbackUrl { get; set; }
    }

    public class SendGridOptions
    {
        public string ApiKey { get; set; }
        public string EmailFrom { get; set; }
        public string NameFrom { get; set; }
    }
}