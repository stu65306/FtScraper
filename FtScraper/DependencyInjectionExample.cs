using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtScraper
{
    public interface IDbConnection
    {
        void Write(Saveable ToSave);
    }

    public class DbConnection : IDbConnection
    {
        private readonly SqlConnection sqlConnection;

        DbConnection()
        {
            sqlConnection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            sqlConnection.Open();
            // TODO: Make sure this is disposed correctly!
        }

        public void Write(Saveable ToSave)
        {
            ToSave.Save(sqlConnection);
        }
    }

    public class DbConnectionExample
    {
        private readonly IDbConnection _connection;

        public DbConnectionExample(IDbConnection connection) =>
            _connection = connection;

        private void DoExample()
        {
            _connection.Write(new Account() { Name = "Test" });
        }
    }
}
