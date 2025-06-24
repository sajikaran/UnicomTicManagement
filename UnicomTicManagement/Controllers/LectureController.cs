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
    public class LectureController
    {

        public void AddLecture(Lecturer lect)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Lecturer (LName, SuId) VALUES (@LNmae, @SuId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@LName", lect.LName);
                    cmd.Parameters.AddWithValue("@SuId", lect.SuId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public int AddLecturerAndReturnId(Lecturer lect)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = @"
                INSERT INTO Lecturer (LName, SuId)
                VALUES (@LName, @SuId);
                SELECT last_insert_rowid();"; // gets the auto-increment ID

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LName", lect.LName);
                    cmd.Parameters.AddWithValue("@SuId", lect.SuId);

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }



        public List<Lecturer> GetAllLecturers()
        {
            List<Lecturer> students = new List<Lecturer>();
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = @"
                            SELECT LId, LName, SuId
                            From Lecturer";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Lecturer
                        {
                            LId = reader.GetInt32(0),
                            LName = reader.GetString(1),
                            SuId = reader.GetInt32(2)
                        });
                    }
                }
            }
            return students;
        }

        public void DeleteLecturer(int LId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Lecturer WHERE LId = @LId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@LId", LId);
                    cmd.ExecuteNonQuery();
                }


            }
        }

        public void UpdateLecturer(Lecturer lect)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE  Lecturer SET StName = @LName, SuId = @SuId WHERE LId = @LId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StName", lect.LName);
                    cmd.Parameters.AddWithValue("@SuId", lect.SuId);
                    cmd.Parameters.AddWithValue("@LId", lect.LId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }

}












    

