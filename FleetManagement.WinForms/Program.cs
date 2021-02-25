using FleetManagement.WinForms.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Drawing.Text;
using System.Windows.Forms;

namespace FleetManagement.WinForms
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            using var provider = services
                .ConfigureServices()
                .BuildServiceProvider();

            var mainForm = provider.GetRequiredService<MainForm>();

            Application.Run(mainForm);
        }
    }
}
