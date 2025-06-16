using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Controllers
{
    public   class MarkController
    {
        public void AddMarks(Mark mark) 
        {
            using (var m = DataConnect.GetConnection())
            {
                string AddmarksQuery = "INSER INTO THE TABLE(Marks)VALUES(@Marks)";
                SQLiteCommand ma=new SQLiteCommand(AddmarksQuery,m);
                ma.Parameters.AddWithValue("@Markas",mark.Marks);
                ma.ExecuteNonQuery();
            }



        }
    }
}
