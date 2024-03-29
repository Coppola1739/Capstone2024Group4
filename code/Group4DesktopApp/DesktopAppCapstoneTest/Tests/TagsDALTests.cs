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
    /// Test Class for all the Tags DAL Methods
    /// Author: Jeffrey Emekwue
    /// Version: Spring 2024
    /// </summary>
    public class TagsDALTests
    {
        /// <summary>
        /// Test if the Tags DAL method properly handles adding a new valid tag.
        /// </summary>
        [Test]
        public void TestAddValidNewTag()
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
                    TagsDAL.AddNewTag("AddedTag", connection);

                    bool isTagAdded = TagsDAL.isTagExisting("AddedTag");

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(isTagAdded);
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
        /// Test if the Tags DAL method properly handles not adding a tag if it is empty.
        /// </summary>
        [Test]
        public void TestAddEmptyNewTag()
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
                    bool isTagAdded = TagsDAL.AddNewTag(string.Empty, connection);

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsFalse(isTagAdded);
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
        /// Test if the Tags DAL method properly handles not adding a tag if it already exists.
        /// </summary>
        [Test]
        public void TestAddExistingTag()
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
                    TagsDAL.AddNewTag("AddedTag", connection);
                    bool isTagAdded = TagsDAL.AddNewTag("AddedTag");

                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsFalse(isTagAdded);
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
