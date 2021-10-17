using Brcgs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtScraper
{
    class BrcgsTasks
    {
        public static Queue<int> FoundCompanies = new Queue<int>();

        public static List<Task> RunPage(int pageNumber)
        {
            if (MainForm.token.IsCancellationRequested) return null;
            string Url = string.Format("https://api.brcgs.com/PublicDirectory/api/PublicDirectory/site?Country=United%20Kingdom&searchTerm=&pageNumber={0}&order=asc", pageNumber);

            BrcgsList result;
            try
            {
                result = Common.LoadApi<BrcgsList>(Url);
            }
            catch (Exception ex)
            {
                MainForm.AddToLog($"Error on page { pageNumber + 1 }: { ex.Message }");
                return null;
            }

            // Setup for first time
            if (Common.ResultsPerPage == 0 && result.resultsList.Length > 0)
            {
                Common.ResultsPerPage = result.resultsList.Length;
                Common.TotalPages = result.totalPages + 1; // Add one because page 0 counts towards the work
                Common.TotalCompanies = Common.TotalPages * Common.ResultsPerPage; // Estimate, assumes the final page is full
            }
            else if (Common.TotalPages == pageNumber + 1)
            {
                Common.TotalCompanies = ((Common.TotalPages - 1) * Common.ResultsPerPage) + result.resultsList.Length; // Actual, using the final page's count
            }
            if (MainForm.token.IsCancellationRequested) return null;

            List<Task> companyTasks = new List<Task>();

            foreach (var company in result.resultsList)
            {
                if (MainForm.token.IsCancellationRequested) return companyTasks;
                companyTasks.Add(Task.Run(() => RunCompany(company.id)));
            }

            MainForm.AddToLog($"Searched page { pageNumber + 1 } with { result.resultsList.Length } results");

            Common.SearchedPages++;
            return companyTasks;
        }

        public static void RunCompany(int id)
        {

            string Url = string.Format("https://api.brcgs.com/PublicDirectory/api/PublicDirectory/site/{0}", id);

            BrcgsCompany company;
            try
            {
                company = Common.LoadApi<BrcgsCompany>(Url);
            }
            catch (Exception ex)
            {
                MainForm.AddToLog($"Error on company { id }: { ex.Message }");
                return;
            }

            MainForm.AddToLog($"Found: { company.name }");

            StringBuilder builder = new StringBuilder();
            foreach (var category in company.auditDetails[0].categories)
            {
                builder.Append(category);
            }

            Account acc = new Account()
            {
                Name = company.name,
                BrgcsId = company.id,
                BrcgsCategories = builder.ToString(),
                BrcgsGrade = company.auditDetails[0].grade,
                BrcgsStandard = company.auditDetails[0].standard
            };
            Common.UnsavedRecords.Add(acc);

            foreach (var contact in company.contacts)
            {
                var cont = new Contact()
                {
                    Account = acc,
                    ContactType = contact.contactType,
                    Email = contact.email,
                    FaxNumber = contact.faxNumber,
                    FirstName = contact.firstName,
                    LastName = contact.lastName,
                    PhoneNumber = contact.phoneNumber,
                };
                Common.UnsavedRecords.Add(cont);
            }

            Common.SearchedCompanies++;
        }
    }
}
