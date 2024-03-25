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
    public class TagsDAL
    {
        /// <summary>
        /// Returns a collection of every tag existing in the database.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Tags> GetAllTags()
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "Select * FROM Tags";
            ObservableCollection<Tags> items =
                new(connection.Query<Tags>(query).ToList());
            return items;
        }

        /// <summary>
        /// Adds a new tag to the database only if a tag with the specified tag name does not already exists.
        /// </summary>
        /// <param name="tagName">Name of the tag.</param>
        /// <returns>True if tag was successfully added, false otherwise</returns>
        public static bool AddNewTag(string tagName)
        {
            if(string.IsNullOrWhiteSpace(tagName) || isTagExisting(tagName))
            {
                return false;
            }

            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

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
        public static bool isTagExisting(string tagName)
        {

            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "SELECT COUNT(*) FROM Tags where TagName = @tName";


            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@tName", SqlDbType.NVarChar);
            command.Parameters["@tName"].Value = tagName;

            var count = Convert.ToInt32(command.ExecuteScalar());

            return count >= 1;
        }
    }
}
