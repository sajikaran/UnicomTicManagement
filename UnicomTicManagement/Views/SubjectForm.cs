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

namespace UnicomTicManagement.Views
{
    public partial class SubjectForm : Form
        
    {
        
        
        public SubjectForm(int CId)
        {
            InitializeComponent();
            LoadSubjects(); 
            Loadcourses();
            textSubName.Clear();
            comboBox1.Text = "";

        }

        private void SubjectForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadSubjects()
       
        {
            SubjectController subjectController = new SubjectController();
            var subjects = subjectController.GetAllSubjects();

            // Display only necessary fields
            var subjectDisplayList = subjects.Select(s => new
            {
                ID = s.SuId,
                SubjectName = s.SuName,
                CourseID = s.CId // You can fetch Course name with a JOIN if needed
            }).ToList();

            dataGridView1.DataSource = subjectDisplayList;
        }

        

        private void Loadcourses()
        {
            CourseController courseController = new CourseController();
            var courses = courseController.GetAllCourses();
            // Bind the courses to the combo box
            comboBox1.DataSource = courses;
            comboBox1.DisplayMember = "CName"; // Display name in the combo box
            comboBox1.ValueMember = "CId"; // Use CId as value
        }

        private void ADD_Click(object sender, EventArgs e)

        {
            string subjectName = textSubName.Text.Trim();
            string courseName = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(subjectName) || string.IsNullOrWhiteSpace(courseName))
            {
                MessageBox.Show("Please enter both Subject Name and Course Name.");
                return;
            }

            CourseController courseController = new CourseController();
            SubjectController subjectController = new SubjectController();

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

            // Create and add the subject
            Subject newSubject = new Subject
            {
                SuName = subjectName,
                CId = existingCourse.CId
            };

            subjectController.AddSubject(newSubject);

            MessageBox.Show("Subject added successfully!");

            // Optional: Refresh the subject grid
            LoadSubjects();

            // Optional: clear input
            textSubName.Clear();
            comboBox1.Text = "";
        }


           

        

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
private void textSubName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int subjectId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SuId"].Value);
                string suName = textSubName.Text;
                int courseId = int.Parse(textSubName.Text); // ensure this input is valid

                Subject updatedSubject = new Subject
                {
                    SuId = subjectId,
                    SuName = suName,
                    CId = courseId
                };
                SubjectController controller= new SubjectController();
                controller.UpdateSubject(updatedSubject);
                LoadSubjects();

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int subjectId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["SuId"].Value);
                var confirmResult = MessageBox.Show("Are you sure to delete this subject?",
                                                     "Confirm Delete",
                                                     MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    SubjectController controller= new SubjectController();
                    controller.DeleteSubject(subjectId);
                    LoadSubjects();
                }
            }
        }
    }


}


