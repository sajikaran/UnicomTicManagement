using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace UnicomTicManagement.Views
{
    public partial class MarkForm : Form
    {
        private string _role;
        private int? _studentId;

        public MarkForm()
        {

          

            InitializeComponent();
            LoadStudents();
            LoadSubjects();
            LoadExams();
            LoadMarks();

        }

        private void MarkForm_Load(object sender, EventArgs e)
        {
            switch (_role)
            {
                case "Admin":
                    // Full access – no restrictions
                    break;

                case "Lecturer":
                case "Staff":
                    
                    break;

                case "Student":
                    // Load only own marks and disable editing
                    button1.Visible = false;
                    button2.Visible = false;
                    button3.Visible = false;
                    comboBox2.Enabled = false; // prevent changing student
                    textMarks.Enabled=false; // You must define this method
                    break;
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textMarks.Text) ||
                                     comboBox2.SelectedValue == null ||
                                     comboBox1.SelectedValue == null ||
                                     comboBox3.SelectedValue == null)
                                            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            int marks;
            if (!int.TryParse(textMarks.Text, out marks))
            {
                MessageBox.Show("Please enter a valid numeric mark.");
                return;
            }

            // Create new Mark object
            Mark newMark = new Mark
            {
                Marks = marks,
                StId = Convert.ToInt32(comboBox2.SelectedValue),
                SuId = Convert.ToInt32(comboBox1.SelectedValue),
                ExId = Convert.ToInt32(comboBox3.SelectedValue)
            };

            // Save to database
            MarkController markController = new MarkController();
            markController.AddMarks(newMark);

            MessageBox.Show("Mark added successfully!");

            
            LoadMarks();
            textMarks.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";


        }


        private void LoadMarks()
        {
            MarkController markController = new MarkController();
            var marks = markController.GetAllMarks();

            var displayList = marks.Select(m => new
            {
                Student = m.StId, // You could join to get name if needed
                Subject = m.SuId,
                Exam = m.ExId,
                m.MId,
                m.Marks
               
            }).ToList();

            dataGridView1.DataSource = displayList;
        }

        private void LoadStudents()
        {
            StudentController studentController = new StudentController();
            var students = studentController.GetAllStudents();
            comboBox2.DataSource = students;
            comboBox2.DisplayMember = "StName";
            comboBox2.ValueMember = "StId";


        }

        private void LoadSubjects()
        {
            SubjectController subjectController = new SubjectController();
            var subjects = subjectController.GetAllSubjects();
            comboBox1.DataSource = subjects;
            comboBox1.DisplayMember = "SuName";
            comboBox1.ValueMember = "SuId";
        }


        private void LoadExams()
        {
            ExamController examController = new ExamController();
            var exams = examController.GetAllExams();
            comboBox3.DataSource = exams;
            comboBox3.DisplayMember = "ExName";
            comboBox3.ValueMember = "ExId";

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a mark record to delete.");
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete this mark?", "Confirm Delete", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MId"].Value);

                MarkController markController = new MarkController();
                markController.DeleteMark(selectedId);

                MessageBox.Show("Mark deleted successfully!");

                LoadMarks();
                textMarks.Text = "";
                comboBox2.Text = "";
                comboBox1.Text = "";
                comboBox3.Text = "";



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a mark record to update.");
                return;
            }

            if (string.IsNullOrWhiteSpace(textMarks.Text))
            {
                MessageBox.Show("Please enter marks.");
                return;
            }

            if (comboBox1.SelectedValue == null || comboBox2.SelectedValue == null || comboBox3.SelectedValue == null)
            {
                MessageBox.Show("Please select student, subject, and exam.");
                return;
            }

            int marks;
            if (!int.TryParse(textMarks.Text, out marks))
            {
                MessageBox.Show("Invalid marks value.");
                return;
            }

            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["MId"].Value);

            Mark updatedMark = new Mark
            {
                MId = selectedId,
                Marks = marks,
                StId = Convert.ToInt32(comboBox2.SelectedValue),
                SuId = Convert.ToInt32(comboBox1.SelectedValue),
                ExId = Convert.ToInt32(comboBox3.SelectedValue)
            };

            MarkController markController = new MarkController();
            markController.UpdateMark(updatedMark);

            MessageBox.Show("Mark updated successfully!");

            LoadMarks();
            textMarks.Text = "";
            comboBox2.Text = "";
            comboBox1.Text = "";
            comboBox3.Text = "";



        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}



