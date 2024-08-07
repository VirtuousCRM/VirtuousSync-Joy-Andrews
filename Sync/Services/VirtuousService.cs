using RestSharp;
using System.Threading.Tasks;
using System.Collections.Generic;
using Sync.Models;

namespace Sync.Services
{
    /// <summary>
    /// API Docs found at https://docs.virtuoussoftware.com/
    /// </summary>
    internal class VirtuousService
    {
        private readonly RestClient _restClient;

        /// <summary>
        /// This construtor method initializes rest client for API call
        /// </summary>
        public VirtuousService() 
        {
            var configuration = new Configuration();

            var apiBaseUrl = configuration.GetValue("VirtuousApiBaseUrl");
            var apiKey = configuration.GetValue("VirtuousApiKey");

            var options = new RestClientOptions(apiBaseUrl)
            {
                Authenticator = new RestSharp.Authenticators.OAuth2.OAuth2AuthorizationRequestHeaderAuthenticator(apiKey)
            };

            _restClient = new RestClient(options);
        }

        /// <summary>
        /// This method retrieves contacts from the state of AZ
        /// by calling Virtuous API
        /// </summary>
        /// <param name="skip">skip query parameter for Virtuous API</param>
        /// <param name="take">take query parameter for Virtuous API</param>
        /// <returns>
        /// List of abbreviated contacts
        /// </returns>
        public async Task<PagedResult<AbbreviatedContact>> GetContactsAsync(int skip, int take)
        {
            var request = new RestRequest("/api/Contact/Query", Method.Post);
            request.AddQueryParameter("Skip", skip);
            request.AddQueryParameter("Take", take);

            var body = new ContactQueryRequest()
            {
                Groups = new List<Group>() 
                {
                    new Group()
                    {
                        Conditions = new List<Condition>()
                        {
                            new Condition()
                            {
                                Parameter = "state",
                                Operator = "in",
                                Values = new List<string>() { "AZ" }
                            }
                        }
                    }
                }
            };

            request.AddJsonBody(body);

            var response = await _restClient.PostAsync<PagedResult<AbbreviatedContact>>(request);
            return response;
        }
    }
}
