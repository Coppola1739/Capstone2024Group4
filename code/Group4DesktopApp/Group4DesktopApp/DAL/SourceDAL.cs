using Dapper;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
    }
}
