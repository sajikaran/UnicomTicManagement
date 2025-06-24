using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Controllers;
using UnicomTicManagement.Models;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UnicomTicManagement.Views
{
    public partial class SubjectForm : Form

    {
        private string _role;
        private int? _studentId;

        public SubjectForm(string role)
        {
            InitializeComponent();
            LoadSubjects();
            Loadcourses();
            textSubName.Clear();
            comboBox1.Text = "";
            _role = role;
            ApplyRoleRestrictions();

        }



        private void ApplyRoleRestrictions()
        {
            switch (_role)
            {
                case "Admin":
                    // Full access
                    break;

                case "Lecturer":
                case "Staff":
                    button1.Visible = false;   // Delete
                    button2.Visible = false;   // Update
                    ADD.Visible = false;       // Add
                    label1.Visible = false;
                    label2.Visible = false;
                    textSubName.Visible = false;
                    comboBox1.Visible = false;
                    break;

                case "Student":
                    button1.Visible = false;   // Delete
                    button2.Visible = false;   // Update
                    ADD.Visible = false;       // Add
                    label1.Visible = false;
                    label2.Visible = false;
                    textSubName.Visible = false;
                    comboBox1.Visible = false;
                    break;
            }
        }



        private void SubjectForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadSubjects()

        {
            SubjectController subjectController = new SubjectController();
            var subjects = subjectController.GetAllSubjects();

            
            var subjectDisplayList = subjects.Select(s => new
            {
                CourseID = s.CId,
                ID = s.SuId,
                SubjectName = s.SuName,
                 
            }).ToList();

            dataGridView1.DataSource = subjectDisplayList;
        }



        private void Loadcourses()
        {
            CourseController courseController = new CourseController();
            var courses = courseController.GetAllCourses();
            comboBox1.DataSource = courses;
            comboBox1.DisplayMember = "CName"; 
            comboBox1.ValueMember = "CId"; 
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

            
            Course existingCourse = courseController.GetAllCourses()
                .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

            
            if (existingCourse == null)
            {
                Course newCourse = new Course { CName = courseName };
                courseController.AddCourse(newCourse);

                
                existingCourse = courseController.GetAllCourses()
                    .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (existingCourse == null)
                {
                    MessageBox.Show("Course insertion failed.");
                    return;
                }
            }

            
            Subject newSubject = new Subject
            {
                SuName = subjectName,
                CId = existingCourse.CId
            };

            subjectController.AddSubject(newSubject);

            MessageBox.Show("Subject added successfully!");

            
            LoadSubjects();

        
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
            
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a subject to update.");
                return;
            }

            string subjectName = textSubName.Text.Trim();
            string courseName = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(subjectName) || string.IsNullOrWhiteSpace(courseName))
            {
                MessageBox.Show("Please enter both Subject Name and Course.");
                return;
            }

   
            int subjectId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

            CourseController courseController = new CourseController();
            SubjectController subjectController = new SubjectController();

            Course existingCourse = courseController.GetAllCourses()
                .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

            if (existingCourse == null)
            {
                Course newCourse = new Course { CName = courseName };
                courseController.AddCourse(newCourse);

                existingCourse = courseController.GetAllCourses()
                    .FirstOrDefault(c => c.CName.Equals(courseName, StringComparison.OrdinalIgnoreCase));

                if (existingCourse == null)
                {
                    MessageBox.Show("Failed to insert or retrieve course.");
                    return;
                }
            }

            
            Subject updatedSubject = new Subject
            {
                SuId = subjectId,
                SuName = subjectName,
                CId = existingCourse.CId
            };

           
            subjectController.UpdateSubject(updatedSubject);

            MessageBox.Show("Subject updated successfully!");

            
            LoadSubjects();
            textSubName.Clear();
            comboBox1.Text = "";

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a subject to delete.");
                return;
            }

            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this subject?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                SubjectController subjectController = new SubjectController();
                subjectController.DeleteSubject(selectedId);

                MessageBox.Show("Subject deleted successfully!");

                LoadSubjects();
                textSubName.Clear();
                comboBox1.Text = "";
            }
        }
    }


}


