using CsvHelper;
using System;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Sync().GetAwaiter().GetResult();
        }

        private static async Task Sync()
        {
            var apiKey = "REPLACE_WITH_API_KEY_PROVIDED";
            var configuration = new Configuration(apiKey);
            var virtuousService = new VirtuousService(configuration);

            var skip = 0;
            var take = 100;
            var maxContacts = 999;
            var hasMore = true;

            var datasource = @"(localdb)\MSSQLLocalDB";
            var database = "Virtuous";

            string connString = @"Data Source =" + datasource + "; Initial Catalog ="
                + database + "; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = False; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";

            try
            {
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

                string sqlQuery = strBuilder.ToString();
                sqlQuery = sqlQuery.Remove(sqlQuery.Length - 2);
                Console.WriteLine(sqlQuery);

                using (var conn = new SqlConnection(connString))
                using (SqlCommand command = new SqlCommand(sqlQuery, conn))
                {
                    Console.WriteLine("Openning Connection ...");

                    conn.Open();

                    Console.WriteLine("Connection successful!");

                    command.ExecuteNonQuery();

                    Console.WriteLine("Query Executed.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Environment.Exit(0);
        }
    }
}
