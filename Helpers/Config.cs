using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerokuAppApiAutomation.Helpers
{
    public static class Config
    {
        private static IConfigurationRoot _configuration;
        private const string Endpoint = "ApiTesting:Endpoint";
        private const string Username = "Credential:username";
        private const string Password = "Credential:password";

        static Config()
        {
            var builder = new ConfigurationBuilder()
           .SetBasePath(AppContext.BaseDirectory)
           .AddJsonFile($"Credentials\\AppSettings.json", optional: false, reloadOnChange: true);

            _configuration = builder.Build() ?? throw new InvalidOperationException("Configuration could not be loaded. Please check the AppSettings.json file.");


        }


        public static string GetEndpoint()=>
            _configuration[Endpoint] ?? throw new InvalidOperationException("Endpoint not found in configuration.");
        

        public static string GetUsername() =>
            _configuration[Username] ?? throw new InvalidOperationException("Username not found in configuration.");


        public static string GetPassword() =>
            _configuration[Password] ?? throw new InvalidOperationException("Password not found in configuration.");







    }
}
