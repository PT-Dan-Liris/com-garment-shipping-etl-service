using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Com.Garment.Shipping.ETL.Service.ViewModels;
using System.Diagnostics.CodeAnalysis;

namespace Com.Garment.Shipping.ETL.Service.Services
{
    [ExcludeFromCodeCoverage]
    public class DWHService
    {
        static string RESOURCE_URL = Environment.GetEnvironmentVariable("RESOURCE_URL", EnvironmentVariableTarget.Process);
        static string SUBSCRIPTION_ID = Environment.GetEnvironmentVariable("SUBSCRIPTION_ID", EnvironmentVariableTarget.Process);
        static string RESOURCES_GROUPS = Environment.GetEnvironmentVariable("RESOURCES_GROUP", EnvironmentVariableTarget.Process);
        static string DWH_NAME = Environment.GetEnvironmentVariable("DWH_NAME", EnvironmentVariableTarget.Process);
        string token;
        ILogger log;
        public DWHService(ILogger logger, string tokens) {
            log = logger;
            token = tokens;
        }
        public bool isOnline()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RESOURCE_URL);

            var request = new HttpRequestMessage(
                HttpMethod.Get, 
                "subscriptions/" + 
                SUBSCRIPTION_ID + 
                "/resourceGroups/" +
                RESOURCES_GROUPS +
                "/providers/Microsoft.Sql/servers/efrata/databases/" +
                DWH_NAME + 
                "?api-version=2020-08-01-preview"
            );

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage resDWH = client.SendAsync(request).Result;
            
            if(resDWH.IsSuccessStatusCode) {
                var contentStream = resDWH.Content.ReadAsStreamAsync().Result;
                var responseBody = JsonSerializer
                    .DeserializeAsync<DWHViewModel>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true })
                    .Result;
                if (responseBody.Properties.Status == "Online") {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool isPaused()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(RESOURCE_URL);

            var request = new HttpRequestMessage(
                HttpMethod.Get, 
                "subscriptions/" + 
                SUBSCRIPTION_ID + 
                "/resourceGroups/" +
                RESOURCES_GROUPS +
                "/providers/Microsoft.Sql/servers/efrata/databases/" +
                DWH_NAME + 
                "?api-version=2020-08-01-preview"
            );

            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage resDWH = client.SendAsync(request).Result;
            
            if(resDWH.IsSuccessStatusCode) {
                var contentStream = resDWH.Content.ReadAsStreamAsync().Result;
                var responseBody = JsonSerializer
                    .DeserializeAsync<DWHViewModel>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true })
                    .Result;
                if (responseBody.Properties.Status == "Paused") {
                    return true;
                }
                return false;
            }
            return false;
        }
        public bool resume() {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RESOURCE_URL);
                
                var request = new HttpRequestMessage(
                    HttpMethod.Post, 
                    "subscriptions/" + 
                    SUBSCRIPTION_ID + 
                    "/resourceGroups/" +
                    RESOURCES_GROUPS +
                    "/providers/Microsoft.Sql/servers/efrata/databases/" +
                    DWH_NAME + 
                    "/resume?api-version=2020-08-01-preview"
                );

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resDWH = client.SendAsync(request).Result;

                if(resDWH.IsSuccessStatusCode) {
                    var contentStream = resDWH.Content.ReadAsStreamAsync().Result;
                    var responseBody = JsonSerializer
                        .DeserializeAsync<DWHViewModel>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true })
                        .Result;
                    if (responseBody.Properties.Status == "Online") {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        public bool pause() {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(RESOURCE_URL);
                
                var request = new HttpRequestMessage(
                    HttpMethod.Post, 
                    "subscriptions/" + 
                    SUBSCRIPTION_ID + 
                    "/resourceGroups/" +
                    RESOURCES_GROUPS +
                    "/providers/Microsoft.Sql/servers/efrata/databases/" +
                    DWH_NAME + 
                    "/pause?api-version=2020-08-01-preview"
                );

                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage resDWH = client.SendAsync(request).Result;

                if(resDWH.IsSuccessStatusCode) {
                    var contentStream = resDWH.Content.ReadAsStreamAsync().Result;
                    var responseBody = JsonSerializer
                        .DeserializeAsync<DWHViewModel>(contentStream, new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true })
                        .Result;
                    if (responseBody.Properties.Status == "Online") {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool waiterChecker(string expectToBe) {
            switch (expectToBe) {
                case "PAUSE":
                    System.Threading.Thread.Sleep(30 * 1000);
                    if (isPaused()){
                        return true;
                    }
                    waiterChecker(expectToBe);
                    return false;
                case "RESUME":
                    System.Threading.Thread.Sleep(30 * 1000);
                    if (isOnline()){
                        return true;
                    }
                    waiterChecker(expectToBe);
                    return false;
            }
            return true;
        }
    }
}