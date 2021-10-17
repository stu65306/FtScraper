using System;
using System.Configuration;
using System.Windows.Forms;

namespace FtScraper
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (ConfigurationManager.AppSettings["ConnectionString"] == null) ConfigurationManager.AppSettings["ConnectionString"] = "Server=localhost;Database=FtScraper;Trusted_Connection=True;";

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
