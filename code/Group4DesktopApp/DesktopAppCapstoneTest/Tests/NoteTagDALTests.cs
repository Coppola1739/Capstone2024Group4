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
    /// <summary>
    /// Test Class for all the NoteTag DAL Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class NoteTagDALTests
    {
        /// <summary>
        /// Test if the NoteTag DAL method properly handles adding a tag to a note.
        /// </summary>
        [Test]
        public void TestAddTagToNote()
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
                    bool value = AccountDAL.CreateAccount("TestAccount", "TestPassword@", connection);
                    int? id = AccountDAL.GetAccountID("TestAccount", "TestPassword@");
                    int accountId = id ?? -1;
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);

                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    int sourceId = sources[0].SourceId;

                    NotesDAL.AddNoteToSource(sourceId, "Original Content");
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    string tagName = "CompSci";
                    NoteTagsDAL.AddTagToNote(tagName, notes[0].NotesId);

                    IList<NoteTags> noteTag = NoteTagsDAL.GetAllTagsByNoteId(notes[0].NotesId);

                    bool isTagLinkedToNote = noteTag.Any(tag => tag.TagName == tagName);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(isTagLinkedToNote);
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
        /// <summary>
        /// Test if the NoteTag DAL method properly handles deleting a tag from a note
        /// </summary>
        [Test]
        public void TestDeleteTagFromNote()
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
                    bool value = AccountDAL.CreateAccount("TestAccount", "TestPassword@", connection);
                    int? id = AccountDAL.GetAccountID("TestAccount", "TestPassword@");
                    int accountId = id ?? -1;
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);

                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    int sourceId = sources[0].SourceId;

                    NotesDAL.AddNoteToSource(sourceId, "Original Content");
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    string tagName = "CompSci";
                    TagsDAL.AddNewTag(tagName);
                    NoteTagsDAL.AddTagToNote(tagName, notes[0].NotesId);

                    IList<NoteTags> noteTag = NoteTagsDAL.GetAllTagsByNoteId(notes[0].NotesId);
                    bool isTagLinkedToNote = noteTag.Any(tag => tag.TagName == tagName);

                    noteTag.Clear();

                    NoteTagsDAL.DeleteTagFromNote(new NoteTags(tagName, notes[0].NotesId));
                    //Gets all the tags under that note after tag has been deleted
                    noteTag = NoteTagsDAL.GetAllTagsByNoteId(notes[0].NotesId);

                    bool isTagStillInNote = noteTag.Any(tag => tag.TagName == tagName);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(isTagLinkedToNote);
                    Assert.IsFalse(isTagStillInNote);
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

        /// <summary>
        /// Test if the NoteTag DAL method properly handles determining if a tag name is NOT under a note.
        /// </summary>
        [Test]
        public void TestTagNameIsNotUnderANote()
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
                    bool value = AccountDAL.CreateAccount("TestAccount", "TestPassword@", connection);
                    int? id = AccountDAL.GetAccountID("TestAccount", "TestPassword@");
                    int accountId = id ?? -1;
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);

                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    int sourceId = sources[0].SourceId;

                    NotesDAL.AddNoteToSource(sourceId, "Original Content");
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    string tagName = "CompSci";

                    //Did not add tag to any note

                    IList<NoteTags> noteTag = NoteTagsDAL.GetAllTagsByNoteId(notes[0].NotesId);

                    bool isTagExistingUnderNote = noteTag.Any(tag => tag.TagName == tagName);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsFalse(isTagExistingUnderNote);
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

        /// <summary>
        /// Test if the NoteTag DAL method properly handles determining if a tag name is under a note.
        /// </summary>
        [Test]
        public void TestTagNameIsUnderANote()
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
                    bool value = AccountDAL.CreateAccount("TestAccount", "TestPassword@", connection);
                    int? id = AccountDAL.GetAccountID("TestAccount", "TestPassword@");
                    int accountId = id ?? -1;
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);

                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    int sourceId = sources[0].SourceId;

                    NotesDAL.AddNoteToSource(sourceId, "Original Content");
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    string tagName = "CompSci";

                    NoteTagsDAL.AddTagToNote(tagName, notes[0].NotesId);

                    bool isTagExistingUnderNote = NoteTagsDAL.isTagExistingUnderNote(tagName, notes[0].NotesId);


                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(isTagExistingUnderNote);
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
        /// <summary>
        /// Test if the NoteTag DAL method properly handles getting all tags under a user ID.
        /// </summary>
        [Test]
        public void TestGetAllTagsUnderUserID()
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
                    bool value = AccountDAL.CreateAccount("TestAccount", "TestPassword@", connection);
                    int? id = AccountDAL.GetAccountID("TestAccount", "TestPassword@");
                    int accountId = id ?? -1;
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);

                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    int sourceId = sources[0].SourceId;

                    NotesDAL.AddNoteToSource(sourceId, "Original Content");
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    string tagName = "CompSci";

                    NoteTagsDAL.AddTagToNote(tagName, notes[0].NotesId);

                    IList<NoteTags> noteTag = NoteTagsDAL.GetAllTagsByUserId(accountId);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(noteTag.Count.Equals(1));
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
