using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using UnicomTicManagement.Controllers;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Views
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }
        
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
      
        {
            var student = new Student
            {
                StName = txtStName.Text,
                CId = Convert.ToInt32(comboCourse.SelectedValue)
            };

            StudentController.AddStudent(student);
            MessageBox.Show("Student added successfully!");
        }


        

        
        private void txtStName_TextChanged(object sender, EventArgs e)
        {


        }

        private void StudentForm_Load(object sender, EventArgs e)
        {


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        
        {
            var courses = CourseController.GetAllCourses();
            comboCourse.DataSource = courses;
            comboCourse.DisplayMember = "CName";
            comboCourse.ValueMember = "CId";
        }

    }

}


