using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;
using static System.Resources.ResXFileRef;

namespace UnicomTicManagement.Controllers
{
    public  class ExamController
    {
    
        public  void AddExam(Exam ex) 
        {
            using (var Ex = DataConnect.GetConnection()) 
            {
                string Examquery = "INSERT INTO Table ( EName) VALUES ( @EName)";
                using (SQLiteCommand cmd = new SQLiteCommand(Examquery, Ex))
                {
                    cmd.Parameters.AddWithValue("@CName", ex.EName);
                    cmd.ExecuteNonQuery();
                }
            }



        }
    }
}
