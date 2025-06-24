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
    public class MarkController
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

        public List<Mark> GetAllMarks()
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
                            MId = Convert.ToInt32(reader["MId"]),
                            Marks = Convert.ToInt32(reader["Marks"]),
                            StId = Convert.ToInt32(reader["StId"]),
                            SuId = Convert.ToInt32(reader["SuId"]),
                            ExId = Convert.ToInt32(reader["ExId"])
                        });
                    }
                }
            }

            return marks;
        }





        public void UpdateMark(Mark mark)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Mark SET Marks = @Marks, StId = @StId, SuId = @SuId, ExId = @ExId WHERE MId = @MId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Marks", mark.Marks);
                    cmd.Parameters.AddWithValue("@StId", mark.StId);
                    cmd.Parameters.AddWithValue("@SuId", mark.SuId);
                    cmd.Parameters.AddWithValue("@ExId", mark.ExId);
                    cmd.Parameters.AddWithValue("@MId", mark.MId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void DeleteMark(int markId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Mark WHERE MId = @MId";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MId", markId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }



}
