using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.DAL
{
    public class NotesDAL
    {
        /// <summary>
        /// Gets all notes by source identifier.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns></returns>
        public static ObservableCollection<Notes> GetAllNotesBySourceId(int sourceId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "Select * FROM Notes WHERE SourceId = @srcId";
            ObservableCollection<Notes> items =
                new(connection.Query<Notes>(query,
                 new { srcId = sourceId }).ToList());
            return items;
        }

        /// <summary>
        /// Adds the note to source.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="content">The content of the note.</param>
        /// <returns></returns>
        public static bool AddNoteToSource(int sourceId, string content)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "insert into Notes (SourceId,Content) values (@srcId, @cont)";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@srcId", SqlDbType.Int);
            command.Parameters["@srcId"].Value = sourceId;
            command.Parameters.Add("@cont", SqlDbType.NVarChar);
            command.Parameters["@cont"].Value = content;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }

        /// <summary>
        /// Updates the content of the note to with the specified new content
        /// </summary>
        /// <param name="noteId">The source identifier.</param>
        /// <param name="updatedContent">The content of the note.</param>
        /// <returns></returns>
        public static bool UpdateNoteContent(int noteId, string updatedContent)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "update Notes set Content = @updCont where NotesId = @nId";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@nId", SqlDbType.Int);
            command.Parameters["@nId"].Value = noteId;
            command.Parameters.Add("@updCont", SqlDbType.NVarChar);
            command.Parameters["@updCont"].Value = updatedContent;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }

        /// <summary>
        /// Deletes the note of the specified note ID.
        /// </summary>
        /// <param name="noteId">The note identifier.</param>
        /// <returns></returns>
        public static bool DeleteNote(int noteId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

            var goodQuery = "delete from Notes where NotesId = @nId";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@nId", SqlDbType.Int);
            command.Parameters["@nId"].Value = noteId;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }
    }
}
