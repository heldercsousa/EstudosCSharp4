using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application. The STAThread attribute applied to the entry-point method is also part of the singlethreaded plot in UI-driven applications and means single-threaded apartment, a term that goes back to the COM days
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new PersonForm());
            Application.Run(new CountDownForm());
        }
    }
}
