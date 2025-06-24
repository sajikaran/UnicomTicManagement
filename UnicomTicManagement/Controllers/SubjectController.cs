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
    public class SubjectController
    {
        
        public void AddSubject(Subject subject)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Subject (SuName, CourseId) VALUES (@SuName, @CourseId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuName", subject.SuName);
                    cmd.Parameters.AddWithValue("@CourseId", subject.CId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
        public List<Subject> GetAllSubjects()
        {
            var subjects = new List<Subject>();

            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT SuId, SuName, CourseId FROM Subject";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        subjects.Add(new Subject
                        {
                            SuId = reader.GetInt32(0),
                            SuName = reader.GetString(1),
                            CId = reader.GetInt32(2)
                        });
                    }
                }
            }

            return subjects;
        }



        public void UpdateSubject(Subject subject)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Subject SET SuName = @SuName, CourseId = @CourseId WHERE SuId = @SuId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuName", subject.SuName);
                    cmd.Parameters.AddWithValue("@CourseId", subject.CId);
                    cmd.Parameters.AddWithValue("@SuId", subject.SuId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSubject(int subjectId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Subject WHERE SuId = @SuId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@SuId", subjectId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


    }



}


