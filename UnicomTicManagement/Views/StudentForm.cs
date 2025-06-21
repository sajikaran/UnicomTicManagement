using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Controllers;
using UnicomTicManagement.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UnicomTicManagement.Views
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
            LoadStudents();
            LoadCourses();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


            string studentName = textStName.Text.Trim();
            string courseName = txtcompobox.Text.Trim();

            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(courseName))
            {
                MessageBox.Show("Please enter both Student Name and Course Name.");
                return;
            }

            CourseController courseController = new CourseController();
            StudentController studentController = new StudentController();

            // Check if course already exists
            Course existingCourse = courseController.GetAllCourses()
                .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

            // If not, insert the course
            if (existingCourse == null)
            {
                Course newCourse = new Course { CName = courseName };
                courseController.AddCourse(newCourse);

                // Reload and get inserted course
                existingCourse = courseController.GetAllCourses()
                    .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (existingCourse == null)
                {
                    MessageBox.Show("Course insertion failed.");
                    return;
                }
            }

            // Add student with course ID
            Student newStudent = new Student
            {
                StName = studentName,
                CId = existingCourse.CId
            };

            studentController.AddStudent(newStudent);

            MessageBox.Show("Student added successfully.");

            LoadStudents();
            textStName.Text = "";
            txtcompobox.Text = "";
        }

        private void txtcomobox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void StudentForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadStudents()
        {
            StudentController studentController = new StudentController();
            CourseController courseController = new CourseController();

            var students = studentController.GetAllStudents();
            var courses = courseController.GetAllCourses();

            var studentDisplayList = (from student in students
                                      join course in courses on student.CId equals course.CId
                                      select new
                                      {
                                          ID = student.StId,
                                          Name = student.StName,
                                          Course = course.CName
                                      }).ToList();

            dataGridView1.DataSource = studentDisplayList;
        }


        private void LoadCourses()
        {
            CourseController courseController = new CourseController();
            var courses = courseController.GetAllCourses();
            // Bind the courses to the combo box
            txtcompobox.DataSource = courses;
            txtcompobox.DisplayMember = "CName"; // Display name in the combo box
            txtcompobox.ValueMember = "CId"; // Use CId as value
        }

        private void textStName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Get the selected StId from the first selected row
                int selectedStId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Optional: Confirm before delete
                var confirmResult = MessageBox.Show("Are you sure to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    // Call the delete method from StudentController
                    StudentController controller = new StudentController();
                    controller.DeleteStudent(selectedStId);

                    // Refresh the DataGridView
                    LoadStudents(); // Make sure you have a method that reloads the data
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.");
            }
        }

        private void button3_Click(object sender, EventArgs e)

        {



        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

    }
}



