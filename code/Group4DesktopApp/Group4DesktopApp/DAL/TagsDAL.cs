using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.DAL
{
    /// <summary>
    /// The Tags Data Access Layer
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class TagsDAL
    {

        /// <summary>
        /// Adds a new tag to the database only if a tag with the specified tag name does not already exists.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>True if tag was successfully added, false otherwise</returns>
        public static bool AddNewTag(string tagName, [Optional] SqlConnection conn)
        {
            if(string.IsNullOrWhiteSpace(tagName) || isTagExisting(tagName,conn))
            {
                return false;
            }

            var con2 = Connection.SqlConnection(null);
            Connection.tryOpenConnection(ref con2);
            using var connection = con2;

            var goodQuery = "insert into Tags (TagName) values (@tName)";


            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@tName", SqlDbType.NVarChar);
            command.Parameters["@tName"].Value = tagName;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }
        /// <summary>
        /// Determines whether a tag with the specified tag name already exists in the database.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>
        ///   <c>true</c> if a tag exists with the specified tag name; otherwise, <c>false</c>.
        /// </returns>
        public static bool isTagExisting(string tagName, [Optional] SqlConnection conn)
        {

            var con2 = Connection.SqlConnection(conn);
            Connection.tryOpenConnection(ref con2);
            using var connection = con2;

            var goodQuery = "SELECT COUNT(*) FROM Tags where TagName = @tName";


            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@tName", SqlDbType.NVarChar);
            command.Parameters["@tName"].Value = tagName;

            var count = Convert.ToInt32(command.ExecuteScalar());

            return count >= 1;
        }
    }
}
