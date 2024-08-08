﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Services
{
    internal class DatabaseService
    {
        private readonly IConfiguration _config;

        public DatabaseService(IConfiguration configuration)
        {
            _config = configuration;
        }
        /// <summary>
        /// This method connects to the database and runs
        /// the sql query string provided
        /// </summary>
        /// <param name="sqlQueryString">SQL query string to be executed</param>
        public void RunQuery(string sqlQueryString)
        {
            using (var conn = new SqlConnection(_config.GetValue("ConnectionString")))
            using (SqlCommand command = new SqlCommand(sqlQueryString, conn))
            {
                Console.WriteLine("Openning Connection ...");

                conn.Open();

                Console.WriteLine("Connection successful!");

                command.ExecuteNonQuery();

                Console.WriteLine("Query Executed.");
            }
        }
    }
}
