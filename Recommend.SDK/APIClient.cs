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
        public APIClient(string apiKey, string baseUrl = "https://api.recommend.co/apikeys/", bool throwExceptions = false)
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
        /// <param name="orderNumber">Order number (optional)</param>
        /// <param name="cartTotal">Cart total amount (optional)</param>
        /// <param name="ssnid">Session ID used for bounce rate tracking (optional)</param>
        /// <returns>ApiKeyResponse with status code</returns>
        public async Task<ApiKeyResponse> ReferralCheck(string code, string email = "", string phone = "", string orderNumber = "", string cartTotal = "", string ssnid = "")
        {
            if (string.IsNullOrEmpty(code)) throw new ArgumentException("Code cannot be empty");

            try
            {
                var request = new ApiKeyRequest()
                {
                    apiToken = this.ApiKey,
                    code = code,
                    email = email,
                    phone = phone,
                    orderNumber = orderNumber,
                    cartTotal = cartTotal,
                    ssnid = ssnid
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

        /// <summary>
        /// Approve single conversion
        /// </summary>
        /// <param name="conversionId">Conversion ID received from ReferralCheck</param>
        public async Task<ApiKeyResponse> ApproveConversion(int conversionId)
        {
            try
            {
                var request = new ConversionDTO()
                {
                    ApiKey = this.ApiKey,
                    ConversionId = conversionId
                };

                var result = await HttpClient.PostAsJsonAsync("approve", request);
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

        /// <summary>
        /// Reject single conversion
        /// </summary>
        /// <param name="conversionId">Conversion ID received from ReferralCheck</param>
        public async Task<ApiKeyResponse> RejectConversion(int conversionId)
        {
            try
            {
                var request = new ConversionDTO()
                {
                    ApiKey = this.ApiKey,
                    ConversionId = conversionId
                };

                var result = await HttpClient.PostAsJsonAsync("reject", request);
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

        /// <summary>
        /// Gets conversion status
        /// </summary>
        /// <param name="conversionId">Conversion ID received from ReferralCheck</param>
        public async Task<ApiKeyResponse> GetConversionStatus(int conversionId)
        {
            try
            {
                var request = new ConversionDTO()
                {
                    ApiKey = this.ApiKey,
                    ConversionId = conversionId
                };

                var result = await HttpClient.PostAsJsonAsync("GetConversionStatus", request);
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
