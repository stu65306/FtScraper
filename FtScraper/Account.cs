using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtScraper
{
    public class Account : Saveable
    {
        public string Name;
        public int BrgcsId;
        public string BrcgsGrade;
        public string BrcgsStandard;
        public string BrcgsCategories;

        public override string GetDescription()
        {
            return $"Account with ID: { Id }, Name: { Name }";
        }
        public override void Save(SqlConnection connection)
        {
            // Get our account using BrgcsId
            if (BrgcsId > 0) GetIdFromBrcgs(connection, BrgcsId);

            string sql;
            if (Id > 0) sql = "UPDATE [dbo].[Account] SET Name = @Name, BrcgsId = @BrcgsId, BrcgsGrade = @BrcgsGrade, BrcgsStandard = @BrcgsStandard, BrcgsCategories = @BrcgsCategories WHERE Id = @Id;";
            else sql = "INSERT INTO [dbo].[Account] (Name, BrcgsId, BrcgsGrade, BrcgsStandard, BrcgsCategories) OUTPUT INSERTED.ID VALUES (@Name, @BrcgsId, @BrcgsGrade, @BrcgsStandard, @BrcgsCategories);";

            using (SqlCommand Command = new SqlCommand(sql, connection))
            {
                Command.Parameters.Add("@Name", SqlDbType.VarChar, 10).Value = Name;
                Command.Parameters.Add("@BrcgsId", SqlDbType.Int).Value = BrgcsId;
                Command.Parameters.Add("@BrcgsGrade", SqlDbType.VarChar, 10).Value = BrcgsGrade ?? "";
                Command.Parameters.Add("@BrcgsStandard", SqlDbType.VarChar, 50).Value = BrcgsStandard ?? "";
                Command.Parameters.Add("@BrcgsCategories", SqlDbType.VarChar, 500).Value = BrcgsCategories ?? "";
                if (Id > 0) Command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;

                Command.CommandType = CommandType.Text;


                if (Id > 0) Command.ExecuteNonQuery();
                else Id = (int)Command.ExecuteScalar();
            }
        }

        public void GetIdFromBrcgs(SqlConnection connection, int brcgsId)
        {
            string sql = "SELECT Id FROM [dbo].[Account] WHERE BrcgsId = @BrcgsId;";

            using (SqlCommand Command = new SqlCommand(sql, connection))
            {
                Command.Parameters.Add("@BrcgsId", SqlDbType.Int).Value = brcgsId;

                Command.CommandType = CommandType.Text;

                var result = Command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int newId)) Id = newId;
            }
        }
    }
}
