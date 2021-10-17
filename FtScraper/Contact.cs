using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtScraper
{
    public class Contact : Saveable
    {
        public int AccountId;
        public string PhoneNumber;
        public string FaxNumber;
        public string Email;
        public string FirstName;
        public string LastName;
        public string ContactType;

        public Account Account;

        public override string GetDescription()
        {
            return $"Contact with ID: { Id }, AccountId: { AccountId }";
        }

        public override void Save(SqlConnection connection)
        {
            if (AccountId == 0)
            {
                // If the AccountId wasn't known at the time of creation, get it now
                AccountId = Account.Id;
            }

            if (AccountId == 0) throw new Exception("Contact cannot be saved without an Account Id!");

            string sql;
            if (Id > 0) sql = "UPDATE [dbo].[Contact] SET AccountId = @AccountId, PhoneNumber = @PhoneNumber, FaxNumber = @FaxNumber, Email = @Email, FirstName = @FirstName, LastName = @LastName, ContactType = @ContactType WHERE Id = @Id;";
            else sql = "INSERT INTO [dbo].[Contact] (AccountId, PhoneNumber, FaxNumber, Email, FirstName, LastName, ContactType) VALUES (@AccountId, @PhoneNumber, @FaxNumber, @Email, @FirstName, @LastName, @ContactType);";

            using (SqlCommand Command = new SqlCommand(sql, connection))
            {
                Command.Parameters.Add("@AccountId", SqlDbType.Int).Value = AccountId;
                Command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 12).Value = PhoneNumber ?? "";
                Command.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 12).Value = FaxNumber ?? "";
                Command.Parameters.Add("@Email", SqlDbType.VarChar, 20).Value = Email ?? "";
                Command.Parameters.Add("@FirstName", SqlDbType.VarChar, 20).Value = FirstName ?? "";
                Command.Parameters.Add("@LastName", SqlDbType.VarChar, 20).Value = LastName ?? "";
                Command.Parameters.Add("@ContactType", SqlDbType.VarChar, 20).Value = ContactType ?? "";
                if (Id > 0) Command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

                Command.CommandType = CommandType.Text;

                Command.ExecuteNonQuery();
            }
        }

        public bool AlreadyExists(SqlConnection connection)
        {
            if (Id > 0) return true;

            string sql = "SELECT Id FROM [dbo].[Contact] WHERE PhoneNumber = @PhoneNumber AND FaxNumber = @FaxNumber AND Email = @Email AND FirstName = @FirstName AND LastName = @LastName AND ContactType = @ContactType;";

            using (SqlCommand Command = new SqlCommand(sql, connection))
            {
                Command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 15).Value = PhoneNumber;
                Command.Parameters.Add("@FaxNumber", SqlDbType.VarChar, 15).Value = FaxNumber;
                Command.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = Email;
                Command.Parameters.Add("@FirstName", SqlDbType.VarChar, 20).Value = FirstName;
                Command.Parameters.Add("@LastName", SqlDbType.VarChar, 20).Value = LastName;
                Command.Parameters.Add("@ContactType", SqlDbType.VarChar, 20).Value = ContactType;

                Command.CommandType = CommandType.Text;
                using (SqlDataReader reader = Command.ExecuteReader())
                {
                    reader.Read();
                    if (int.TryParse(reader["Id"].ToString(), out int newId))
                    {
                        Id = newId;
                    }
                }
            }

            return Id > 0;
        }
    }
}
