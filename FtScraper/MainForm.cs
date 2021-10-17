using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FtScraper
{
    public partial class MainForm : Form
    {
        List<Task> PageTasks = new List<Task>();
        List<Task> CompanyTasks = new List<Task>();
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        public static CancellationToken token = new CancellationToken();

        public static MainForm instance;

        public MainForm()
        {
            InitializeComponent();
            instance = this;
        }


        private void CancelBtn_Click(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        private void RunBtn_Click(object sender, EventArgs e)
        {
            RunBtn.Visible = false;
            progressBar.Visible = true;
            WorkingPanel.Visible = true;
            token = tokenSource.Token;

            Stopwatch st = new Stopwatch();
            st.Start();

            // First run get's info such as how many pages, estimates the amount of work
            Task.Run(() =>
            {
                var companyTasks = BrcgsTasks.RunPage(0);
                CompanyTasks.AddRange(companyTasks);
            }, token).ContinueWith(x =>
                {
                    // Go from last till first, as we can correct our estimate by running the last one
                    // See the BrcgsTasks.RunPage logic
                    for (var i = Common.TotalPages - 1; i > 0; i--)
                    {
                        int thisIndex = i; // Can't use i, as it's continuously assigned to
                        var task = Task.Run(() =>
                        {
                            var companyTasks = BrcgsTasks.RunPage(thisIndex);
                            if (companyTasks != null) PageTasks.AddRange(companyTasks);
                        }, token);
                        PageTasks.Add(task);
                    }

                    // Write to DB when reads are done
                    Task.WhenAll(PageTasks.ToArray()).ContinueWith(x =>
                    {
                        Task.WhenAll(CompanyTasks.ToArray()).ContinueWith(x =>
                        {
                            if (!token.IsCancellationRequested) Common.WriteRecordsToDatabase();
                            HideShowFromDifferentThread(instance.CancelButton, false);
                            st.Stop();
                            UpdateLabelFromDifferentThread(instance.StopwatchLabel, $"Completed in: { st.Elapsed.ToString("h'h 'm'm 's's'") }");
                        });
                    });
                });
        }

        public static void UpdatePageValue()
        {
            UpdateLabelFromDifferentThread(instance.pageLabel, $"{ Common.SearchedPages } / { Common.TotalPages } Pages");
        }

        public static void UpdateCompanyValue()
        {
            UpdateLabelFromDifferentThread(instance.companyLabel, $"{ Common.SearchedCompanies } / { Common.TotalCompanies } Companies");
            UpdateProgressBarFromDifferentThread(instance.progressBar, Common.SearchedCompanies, Common.TotalCompanies);
        }


        public static void AddToLog(string message)
        {
            UpdateLabelFromDifferentThread(instance.logLabel, message + "\n" + instance.logLabel.Text);
        }

        /// <summary>
        /// This method allows tasks to write back to the UI using Begin Evoke
        /// This is important as trying to write to the UI from a different thread is not supported. 
        /// </summary>
        /// <param name="label">Label to write to</param>
        /// <param name="text">Label's new text</param>
        private static void UpdateLabelFromDifferentThread(Label label, string text)
        {

            object[] inputArray = new object[2];
            inputArray[0] = label;
            inputArray[1] = text;

            _ = instance.BeginInvoke(new TextDelegate(instance.TextDelegateMethod), inputArray);
        }

        public delegate void TextDelegate(Label label, string text);
        public void TextDelegateMethod(Label label, string text)
        {
            lock (instance)
            {
                label.Text = text;
            }
        }

        private static void UpdateProgressBarFromDifferentThread(ProgressBar bar, int amount, int max)
        {
            if (bar.Value == amount && bar.Maximum == max) return;

            object[] inputArray = new object[3];
            inputArray[0] = bar;
            inputArray[1] = amount;
            inputArray[2] = max;

            _ = instance.BeginInvoke(new ProgressDelegate(instance.ProgressDelegateMethod), inputArray);
        }

        public delegate void ProgressDelegate(ProgressBar bar, int amount, int max);
        public void ProgressDelegateMethod(ProgressBar bar, int amount, int max)
        {
            lock (instance)
            {
                bar.Value = amount;
                bar.Maximum = max;
            }
        }

        private static void HideShowFromDifferentThread(Control control, bool show)
        {

            object[] inputArray = new object[2];
            inputArray[0] = control;
            inputArray[1] = show;

            _ = instance.BeginInvoke(new ShowHideDelegate(instance.ShowHideDelegateMethod), inputArray);
        }

        public delegate void ShowHideDelegate(Control control, bool show);
        public void ShowHideDelegateMethod(Control control, bool show)
        {
            lock (instance)
            {
                control.Visible = show;
            }
        }
    }
}
