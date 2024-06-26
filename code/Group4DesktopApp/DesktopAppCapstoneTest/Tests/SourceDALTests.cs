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
    /// Test Class for all the Source DAL Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class SourceDALTests
    {
        /// <summary>
        /// Test if the Source DAL method properly handles getting all sources under a userID
        /// </summary>
        [Test]
        public void TestGetAllSourcesFromUserID()
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
                    bool value = AccountDAL.CreateAccount("TestAccount", "TestPassword@",connection);
                    int? id = AccountDAL.GetAccountID("TestAccount", "TestPassword@");
                    Source newSource = new Source();
                    Source newSource2 = new Source();
                    int accountId = id ?? -1;
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    SourceDAL.AddNewSource(newSource.UserId, newSource2);
                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    
                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(sources.Count, Is.EqualTo(2));
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
        /// Test if the Source DAL method properly handles deleting a source and all notes attached to that source.
        /// </summary>
        [Test]
        public void TestDeleteSourceDeletesSourceAndNotes() {
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
                    Source newSource = new Source();
                    Source newSource2 = new Source();

                    int accountId = id ?? -1;
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    //SourceDAL.AddNewSource(newSource.UserId, newSource2);

                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);
                    int sourceId = sources[0].SourceId;

                    Notes newNote = new Notes();
                    newNote.SourceId = sourceId;
                    NotesDAL.AddNoteToSource(sourceId, newNote.Content);
                    IList<Notes> notes = NotesDAL.GetAllNotesBySourceId(sourceId);

                    SourceDAL.DeleteSource(sources[0]);
                    IList<Source> remainingSources = SourceDAL.GetAllSourcesByUserId(accountId);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(remainingSources.Count, Is.EqualTo(0));
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
        /// Test if the Source DAL method properly handles getting a source by its ID
        /// </summary>
        [Test]
        public void TestGetSourceByID()
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
                    Source newSource = new Source();
                    int accountId = id ?? -1;
                    newSource.UserId = accountId;
                    SourceDAL.AddNewSource(newSource.UserId, newSource);
                    IList<Source> sources = SourceDAL.GetAllSourcesByUserId(accountId);

                    int srcID = sources[0].SourceId;

                    Source source = SourceDAL.GetSourceById(srcID);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.That(srcID, Is.EqualTo(source.SourceId));
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
