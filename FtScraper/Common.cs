using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FtScraper
{
    static class Common
    {
        // Global
        public static int ResultsPerPage;

        public static List<Saveable> UnsavedRecords = new List<Saveable>();

        public static List<Saveable> FailedRecords = new List<Saveable>();
        static string connectionString => ConfigurationManager.AppSettings["ConnectionString"];


        private static int _companyNumbers;
        public static int TotalCompanies
        {
            get { return _companyNumbers; }
            set
            {
                _companyNumbers = value;
                MainForm.UpdateCompanyValue();
            }
        }
        private static int _searchedCompanies;
        public static int SearchedCompanies
        {
            get { return _searchedCompanies; }
            set
            {
                _searchedCompanies = value;
                MainForm.UpdateCompanyValue();
            }
        }

        private static int _pageNumbers;
        public static int TotalPages
        {
            get { return _pageNumbers; }
            set
            {
                _pageNumbers = value;
                MainForm.UpdatePageValue();
            }
        }
        private static int _searchedPages;
        public static int SearchedPages
        {
            get { return _searchedPages; }
            set
            {
                _searchedPages = value;
                MainForm.UpdatePageValue();
            }
        }

        public static T LoadApi<T>(string Url)
        {
            using (System.Net.WebClient wc = new System.Net.WebClient())
                return JsonSerializer.Deserialize<T>(wc.DownloadString(Url));
        }

        public static void WriteRecordsToDatabase()
        {
            lock (UnsavedRecords)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    foreach (var ToSave in UnsavedRecords)
                    {
                        if (MainForm.token.IsCancellationRequested)
                        {
                            UnsavedRecords.Clear();
                            return;
                        }

                        try
                        {
                            ToSave.Save(connection);
                        }
                        catch (Exception)
                        {
                            MainForm.AddToLog($"Failed to add record { ToSave.GetDescription() }");
                            FailedRecords.Add(ToSave);
                        }
                    }
                }

                UnsavedRecords.Clear();
            }
        }
    }

    public abstract class Saveable
    {
        public int Id;
        public abstract void Save(SqlConnection connection);
        public abstract string GetDescription();
    }
}
