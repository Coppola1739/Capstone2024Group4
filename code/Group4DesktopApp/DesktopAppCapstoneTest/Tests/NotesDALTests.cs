using Group4DesktopApp.DAL;
using Group4DesktopApp.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DesktopAppCapstoneTest.Tests
{
    public class NotesDALTests
    {
        [Test]
        public void TestGetAllNotesFromUserSourceId()
        {
            using (TransactionScope scop = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                using var connection = new SqlConnection(Connection.ConnectionString);
                connection.Open();
                using SqlTransaction myTrans = connection.BeginTransaction();
                using var myCommand = connection.CreateCommand();
                myCommand.Transaction = myTrans;
                try
                {
                    //Ensures there is at least one user in the database
                    bool value = AccountDAL.CreateAccount("Test", "ok", connection);
                    int? id = AccountDAL.GetAccountID("Test", "ok");
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    newSource.UserId = id.Value;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);
                    
                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(id.Value);
                    int sourceId = sources[0].SourceId;

                    Notes newNote = new Notes();
                    newNote.SourceId = sourceId;
                    NotesDAL.AddNoteToSource(sourceId, newNote.Content);
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(notes.Count, Is.EqualTo(1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    myTrans.Rollback();
                    connection.Close();
                    Assert.Fail(ex.Message);
                }
            }
        }
    }
}
