using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuAppApiAutomation.Clients
{
    public class BaseClient
    {
        
        protected readonly RestClient _client;
        
        protected BaseClient()
        {
            var option = new RestClientOptions(Helpers.Config.GetEndpoint())
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromSeconds(10)
            };  

            _client = new RestClient(option);
                    
        }


        /// <summary>
        /// Create a RestRequest with the specified resource and method.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="method"></param>
        /// <param name="withAuth"></param>
        /// <returns></returns>
        protected RestRequest CreateRequest(string resource, Method method , bool withAuth=false)
        {
            var request = new RestRequest(resource, method);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Cache-Control", "no-cache");
            if (withAuth)
            {
                string token = new TokenGenerate().GenerateToken();
                request.AddHeader($"Cookie", $"token={token}");
            }

            return request;

        }
    }
}
