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

        public void AddStaff(Staff staff)
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


        public int AddStaffAndReturnId(Staff staff)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = @"
                INSERT INTO Staff(StaffName)
                VALUES (@StaffName);
                SELECT last_insert_rowid();"; // gets the auto-increment ID

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StaffName", staff.StaffName);
                   

                    object result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
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
