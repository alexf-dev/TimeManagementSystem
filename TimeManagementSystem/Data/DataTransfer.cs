using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeManagementSystem.Data
{
    /// <summary>
    /// Data class
    /// </summary>
    public static partial class DataTransfer
    {
        /// <summary>
        /// DB connection string
        /// </summary>
        public static string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DBStringConnection"].ConnectionString;

        /// <summary>  
        /// Get the db connection  
        /// </summary>  
        /// <param name="connStr"></param>  
        /// <returns></returns>  
        public static IDbConnection OpenConnection(string connStr)
        {
            var conn = new SQLiteConnection(connStr);
            conn.Open();
            return conn;
        }
    }
}
