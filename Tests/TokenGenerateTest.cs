using HerokuAppApiAutomation.Clients;
using HerokuAppApiAutomation.Models;
using Newtonsoft.Json;
using System.Net;

namespace HerokuAppApiAutomation.Tests
{
    [TestFixture]
    public class TokenGenerateTest : BaseTest
    {
        private TokenGenerate _tokenClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _tokenClient = CreateClient<TokenGenerate>();
        }

        [Test]
        [Order(1)]
        [Category("Token Generation")]
        [Description("Valid credentials should return a non-empty token.")]
        public void GenerateToken_Should_Return_ValidToken()
        {
            var token = _tokenClient.GenerateToken();
            Assert.That(token, Is.Not.Null.And.Not.Empty, "Token should not be null or empty.");
            TestContext.WriteLine("Generated Token: " + token);
        }

        [Test]
        [Category("Token Generation")]
        [Description("Invalid credentials should return 200 with Bad credentials reason and no token.")]
        public void GenerateToken_WithInvalidCredentials_ShouldReturnBadCredentials()
        {
            var response = _tokenClient.ExecuteGenerateToken("invalid_user", "wrong_password");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),
                "Auth endpoint always returns 200 even for bad credentials.");

            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content!);
            Assert.That(authResponse, Is.Not.Null);
            Assert.That(authResponse!.Reason, Is.EqualTo("Bad credentials"),
                "Response should contain Bad credentials reason.");
            Assert.That(authResponse.Token, Is.Null.Or.Empty,
                "Token should not be issued for invalid credentials.");
        }
    }
}
