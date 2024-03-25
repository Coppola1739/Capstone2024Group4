using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.DAL
{
    public class NoteTagsDAL
    {
        /// <summary>
        /// Gets a collection of all Tags under a note by the specified noteId
        /// </summary>
        /// <param name="noteId"></param>
        /// <returns>a collection of all Tags under a note by the specified noteId</returns>
        public static ObservableCollection<NoteTags>GetAllTagsByNoteId(int noteId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "Select * FROM NoteTags WHERE NotesId = @ntId";
            ObservableCollection<NoteTags> items =
                new(connection.Query<NoteTags>(query,
                 new { ntId = noteId }).ToList());
            return items;
        }

        /// <summary>
        /// Adds the specified tag name to the specified noteId
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="noteId"></param>
        /// <returns>True, if added successfully, false otherwise</returns>
        public static bool AddTagToNote(string tagName, int noteId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            //Only creates a new tag ONLY if tag name is not existing
            TagsDAL.AddNewTag(tagName);

            var goodQuery = "insert into NoteTags (TagName,NotesId) values (@tName, @nId)";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@tName", SqlDbType.NVarChar);
            command.Parameters["@tName"].Value = tagName;
            command.Parameters.Add("@nId", SqlDbType.Int);
            command.Parameters["@nId"].Value = noteId;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }

        public static bool DeleteTagFromNote(NoteTags tag)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "delete from NoteTags where TagName = @tName AND NotesId = @nId";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@tName", SqlDbType.NVarChar);
            command.Parameters["@tName"].Value = tag.TagName;
            command.Parameters.Add("@nId", SqlDbType.Int);
            command.Parameters["@nId"].Value = tag.NotesId;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }

        public static bool isTagExistingUnderNote(string tagName, int noteId)
        {

            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "SELECT COUNT(*) FROM NoteTags where TagName = @tName AND NotesId = @nId";


            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@tName", SqlDbType.NVarChar);
            command.Parameters["@tName"].Value = tagName;
            command.Parameters.Add("@nId", SqlDbType.Int);
            command.Parameters["@nId"].Value = noteId;

            var count = Convert.ToInt32(command.ExecuteScalar());

            return count >= 1;
        }
    }
}
