using RestSharp;
using System.Net;
using Newtonsoft.Json;
using HerokuAppApiAutomation.Models;

namespace HerokuAppApiAutomation.Clients
{
    public class TokenGenerate : BaseClient
    {
        public string GenerateToken()
        {
            var response = ExecuteGenerateToken(Helpers.Config.GetUsername(), Helpers.Config.GetPassword());

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Auth request failed.");

            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content!);
            if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
                throw new InvalidOperationException("Token not found in response.");

            return authResponse.Token;
        }

        public RestResponse ExecuteGenerateToken(string username, string password)
        {
            var request = CreateRequest("/auth", Method.Post);
            request.AddJsonBody(new AuthRequest { Username = username, Password = password });
            return _client.Execute(request);
        }
    }
}
