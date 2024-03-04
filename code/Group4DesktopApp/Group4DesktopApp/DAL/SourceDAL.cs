using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;

namespace Group4DesktopApp.DAL
{
    public class SourceDAL
    {
        /// <summary>
        /// Gets all sources by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public static ObservableCollection<Source> GetAllSourcesByUserId(int userId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "Select * FROM Source WHERE UserId = @uId";
            ObservableCollection<Source> items =
                new(connection.Query<Source>(query,
                 new { uId = userId }).ToList());
            return items;
        }
        /// <summary>
        /// Adds the new source.
        /// </summary>
        /// <param name="userId">The user identifier connected to the source</param>
        /// <param name="source">The source data</param>
        /// <param name="conn">The optional sqlConnection to use</param>
        /// <returns></returns>
        public static bool AddNewSource(int userId, Source source, [Optional] SqlConnection conn)
        {
            var con2 = Connection.SqlConnection(conn);
            Connection.tryOpenConnection(ref con2);
            using var connection = con2;

            var goodQuery = "insert into Source " +
                "(UserId,SourceName,UploadDate,Content,SourceType,AuthorFirstName,AuthorLastName,Title) " +
                "values (@uId, @srcName, @uDate, @cont, @srcType, @authFName, @authLName, @title)";

            using var command = new SqlCommand(goodQuery, connection);


            command.Parameters.Add("@uId", SqlDbType.Int);
            command.Parameters["@uId"].Value = userId;

            command.Parameters.Add("@srcName", SqlDbType.NVarChar);
            command.Parameters["@srcName"].Value = source.SourceName;

            command.Parameters.Add("@uDate", SqlDbType.DateTime);
            command.Parameters["@uDate"].Value = DateTime.Now;

            command.Parameters.Add("@cont", SqlDbType.VarBinary);
            command.Parameters["@cont"].Value = source.Content;

            command.Parameters.Add("@srcType", SqlDbType.NVarChar);
            command.Parameters["@srcType"].Value = source.SourceType;

            command.Parameters.Add("@authFName", SqlDbType.NVarChar);
            command.Parameters["@authFName"].Value = source.AuthorFirstName;

            command.Parameters.Add("@authLName", SqlDbType.NVarChar);
            command.Parameters["@authLName"].Value = source.AuthorLastName;

            command.Parameters.Add("@title", SqlDbType.NVarChar);
            command.Parameters["@title"].Value = source.Title;

            int result = command.ExecuteNonQuery();

            return result >= 0;
        }

        public static bool DeleteSource(Source source)
        {
            int sourceId = source.SourceId;
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();
            using SqlTransaction myTrans = connection.BeginTransaction();
            using var myCommand = connection.CreateCommand();
            myCommand.Transaction = myTrans;
            try
            {
                myCommand.CommandText = "delete from Notes where SourceId = @srcId";
                myCommand.Parameters.Add("@srcId", SqlDbType.Int);
                myCommand.Parameters["@srcId"].Value = sourceId;
                myCommand.ExecuteNonQuery();

                myCommand.CommandText = "delete from Source where SourceId = @srcId";
                myCommand.ExecuteNonQuery();

                myTrans.Commit();
                return true;

            } catch(SqlException ex)
            {
                Debug.WriteLine(ex.ToString());
                myTrans.Rollback();
                return false;
            }
        }
    }
}
