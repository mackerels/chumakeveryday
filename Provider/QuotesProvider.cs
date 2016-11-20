using System.Net.Http;
using System.Threading.Tasks;
using CoreSandbox.Config;
using Newtonsoft.Json;

namespace CoreSandbox.Provider
{
    public static class QuotesProvider
    {
        private static readonly HttpClient Client;

        static QuotesProvider()
        {
            Client = new HttpClient();
        }

        public static Task<Quote.Quote> GetRandomQuote()
        {
            var promise = new TaskCompletionSource<Quote.Quote>();

            Client.GetAsync(Configurator.Config.QuoteProvider)
                .ContinueWith(response => response.Result.Content.ReadAsStringAsync()
                    .ContinueWith(x => promise.SetResult(JsonConvert.DeserializeObject<Quote.Quote>(x.Result))));

            return promise.Task;
        }
    }
}