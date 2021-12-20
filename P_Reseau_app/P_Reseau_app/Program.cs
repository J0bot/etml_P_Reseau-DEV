using System;
using System.Windows.Forms;

namespace P_Reseau_app
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Controller controller = new Controller(new View());
            Application.Run(controller.view);
        }
    }

}
