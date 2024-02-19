using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Group4DesktopApp.DAL
{
    public class SourceDAL
    {
        public static ObservableCollection<Source> GetAllSourcesByUserId(int userId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "Select * FROM Source WHERE UserId = @uId";
            ObservableCollection<Source> items =
                new(connection.Query<Source>(query,
                 new { uId = userId }).ToList());
            return items;
        }

        public static bool AddNewSource(int userId, Source source)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            connection.Open();

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

            if (result < 0)
            {
                return false;
            }
            return true;
        }
    }
}
