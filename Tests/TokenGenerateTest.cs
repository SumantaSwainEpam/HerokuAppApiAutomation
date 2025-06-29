using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerokuAppApiAutomation.Clients;

namespace HerokuAppApiAutomation.Tests
{
   
    public class TokenGenerateTest:BaseTest
    {
      
        private TokenGenerate Token => CreateClient<TokenGenerate>();

        [Test, Order(1), Description("Valid credentials should return a non-empty token")]
        public void GenerateToken_Should_Return_ValidToken()
        {
            var token = Token.GenerateToken();
            Assert.That(token, Is.Not.Null.And.Not.Empty, "Token should not be null or empty.");
            TestContext.WriteLine("Generated Token: " + token);
        }
    }

}

