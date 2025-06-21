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
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Mark (Marks,StId,SuId,ExId) VALUES (@Marks,@StId,@SuId,@ExId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Marks", mark.Marks);
                    cmd.Parameters.AddWithValue("@StId", mark.StId);
                    cmd.Parameters.AddWithValue("@SuId", mark.SuId);
                    cmd.Parameters.AddWithValue("@ExId", mark.ExId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Mark>GetAllMarks()
        {
            var marks = new List<Mark>();

            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT MId, Marks, StId, SuId, ExId FROM Mark";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        marks.Add(new Mark
                        {
                            MId = reader.GetInt32(0),
                            Marks = reader.GetInt32(1), 
                            StId = reader.GetInt32(2),
                            SuId = reader.GetInt32(3),
                            ExId = reader.GetInt32(4)
                        });
                    }
                }
            }

            return marks;
        }


    }

      
    
}
