using System;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Group4DesktopApp.DAL
{
    /// <summary>
    /// The Connection class
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Connection
    {

        /// <summary>
        /// The server name
        /// </summary>
        public static readonly string ServerName = "(localdb)\\MSSQLLocalDB";

        /// <summary>
        /// The database name
        /// </summary>
        public static readonly string DBName = "capstone2024";

        /// <summary>
        /// The connection string
        /// </summary>
        public static readonly string ConnectionString = "Server=" + ServerName + "; Database=" + DBName;

        /// <summary>
        /// Returns a new SQLConnection Object if specified one is null or undefined. Otherwise, returns the same one if valid
        /// </summary>
        /// <param name="sqlConnection">The SQL connection.</param>
        /// <returns></returns>
        public static SqlConnection SqlConnection(SqlConnection? sqlConnection)
        {
            if (sqlConnection == null)
            {
                return new SqlConnection(ConnectionString);
            }
            return sqlConnection;
        }

        /// <summary>
        /// Tries to open the specified connection and returns true or false if the connection was opened.
        /// </summary>
        /// <postcondition>specified connection is open depending on bool return</postcondition>
        /// <param name="sqlConnection">The SQL connection.</param>
        /// <returns>true if connection can and is opened, false otherwise</returns>
        public static bool tryOpenConnection(ref SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
