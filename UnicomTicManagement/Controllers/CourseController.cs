using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Controllers
{
    public   class CourseController
    {
        public void AddCourse(Course course)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Course (CName) VALUES (@CName)";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CName", course.CName);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Course";
                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CId = reader.GetInt32(0),
                            CName = reader.GetString(1)
                        });
                    }
                }
            }
            return courses;
        }

        public string GetCourseNameById(int cId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "SELECT CName FROM Course WHERE CId = @CId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CId", cId);
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
        public void DeleteCourse(int cId)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Course WHERE CId = @CId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CId", cId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void UpdateCourse(int courseId, string newCourseName)
        {
            using (var conn = DataConnect.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Course SET CName = @CName WHERE CId = @CId";
                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@CName", newCourseName);
                    cmd.Parameters.AddWithValue("@CId", courseId);

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (SQLiteException ex)
                    {
                        if (ex.ResultCode == SQLiteErrorCode.Constraint)
                            throw new Exception("A course with this name already exists.");
                        throw;
                    }
                }
            }
        }




    }










}








    


