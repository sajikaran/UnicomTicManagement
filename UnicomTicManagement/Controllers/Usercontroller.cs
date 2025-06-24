using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Data;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UnicomTicManagement.Controllers
{
    public class Usercontroller
    {

        public bool CreateStudentUser(string studentName, int studentId, out string generatedUsername, out string generatedPassword)
        {
            generatedUsername = studentName.ToLower().Replace(" ", "") + studentId;
            generatedPassword = "stu" + studentId;

            try
            {
                using (SQLiteConnection connection = DataConnect.GetConnection())
                {
                    connection.Open();

                    string insertUser = @"
                    INSERT INTO Users (Username, Password, Role, StId)
                    VALUES (@Username, @Password, 'Student', @StId);";

                    using (SQLiteCommand cmd = new SQLiteCommand(insertUser, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", generatedUsername);
                        cmd.Parameters.AddWithValue("@Password", generatedPassword);
                        cmd.Parameters.AddWithValue("@StId", studentId);

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                // Optional: log error
                Console.WriteLine("Error inserting user: " + ex.Message);
                return false;



            }
        }



        public bool CreateLecturerUser(string lecturerName, int lecturerId, out string username, out string password)
        {
            username = lecturerName.ToLower().Replace(" ", "") + lecturerId;
            password = "lect" + lecturerId;

            try
            {
                using (var conn = DataConnect.GetConnection())
                {
                    conn.Open();
                    string query = @"
                INSERT INTO Users (Username, Password, Role, StId)
                VALUES (@Username, @Password, 'Lecturer', NULL);";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating lecturer user: " + ex.Message);
                return false;
            }
        }
        public bool CreateStaffUser(string staffName, int staffId, out string username, out string password)
        {
            username = staffName.ToLower().Replace(" ", "") + staffId;
            password = "staff" + staffId;

            try
            {
                using (SQLiteConnection conn = DataConnect.GetConnection())
                {
                    conn.Open();
                    string insertQuery = @"
                INSERT INTO Users (Username, Password, Role, StId)
                VALUES (@Username, @Password, 'Staff', NULL);";

                    using (var cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating staff user: " + ex.Message);
                return false;
            }
        }






    }


}