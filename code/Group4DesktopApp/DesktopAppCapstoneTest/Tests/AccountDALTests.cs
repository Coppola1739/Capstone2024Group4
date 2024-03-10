using System.Data.SqlClient;
using System.Transactions;
using Group4DesktopApp.DAL;

namespace DesktopAppCapstoneTest.Tests
{
    public class AccountDALTests
    {

        [Test]
        public void CreateAccountDALisValid()
        {
            bool success = false;
            using (TransactionScope scop = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                using var connection = new SqlConnection(Connection.ConnectionString);
                connection.Open();
                using SqlTransaction myTrans = connection.BeginTransaction();
                using var myCommand = connection.CreateCommand();
                myCommand.Transaction = myTrans;
                try
                {
                    bool value = AccountDAL.CreateAccount("TestShouldBeDeleted", "TestPassword@", connection);
                    if (value == true)
                    {
                        success = true;
                    }
                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(success);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    myTrans.Rollback();
                    success = false;
                    connection.Close();
                    Assert.IsTrue(success);
                }
            }
        }

        [Test]
        public void CreateAccountUserNameAlreadyExists()
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
                    bool firstEntry = AccountDAL.CreateAccount("TestShouldBeDeleted", "TestPassword@", connection);
                    bool noDuplicateAccount = true;
                    if (firstEntry == true)
                    {
                        noDuplicateAccount = AccountDAL.CreateAccount("TestShouldBeDeleted", "TestPassword2@");
                        Assert.IsFalse(noDuplicateAccount);
                    }
                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsFalse(noDuplicateAccount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    myTrans.Rollback();
                    connection.Close();
                    Assert.Fail();
                }
            }
        }

        [Test]
            public void TestAccountIDNotFound()
            {
                bool noResult = false;
                using (TransactionScope scop = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    using var connection = new SqlConnection(Connection.ConnectionString);
                    connection.Open();
                    using SqlTransaction myTrans = connection.BeginTransaction();
                    using var myCommand = connection.CreateCommand();
                    myCommand.Transaction = myTrans;
                    try
                    {
                        int? value = AccountDAL.GetAccountID( "", "",connection);
                        if (value == null)
                        {
                        noResult = true;
                        }
                        myTrans.Rollback();
                        connection.Close();
                        Assert.IsTrue(noResult);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        myTrans.Rollback();
                        noResult = false;
                        connection.Close();
                    Assert.Fail(ex.Message);
                    }
                }
            }

        [Test]
        public void TestGetAccountIDByCredentials()
        {
            bool success = false;
            using (TransactionScope scop = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                using var connection = new SqlConnection(Connection.ConnectionString);
                connection.Open();
                using SqlTransaction myTrans = connection.BeginTransaction();
                using var myCommand = connection.CreateCommand();
                myCommand.Transaction = myTrans;
                try
                {
                    bool firstEntry = AccountDAL.CreateAccount("DummyAcc", "TestPassword@", connection);
                    int? value = AccountDAL.GetAccountID("DummyAcc", "TestPassword@");
                    if (value != null)
                    {
                        success = true;
                    }
                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsTrue(success);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        myTrans.Rollback();
                        connection.Close();
                    }
                    success = false;
                    Assert.Fail(ex.Message);
                }
            }
        }

    }
}
