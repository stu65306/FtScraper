using Microsoft.VisualStudio.TestTools.UnitTesting;
using FtScraper;
using System.Data.SqlClient;
using System.Configuration;

namespace FtScraperTests
{
    [TestClass]
    public class DatabaseTests
    {
        static string connectionString => "Server=localhost;Database=FtScraper;Trusted_Connection=True;";

        Account MinAccount => new Account() { Name = "Test" }; // Minimum required info
        Contact MinContact => new Contact(); // Required info is AccountId, not known here

        [TestMethod]
        public void CheckAccDesc()
        {
            Assert.AreEqual(MinAccount.GetDescription(), "Account with ID: 0, Name: Test", "Account description incorrect");
        }

        [TestMethod]
        public void SaveAccount()
        {
            var acc = MinAccount;
            using SqlConnection connection = new(connectionString);
            connection.Open();
            acc.Save(connection);
            Assert.IsNotNull(acc.Id, "Account not saved correctly");
        }

        [TestMethod]
        public void SaveContactWithAccountId()
        {
            var acc = MinAccount;
            using SqlConnection connection = new(connectionString);
            connection.Open();
            acc.Save(connection);
            if (acc.Id == null || acc.Id == 0) Assert.Fail("Account not saved correctly");

            var cont = MinContact;
            cont.AccountId = acc.Id;
            cont.Save(connection);
            Assert.IsNotNull(cont.Id, "Contact not saved correctly");
        }

        [TestMethod]
        public void SaveContactWithAccountReference()
        {
            var acc = MinAccount;
            using SqlConnection connection = new(connectionString);
            connection.Open();
            acc.Save(connection);
            if (acc.Id == null || acc.Id == 0) Assert.Fail("Account not saved correctly");

            var cont = MinContact;
            cont.Account = acc;
            cont.Save(connection);
            Assert.IsNotNull(cont.Id, "Contact not saved correctly");
        }
    }
}
