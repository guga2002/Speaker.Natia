using Jandagashvili.speake.DLL.Kontext;
using Speaker.leison.Business_layer.Services;
using Speaker.leison.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Speaker.Natia.Jandagovna
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            PortCheckAndRefresh refrshi = new PortCheckAndRefresh();
        aqa:
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Speakerdb db = new Speakerdb();

                Application.Run(new Main());

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                goto aqa;
            }

        }
    }
}
