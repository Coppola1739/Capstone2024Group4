﻿using Group4DesktopApp.DAL;
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
    /// Test Class for all the Notes DAL Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class NotesDALTests
    {
        /// <summary>
        /// Test if the Notes DAL method properly handles getting all notes by sourceID
        /// </summary>
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

        /// <summary>
        /// Test if the Notes DAL method properly handles getting all notes by userID
        /// </summary>
        [Test]
        public void TestGetAllNotesFromUserID()
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
                    int sourceId2 = sources[1].SourceId;

                    Notes newNote = new Notes();
                    newNote.SourceId = sourceId;
                    NotesDAL.AddNoteToSource(sourceId, newNote.Content);
                    NotesDAL.AddNoteToSource(sourceId2, newNote.Content);
                    IList<Notes> notes = NotesDAL.GetAllNotesByUserId(accountId);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(notes.Count, Is.EqualTo(2));
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
        /// Test if the Notes DAL method properly handles updating note content
        /// </summary>
        [Test]
        public void TestNoteContentIsUpdated()
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
                    string originalContent = notes[0].Content;

                    NotesDAL.UpdateNoteContent(notes[0].NotesId, "New Content");
                    IList<Notes> notesUpdated = NotesDAL.GetAllNotesBySourceId(sourceId);
                    string updatedContent = notesUpdated[0].Content;

                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(originalContent.ToUpper(), Is.Not.EqualTo(updatedContent.ToUpper()));
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
        /// Test if the Notes DAL method properly handles deleting notes
        /// </summary>
        [Test]
        public void TestNoteIsDeleted()
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
                    string originalContent = notes[0].Content;

                    bool isNoteDeleted = NotesDAL.DeleteNoteById(notes[0].NotesId);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(isNoteDeleted);
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
