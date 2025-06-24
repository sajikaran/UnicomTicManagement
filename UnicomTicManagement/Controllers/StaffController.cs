using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Controllers
{
    public class StaffController
    {

        public void AddStaff(  Staff staff)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Staff (StaffName) VALUES (@StaffName)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CName", staff.StaffName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Staff> GetAllStaff()
        {
            List<Staff> staff = new List<Staff>();
            using (var conn = DataConnect.GetConnection())



            {
                conn.Open();
                string query = "SELECT * FROM Staff";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        staff.Add(new Staff
                        {
                            StaffId = reader.GetInt32(0),
                            StaffName = reader.GetString(1)
                        });
                    }
                }
            }
            return staff;
        }

        public string GetStaffNameById(int StaffId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT StaffName FROM Staff WHERE StaffId = @StaffId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", StaffId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetString(0); // CName
                        }
                    }
                }
            }
            return null; // Or return an empty string or a message like "Course not found"
        }
        public void DeleteCourse(int StaffId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Staff WHERE StaffId = @StaffId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffId", StaffId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateCourse(Staff staff)
        {
            try
            {
                using (var conn = DataConnect.GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE Staff SET StaffName = @StaffName WHERE StaffId = @StaffId";
                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@StaffName", staff.StaffName);
                        cmd.Parameters.AddWithValue("@StaffId",staff.StaffId);
                        cmd.ExecuteNonQuery(); 
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating course: " + ex.Message);
            }
        }



    }
}
