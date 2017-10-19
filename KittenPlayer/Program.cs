using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KittenPlayer
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
            
            //NativeMethods.AllocConsole();
            //Console.WriteLine("Debug Console");

            Application.Run(new MainWindow());
        }
    }

    internal static class NativeMethods
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int AllocConsole();
        
        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern int FreeConsole();
    }
}
