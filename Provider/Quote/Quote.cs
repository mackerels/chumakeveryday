using Newtonsoft.Json;

namespace CoreSandbox.Provider.Quote
{
    public class Quote
    {
        [JsonProperty("quoteText")]
        public string Text { get; set; }

        [JsonProperty("quoteAuthor")]
        public string Author { get; set; }

        [JsonProperty("senderName")]
        public string Name { get; set; }

        [JsonProperty("senderLink")]
        public string SenderLink { get; set; }

        [JsonProperty("quoteLink")]
        public string QuoteLink { get; set; }
    }
}