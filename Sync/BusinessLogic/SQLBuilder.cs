using Sync.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.BusinessLogic
{
    internal class SQLBuilder
    {
        private readonly IConfiguration _config;

        public SQLBuilder(IConfiguration configuration)
        {
            _config = configuration;
        }
        /// <summary>
        /// This method creates an insert sql query string from contacts 
        /// retrieved from Virtuous API
        /// </summary>
        /// <returns>
        /// sql query string
        /// </returns>
        public async Task<string> CreateSQLQueryString()
        {
            var virtuousService = new VirtuousService(_config);

            var skip = 0;
            var take = 100;
            var maxContacts = 999;
            var hasMore = true;

            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("INSERT INTO AbbreviatedContact (Id, Name, ContactType, ContactName, Address, Email, Phone) VALUES ");

            do
            {
                var contacts = await virtuousService.GetContactsAsync(skip, take);
                skip += take;

                foreach (var contact in contacts.List)
                {
                    strBuilder.Append("('" + contact.Id + "', '" + contact.Name.Replace("'", "''")
                        + "', '" + contact.ContactType + "', '" + contact.ContactName.Replace("'", "''")
                        + "', '" + contact.Address + "', '" + contact.Email
                        + "', '" + contact.Phone + "'), ");
                }
                hasMore = skip > maxContacts;
            }
            while (!hasMore);

            string sqlQueryString = strBuilder.ToString();
            sqlQueryString = sqlQueryString.Remove(sqlQueryString.Length - 2);
            return sqlQueryString;
         }
    }
}
