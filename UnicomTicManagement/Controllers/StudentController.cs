using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;
using static System.Resources.ResXFileRef;

namespace UnicomTicManagement.Controllers
{
    public class StudentController
    {

        public void AddStudent(Student student)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Student (StName, CId) VALUES (@StName, @CId)";
                using (var cmd = new SQLiteCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@StName", student.StName);
                    cmd.Parameters.AddWithValue("@CId", student.CId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = @"
                            SELECT StId, StName, CId
                            From Student";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StId = reader.GetInt32(0),
                            StName = reader.GetString(1),
                            CId = reader.GetInt32(2)
                        });
                    }
                }
            }
            return students;
        }

        public void DeleteStudent(int StId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Student WHERE StId = @StId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StId", StId);
                    cmd.ExecuteNonQuery();
                }


            }
        }

        public void UpdateStudent(Student student)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Student SET StName = @StName, CId = @CId WHERE StId = @StId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StName", student.StName);
                    cmd.Parameters.AddWithValue("@CId", student.CId);
                    cmd.Parameters.AddWithValue("@StId", student.StId);
                    cmd.ExecuteNonQuery();
                }
            }
        }           

    }

}











