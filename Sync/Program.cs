using Sync.BusinessLogic;
using Sync.Services;
using System;
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
            try
            {
                var sqlBuilder = new SQLBuilder();
                string sqlQueryString = await sqlBuilder.CreateSQLQueryString();
                Console.WriteLine(sqlQueryString);
                var databaseService = new DatabaseService();
                databaseService.RunQuery(sqlQueryString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            Environment.Exit(0);
        }
    }
}
