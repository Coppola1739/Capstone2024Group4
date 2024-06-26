﻿using Dapper;
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
using System.Windows.Controls.Primitives;

namespace Group4DesktopApp.DAL
{
    /// <summary>
    /// The Notes Data Access Layer
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class NotesDAL
    {
        /// <summary>
        /// Gets all notes under the specified source identifier.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <returns>All notes under the specified source ID</returns>
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
        /// Gets all notes by the specified user Id.
        /// </summary>
        /// <param name="userId">The user Id.</param>
        /// <returns>All the notes linked to the specified userId</returns>
        public static ObservableCollection<Notes> GetAllNotesByUserId(int userId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "SELECT DISTINCT N.NotesId, N.SourceId, N.Content " +
                "FROM Notes N JOIN Source S ON N.SourceId = S.SourceId JOIN Users U ON S.UserId = @uId";
            ObservableCollection<Notes> items =
                new(connection.Query<Notes>(query,
                 new { uId = userId }).ToList());
            return items;
        }

        /// <summary>
        /// Adds the note to the specified source Id.
        /// </summary>
        /// <param name="sourceId">The source identifier.</param>
        /// <param name="content">The content of the note.</param>
        /// <returns>True if note was successfully added to the database, false otherwise.</returns>
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
        /// <param name="noteId">The note identifier.</param>
        /// <param name="updatedContent">The content of the note.</param>
        /// <returns>True if note was successfully updated, false otherwise.</returns>
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
        /// <returns>True if note was successfully deleted, false otherwise.</returns>
        public static bool DeleteNoteById(int noteId)
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
