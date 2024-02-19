﻿using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Group4DesktopApp.DAL
{
    public class AccountDAL
    {
        /// <summary>
        /// Gets the account identifier from the specified user credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The account ID if found, null otherwise</returns>
        public static int? GetAccountID(string username, string password)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "select UserId from Users where Username = @uname AND Password = @pword";

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
                    int result;
                    bool success = int.TryParse(value, out result);
                    if (success)
                    {
                        accountID = result;
                    }
                    else
                    {
                        accountID = null;
                    }
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
        /// <returns>The account ID if found, null otherwise</returns>
        public static bool CreateAccount(string username, string password)
        {
            if(isAccountExisting(username))
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

            if (result < 0)
            {
                return false;
            }
            return true;

        }

        private static bool isAccountExisting(string username)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "SELECT COUNT(*) FROM Users where username = @uname";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@uname", SqlDbType.NVarChar);
            command.Parameters["@uname"].Value = username;

            var count = Convert.ToInt32(command.ExecuteScalar());

            return count >= 1;
        }
    }
}