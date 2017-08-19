using System;
using System.Windows.Forms;

namespace Neitzel.Forms.Example
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormsApplication.Run(new ExampleForm());
        }
    }
}
