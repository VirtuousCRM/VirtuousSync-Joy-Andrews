using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync
{
    public interface IConfiguration
    {
        string GetValue(string key);
    }

    internal class Configuration : IConfiguration
    {
        public Configuration() 
        {
            Values = new Dictionary<string, string>()
            {
                { "VirtuousApiBaseUrl", "https://api.virtuoussoftware.com" },
                { "VirtuousApiKey", "v_eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiN2VhYTBhNTQtYTBiZC00OTNlLWFjNDMtZjNjZGEwZmVlNWQ5IiwiZXhwIjoyMTQ3NDgzNjQ3LCJpc3MiOiJodHRwczovL2FwcC52aXJ0dW91c3NvZnR3YXJlLmNvbSIsImF1ZCI6Imh0dHBzOi8vYXBpLnZpcnR1b3Vzc29mdHdhcmUuY29tIn0.oN0bfmYMS7lPxGtVH3ouEVhD0Kuzoqa2nAnuvPTyPpk" },
                { "ConnectionString", "REPLACE_WITH_API_KEY_PROVIDED" }
            };
        }

        private Dictionary<string, string> Values { get; set; }

        public string GetValue(string key)
        {
            return Values[key];
        }
    }
}
