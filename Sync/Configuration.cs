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
                { "VirtuousApiKey", "REPLACE_WITH_API_KEY_PROVIDED" },
                { "ConnectionString", @"Data Source =(localdb)\MSSQLLocalDB; Initial Catalog =Virtuous; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False" }
            };
        }

        private Dictionary<string, string> Values { get; set; }

        public string GetValue(string key)
        {
            return Values[key];
        }
    }
}
