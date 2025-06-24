using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnicomTicManagement.Data
{
    public static class DataConnect
    {
        private static string connecter = "Data Source=unicomtic.db";
            

        public static SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(connecter);
            
            return connection;
        }
    }

}






