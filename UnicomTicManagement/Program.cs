using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Data;

namespace UnicomTicManagement
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            
                
                    DataMigration.createTable();
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new MainForm());
                
            
        }
    }

}




