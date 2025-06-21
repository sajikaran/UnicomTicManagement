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

    public class ExamController
    {
        public void AddExam(Exam exam)
        {
            try
            {
                using (var connection = DataConnect.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Exam (ExName, SuId) VALUES (@ExName, @SuId)";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ExName", exam.ExName);
                    cmd.Parameters.AddWithValue("@SuId", exam.SuId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding exam: " + ex.Message);
            }
        }

        public List<Exam> GetAllExams()
        {
            List<Exam> exams = new List<Exam>();
            try
            {
                using (var connection = DataConnect.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT ExId, ExName, SuId FROM Exam";
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Exam exam = new Exam
                        {
                            ExId = Convert.ToInt32(reader["ExId"]),
                            ExName = reader["ExName"].ToString(),
                            SuId = Convert.ToInt32(reader["SuId"])
                        };
                        exams.Add(exam);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching exams: " + ex.Message);
            }
            return exams;
        }

    



            public void DeleteExam(int examId)
            {
                try
                {
                    using (var connection = DataConnect.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Exam WHERE ExId = @ExId";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.Parameters.AddWithValue("@ExId", examId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting exam: " + ex.Message);
                }
            }

            public void UpdateExam(Exam exam)
            {
                try
                {
                    using (var connection = DataConnect.GetConnection())
                    {
                        connection.Open();
                        string query = "UPDATE Exam SET ExName = @ExName WHERE ExId = @ExId";
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        cmd.Parameters.AddWithValue("@ExName", exam.ExName);
                        cmd.Parameters.AddWithValue("@ExId", exam.ExId);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating exam: " + ex.Message);
                }
            }
        }
    }




