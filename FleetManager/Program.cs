using System;
using System.Windows.Forms;

namespace FleetManager
{ 
    static class Program
    {
        public static char[] LegalChars = "aąbcćdeęfghijklłmnńoóprqsśtuwxyzźżAĄBCĆDEĘFGHIJKLŁMNŃOÓPRQSŚTUWXYZŹŻ0123456789., @!()%".ToCharArray();
        public static char[] Digits = "1234567890".ToCharArray();
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
