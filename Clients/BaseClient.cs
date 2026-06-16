using RestSharp;

namespace HerokuAppApiAutomation.Clients
{
    public class BaseClient
    {
        protected readonly RestClient _client;

        private static string? _cachedToken;
        private static readonly object _tokenLock = new();

        protected BaseClient()
        {
            var option = new RestClientOptions(Helpers.Config.GetEndpoint())
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromSeconds(30)
            };
            _client = new RestClient(option);
        }

        protected RestRequest CreateRequest(string resource, Method method, bool withAuth = false)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Cache-Control", "no-cache");
            if (withAuth)
            {
                request.AddHeader("Cookie", $"token={GetCachedToken()}");
            }
            return request;
        }

        // Token is cached for the test session so each test doesn't trigger a new auth call.
        private static string GetCachedToken()
        {
            if (_cachedToken is not null) return _cachedToken;
            lock (_tokenLock)
            {
                _cachedToken ??= new TokenGenerate().GenerateToken();
            }
            return _cachedToken;
        }
    }
}
