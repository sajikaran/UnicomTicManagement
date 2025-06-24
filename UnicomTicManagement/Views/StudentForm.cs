using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Controllers;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UnicomTicManagement.Views
{
    public partial class StudentForm : Form
    {
        private string _role;
        private int? _studentId;

        public StudentForm()
        {
            InitializeComponent();
            LoadStudents();
            LoadCourses();

            switch (_role)
            {
                case "Admin":

                    break;

            }
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
            Usercontroller usersController = new Usercontroller();

            
            Course existingCourse = courseController.GetAllCourses()
                .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

            if (existingCourse == null)
            {
                courseController.AddCourse(new Course { CName = courseName });
                existingCourse = courseController.GetAllCourses()
                    .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (existingCourse == null)
                {
                    MessageBox.Show("Course insertion failed.");
                    return;
                }
            }

            
            Student newStudent = new Student
            {
                StName = studentName,
                CId = existingCourse.CId
            };

            int newStudentId = studentController.AddStudentAndReturnId(newStudent);
            if (newStudentId <= 0)
            {
                MessageBox.Show("Student insertion failed.");
                return;
            }

            
            if (usersController.CreateStudentUser(studentName, newStudentId, out string username, out string password))
            {
                MessageBox.Show($"Student added!\nUsername: {username}\nPassword: {password}", "Success");
                MessageBox.Show("Sucessfully Added Student");
            }
            else
            {
                MessageBox.Show("User account creation failed.");
            }

            LoadStudents();
            textStName.Text = "";
            txtcompobox.Text = "";
            LoadCourses();
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


            var studentDisplayList = students.Select(s => new
            {
                CourseID = s.CId,
                ID = s.StId,
                StudentName = s.StName
            }).ToList();

            dataGridView1.DataSource = studentDisplayList;


        }


        private void LoadCourses()
        {
            CourseController courseController = new CourseController();
            var courses = courseController.GetAllCourses();
            
            txtcompobox.DataSource = courses;
            txtcompobox.DisplayMember = "CName"; 
            txtcompobox.ValueMember = "CId"; 
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
                
                int selectedStId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                
                var confirmResult = MessageBox.Show("Are you sure to delete this student?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                   
                    StudentController controller = new StudentController();
                    controller.DeleteStudent(selectedStId);

                    
                    LoadStudents(); 
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



