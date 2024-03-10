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
    public class UserDALTests
    {
        [Test]
        public void TestGetUserByID()
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
                    User? retrievedUser = null;
                    if (value == true)
                    {
                        retrievedUser = UserDAL.GetUserByID(1);
                    }
                    myTrans.Rollback();
                    connection.Close();
                    Assert.IsNotNull(retrievedUser);
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
