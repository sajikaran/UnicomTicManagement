using System;
using System.Collections.Generic;
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
    public static class StudentController
    {
        public static void AddStudent(Student student)
        {
            using (var st = DataConnect.GetConnection())
            {
                string addStuQuery = "INSERT INTO Table (StName) VALUES (@StName)";
                using (SQLiteCommand cd = new SQLiteCommand(addStuQuery, st))
                {
                    cd.Parameters.AddWithValue("@StName", student.StName);
                    cd.ExecuteNonQuery();
                }
            }
        }
    }

}
