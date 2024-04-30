using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Group4DesktopApp.DAL
{
    /// <summary>
    /// The Account Data Access Layer
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class AccountDAL
    {
        /// <summary>
        /// Gets the account identifier from the specified user credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="conn">The optional sqlConnection that can be used</param>
        /// <returns>The account ID if found, null otherwise</returns>
        public static int? GetAccountID(string username, string password, [Optional] SqlConnection conn)
        {

            var con2 = Connection.SqlConnection(conn);
            Connection.tryOpenConnection(ref con2);
            using var connection = con2;


            var goodQuery = "select UserId from Users where Username = @uname AND Password COLLATE Latin1_General_CS_AS = @pword";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@uname", SqlDbType.NVarChar);
            command.Parameters["@uname"].Value = username;
            command.Parameters.Add("@pword", SqlDbType.NVarChar);
            command.Parameters["@pword"].Value = password;

            using var reader = command.ExecuteReader();
            var userIdOrdinal = reader.GetOrdinal("UserId");
            int? accountID = null;

            while (reader.Read())
            {
                var value = reader[userIdOrdinal].ToString();
                if (value != null)
                {
                    accountID = int.Parse(value);

                }
            }

            if (accountID == null)
            {
                return null;
            }

            return accountID;
        }

        /// <summary>
        /// Gets the account identifier from the specified user credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="conn">The optional sqlConnection that can be used</param>
        /// <returns>The account ID if found, null otherwise</returns>
        public static bool CreateAccount(string username, string password, [Optional] SqlConnection conn)
        {

            if (isAccountExisting(username, conn))
            {
                return false;
            }

            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "insert into Users (Username,Password) values (@uname, @pword)";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@uname", SqlDbType.NVarChar);
            command.Parameters["@uname"].Value = username;
            command.Parameters.Add("@pword", SqlDbType.NVarChar);
            command.Parameters["@pword"].Value = password;

            int result = command.ExecuteNonQuery();

            return result >= 0;

        }

        private static bool isAccountExisting(string username, [Optional] SqlConnection conn)
        {

            var con2 = Connection.SqlConnection(conn);
            Connection.tryOpenConnection(ref con2);
            using var connection = con2;

            var goodQuery = "SELECT COUNT(*) FROM Users where username = @uname";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@uname", SqlDbType.NVarChar);
            command.Parameters["@uname"].Value = username;

            var count = Convert.ToInt32(command.ExecuteScalar());

            return count >= 1;
        }
    }
}
