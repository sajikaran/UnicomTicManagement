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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace UnicomTicManagement.Views
{
    public partial class ExamForm : Form
    {
        public ExamForm()
        {
            InitializeComponent();
            LoadExam();
            textEName.Clear();
            LoadSubjects(); // Load subjects into the combo box
        }

        private void button1_Click(object sender, EventArgs e)

        {
            string examName = textEName.Text.Trim();
            string subjectName = comboBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(examName) || string.IsNullOrWhiteSpace(subjectName))
            {
                MessageBox.Show("Please enter both Exam Name and Subject Name.");
                return;
            }

            SubjectController subjectController = new SubjectController();
            ExamController examController = new ExamController();

            // Check if subject already exists
            Subject existingSubject = subjectController.GetAllSubjects()
                .FirstOrDefault(s => s.SuName.Equals(subjectName, StringComparison.OrdinalIgnoreCase));

            // If not, insert the subject
            if (existingSubject == null)
            {
                Subject newSubject = new Subject { SuName = subjectName };
                subjectController.AddSubject(newSubject);

                // Reload and get inserted subject
                existingSubject = subjectController.GetAllSubjects()
                    .FirstOrDefault(s => s.SuName.Equals(subjectName, StringComparison.OrdinalIgnoreCase));

                if (existingSubject == null)
                {
                    MessageBox.Show("Subject insertion failed.");
                    return;
                }
            }

            // Create and add the exam
            Exam newExam = new Exam
            {
                ExName = examName,
                SuId = existingSubject.SuId
            };

            examController.AddExam(newExam);

            MessageBox.Show("Exam added successfully!");

            // Optional: Refresh the exam grid
            LoadExam();

            // Optional: clear input
            textEName.Clear();
            
        }

        
        


        private void LoadExam()
        {

           
        
            ExamController examController = new ExamController();
            var exams = examController.GetAllExams();

            // Display only necessary fields
            var examDisplayList = exams.Select(e => new
            {
                SubjectID = e.SuId,
                ID = e.ExId,
                ExamName = e.ExName
                
            }).ToList();

            dataGridExam.DataSource = examDisplayList;
        }

        


       
            
        
            

        private void ExamForm_Load(object sender, EventArgs e)
        {

        }

        private void LoadSubjects()
        {
            SubjectController controller = new SubjectController();
            var subjects = controller.GetAllSubjects();
            comboBox1.DataSource = subjects;
            comboBox1.DisplayMember = "SuName"; // Display name in the combo box
            comboBox1.ValueMember = "SuId"; // Use SuId as value
        }

        private void textEName_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridExam_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridExam.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an exam to delete.");
                return;
            }

            int selectedExamId = Convert.ToInt32(dataGridExam.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Are you sure you want to delete this exam?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ExamController examController = new ExamController();
                examController.DeleteExam(selectedExamId);

                MessageBox.Show("Exam deleted successfully!");

                LoadExam();
                textEName.Clear();
                comboBox1.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridExam.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an exam to update.");
                return;
            }

            // Get new exam name
            string updatedExamName = textEName.Text.Trim();
            string selectedSubjectName = comboBox1.Text.Trim(); // Assuming comboBox1 holds subject names

            if (string.IsNullOrWhiteSpace(updatedExamName) || string.IsNullOrWhiteSpace(selectedSubjectName))
            {
                MessageBox.Show("Please enter both Exam Name and Subject.");
                return;
            }

            // Get selected exam ID from grid
            int selectedExamId = Convert.ToInt32(dataGridExam.SelectedRows[0].Cells["ID"].Value); // Ensure column name is "ID"

            // Initialize controllers
            SubjectController subjectController = new SubjectController();
            ExamController examController = new ExamController();

            // Get or create the subject
            Subject existingSubject = subjectController.GetAllSubjects()
                .FirstOrDefault(s => s.SuName.Equals(selectedSubjectName, StringComparison.OrdinalIgnoreCase));

            if (existingSubject == null)
            {
                // Create new subject
                Subject newSubject = new Subject { SuName = selectedSubjectName };

                subjectController.AddSubject(newSubject);

                // Try to retrieve it again
                existingSubject = subjectController.GetAllSubjects()
                    .FirstOrDefault(s => s.SuName.Equals(selectedSubjectName, StringComparison.OrdinalIgnoreCase));

                if (existingSubject == null)
                {
                    MessageBox.Show("Failed to insert or retrieve subject.");
                    return;
                }
            }

            // Create exam object for update
            Exam updatedExam = new Exam
            {
                ExId = selectedExamId,
                ExName = updatedExamName,
                SuId = existingSubject.SuId // Assuming exam is linked to subject by SuId
            };

            // Call the update method
            examController.UpdateExam(updatedExam);

            MessageBox.Show("Exam updated successfully!");

            // Refresh exam list and clear fields
            LoadExam();
            textEName.Clear();
            comboBox1.Text = "";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
