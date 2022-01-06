using System;
using System.Windows.Forms;

namespace FleetManager
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginMenu());
        }
    }

    static class GenerateDatabase
    {
        static void Main()
        {
            Generator generator = new Generator();
            generator.InitializeDb();
        }
    }
}
