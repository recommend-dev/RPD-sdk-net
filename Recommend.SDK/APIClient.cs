using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Recommend.SDK.Model;

namespace Recommend.SDK
{
    /// <summary>
    /// Recommend API client
    /// </summary>
    public class APIClient
    {
        public readonly HttpClient HttpClient = new HttpClient();
        private string ApiKey { get; set; }
        private bool ThrowExceptions { get; set; }

        /// <summary>
        /// Creates Recommend API client
        /// <param name="apiKey">Your recommend.co API key</param>
        /// <param name="baseUrl">Recommend API URL, uses default production URL</param>
        /// <param name="throwExceptions">Throw exception on error, off by default</param>
        /// </summary>
        public APIClient(string apiKey, string baseUrl = "https://api.recommend.co/apikeys", bool throwExceptions = false)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new ArgumentException("ApiKey cannot be empty");
            }

            this.ApiKey = apiKey;
            this.ThrowExceptions = throwExceptions;
            HttpClient.BaseAddress = new Uri(baseUrl);
        }

        /// <summary>
        /// Test connection and API key validity
        /// </summary>
        /// <returns>True if connection and API key are ok, false if wrong API key is used or API endpoint cannot be reached</returns>
        public async Task<bool> TestConnection()
        {
            try
            {
                var test = new ApiKeyRequest()
                {
                    apiToken = this.ApiKey,
                    code = "test-connection"
                };

                var result = await HttpClient.PostAsJsonAsync("", test);
                if (result != null && result.IsSuccessStatusCode)
                {
                    var responseData = await result.Content.ReadAsStringAsync();
                    var response = System.Text.Json.JsonSerializer.Deserialize<ApiKeyResponse>(responseData);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                if (ThrowExceptions) throw ex;
                return false;
            }
        }


        /// <summary>
        /// Send referral information to API
        /// </summary>
        /// <param name="code">Referral code (rcmndref)</param>
        /// <param name="email">Customer email (optional)</param>
        /// <param name="phone">Customer phone (optional)</param>
        /// <returns>ApiKeyResponse with status code</returns>
        public async Task<ApiKeyResponse> ReferralCheck(string code, string email = "", string phone = "")
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentException("Code cannot be empty");

            try
            {
                var request = new ApiKeyRequest()
                {
                    apiToken = this.ApiKey,
                    code = code,
                    email = email,
                    phone = phone
                };

                var result = await HttpClient.PostAsJsonAsync("", request);
                if (result != null && result.IsSuccessStatusCode)
                {
                    var responseData = await result.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<ApiKeyResponse>(responseData);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                if (ThrowExceptions) throw ex;
                return null;
            }
        }
    }
}
