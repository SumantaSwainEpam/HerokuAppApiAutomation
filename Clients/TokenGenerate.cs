using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using HerokuAppApiAutomation.Models;

namespace HerokuAppApiAutomation.Clients
{
    public class TokenGenerate:BaseClient
    {
        
        public  string GenerateToken()
        {
            var request = CreateRequest("/auth", Method.Post);
           
            request.AddJsonBody(new AuthRequest{

                Username = Helpers.Config.GetUsername(),
                Password = Helpers.Config.GetPassword()

            });

            var response = _client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Auth request failed.");

            var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content);

           
            if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
            {
                throw new InvalidOperationException("Token not found in response.");
            }

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK),"Token is not found!");

            return authResponse.Token;


        }
    }
}
