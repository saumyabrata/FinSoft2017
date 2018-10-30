using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FINNSOFT
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
            //Application.Run(new UI.FrmLogin());
            var start = new UI.FrmLogin();
            start.FormClosed += WindowClosed;
            start.Show();
            Application.Run();
        }
        static void WindowClosed(object sender, FormClosedEventArgs e)
        {
            if (Application.OpenForms.Count == 0) Application.Exit();
            else Application.OpenForms[0].FormClosed += WindowClosed;
        }
    }
}
