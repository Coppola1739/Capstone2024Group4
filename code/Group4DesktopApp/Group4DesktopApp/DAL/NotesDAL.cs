using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    }
}
