using System;

namespace Recommend.SDK.Model
{
    public class ApiKeyRequest
    {
        public string code { get; set; }
        public string apiToken { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }
}