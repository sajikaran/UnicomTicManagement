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
    public  class CourseController

    {
        public void AddSub_Course(Course course)
        {
            {
                using (var conn = DataConnect.GetConnection())
                {
                    string query = "INSERT INTO Course (CName) VALUES (@CName)";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CName", course.CName);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }




            public static List<Course> GetAllCourses()
            {
                var courses = new List<Course>();
                using (var conn = DataConnect.GetConnection())
                {
                    string query = "SELECT * FROM Course";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            courses.Add(new Course
                            {
                                CId = Convert.ToInt32(reader["CId"]),
                                CName = reader["CName"].ToString()
                            });
                        }
                    }
                }
                return courses;
            }
        }

    }





    


