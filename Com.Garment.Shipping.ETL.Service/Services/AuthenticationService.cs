using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Globalization;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    [ExcludeFromCodeCoverage]
    public class AuthenticationService
    {
        static string AUTH_URL = Environment.GetEnvironmentVariable("AUTH_URL", EnvironmentVariableTarget.Process);
        static string RESOURCE_URL = Environment.GetEnvironmentVariable("RESOURCE_URL", EnvironmentVariableTarget.Process);
        static string TENANT_ID = Environment.GetEnvironmentVariable("TENANT_ID", EnvironmentVariableTarget.Process);
        static string APPLICATION_ID = Environment.GetEnvironmentVariable("APPLICATION_ID", EnvironmentVariableTarget.Process);
        static string CLIENT_SECRET = Environment.GetEnvironmentVariable("CLIENT_SECRET", EnvironmentVariableTarget.Process);
        static string GRANT_TYPE = "client_credentials";
        ILogger log;

        public AuthenticationService(ILogger logger) {
            log = logger;
        }

        public async Task<string> getAccessToken()
        {
            HttpClient clientAuth = new HttpClient();
            clientAuth.BaseAddress = new Uri(AUTH_URL);
            
            var request = new HttpRequestMessage(HttpMethod.Post, TENANT_ID + "/oauth2/token");

            var body = new List<KeyValuePair<string, string>>();
            body.Add(new KeyValuePair<string, string>("client_id", APPLICATION_ID));
            body.Add(new KeyValuePair<string, string>("grant_type", GRANT_TYPE));
            body.Add(new KeyValuePair<string, string>("client_secret", CLIENT_SECRET));
            body.Add(new KeyValuePair<string, string>("resource", RESOURCE_URL));
            
            request.Content = new FormUrlEncodedContent(body);
            HttpResponseMessage resAuth = await clientAuth.SendAsync(request);
            if(resAuth.IsSuccessStatusCode) {
                var contentStream = await resAuth.Content.ReadAsStreamAsync();
                try
                {
                    var responseBody = await JsonSerializer.DeserializeAsync<AuthResponse>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true });
                    return responseBody.access_token;
                }
                catch (JsonException) // Invalid JSON
                {
                    return "FAILED";
                }    
            }
            return "FAILED";
        }
    }

    public class AuthResponse {
        public string token_type {get;set;}
        public string expires_in {get;set;}
        public string ext_expires_in {get;set;}
        public string expires_on {get;set;}
        public string not_before {get;set;}
        public string resource {get;set;}
        public string access_token {get;set;}
    }
}