using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Group4DesktopApp.Model;

namespace Group4DesktopApp.DAL
{
    public class UserDAL
    {
        /// <summary>
        /// Gets the account identifier from the specified user credentials.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The account ID if found, null otherwise</returns>
        public static User? GetUserByID(int id)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "select Username from Users where UserId = @uId";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@uId", SqlDbType.Int);
            command.Parameters["@uId"].Value = id;

            using var reader = command.ExecuteReader();
            var usernameOrdinal = reader.GetOrdinal("Username");
            User? newUser = null;

            while (reader.Read())
            {
                var value = reader[usernameOrdinal].ToString();
                if (value != null)
                {
                    newUser = new User(id, value);
                }

            }

            return newUser;
        }
    }
}
