using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DotNetEnv;

namespace Solutions2ShareTickets
{
    public class ZohoAuthenticationHelper
    {
        public const string TokenEndpoint = "https://accounts.zoho.com/oauth/v2/token";
        public readonly string clientId = Environment.GetEnvironmentVariable("ZOHO_CLIENT_ID")!;
        public readonly string clientSecret = Environment.GetEnvironmentVariable("ZOHO_CLIENT_SECRET")!;
        public readonly string scope = Environment.GetEnvironmentVariable("ZOHO_DESK_SCOPE")!;

        public ZohoAuthenticationHelper(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;  
        }
        
        public async Task<string> GetAccessToken(string scope)
        {
            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[] {
                    new KeyValuePair<string, string>("grant_type", "client_credentials"),
                    new KeyValuePair<string, string>("client_id", clientId),
                    new KeyValuePair<string, string>("client_secret", clientSecret),
                    new KeyValuePair<string, string>("scope", scope)
                });
                var response = await client.PostAsync(TokenEndpoint, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var tokenResponse = JsonConvert.DeserializeObject<ZohoTokenResponse>(responseContent);
                    return tokenResponse!.AccessToken;
                }
                throw new Exception("Failed to get access token");
            }        
        }

        internal Task GetAccessToken(object scope)
        {
            throw new NotImplementedException();
        }
    }
}

public class ZohoTokenResponse
{
    public required string AccessToken { get; set; }
}


