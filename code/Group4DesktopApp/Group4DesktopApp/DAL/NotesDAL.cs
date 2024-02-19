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
        public static ObservableCollection<Notes> GetAllNotesBySourceId(int sourceId)
        {
            using var connection = new SqlConnection(Connection.ConnectionString);
            var query = "Select * FROM Notes WHERE SourceId = @srcId";
            ObservableCollection<Notes> items =
                new(connection.Query<Notes>(query,
                 new { srcId = sourceId }).ToList());
            return items;
        }
        [ExcludeFromCodeCoverage]
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

            if (result < 0)
            {
                return false;
            }
            return true;
        }
    }
}
