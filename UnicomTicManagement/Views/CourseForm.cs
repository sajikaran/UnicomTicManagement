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
using UnicomTicManagement.Controllers;
using UnicomTicManagement.Data;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Views
{
    public partial class CourseForm : Form
    {
        public CourseForm()
        {
            InitializeComponent();
            LoadCourses();

        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        
           
        {
            string courseName = textCName.Text.Trim();

            if (string.IsNullOrEmpty(courseName))
            {
                MessageBox.Show("Course name cannot be empty.");
                return;
            }

            try
            {
                CourseController controller = new CourseController();
                controller.AddCourse(new Course { CName = courseName });
                MessageBox.Show("Course added successfully.");
                LoadCourses(); // Refresh grid
                textCName.Clear();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding course: " + ex.Message);
            }
        }

        
        


        private void LoadSubjects()
        {
           
         

        }


        private void LoadCourses()
        {
          
        
            CourseController controller = new CourseController();
            var courseList = controller.GetAllCourses();

            dgvCourses.DataSource = null;
            dgvCourses.DataSource = courseList;

            dgvCourses.Columns["CId"].HeaderText = "Course ID";
            dgvCourses.Columns["CName"].HeaderText = "Course Name";
        

        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CourseForm_Load(object sender, EventArgs e)
        {

        }

        private void dgvCourses_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
          
        
            if (dgvCourses.SelectedRows.Count > 0)
            {
                int selectedCourseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CId"].Value);

                var confirm = MessageBox.Show("Are you sure you want to delete this course?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    CourseController controller = new CourseController();
                    controller.DeleteCourse(selectedCourseId);
                    LoadCourses(); // Refresh grid
                }
            }
            else
            {
                MessageBox.Show("Please select a course to delete.");
            }
        }





        

        private void textCName_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)

        {
            if (dgvCourses.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a course to update.");
                return;
            }

            string updatedName = textCName.Text.Trim();

            if (string.IsNullOrEmpty(updatedName))
            {
                MessageBox.Show("Course name cannot be empty.");
                return;
            }

            // ✅ Get selected Course ID from DataGridView
            int selectedCourseId = Convert.ToInt32(dgvCourses.SelectedRows[0].Cells["CId"].Value);

            CourseController courseController = new CourseController();
            Course course = new Course
            {
                CId = selectedCourseId,     
                CName = updatedName
            };

            try
            {
                courseController.UpdateCourse(course);
                MessageBox.Show("Course updated successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating course: " + ex.Message);
            }

            
            LoadCourses();
            textCName.Clear();
             

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            

        }

    }

}


