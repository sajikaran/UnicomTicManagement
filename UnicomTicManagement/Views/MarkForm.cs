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
    public partial class MarkForm : Form
    {
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

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textMarks.Text))
            {
                MessageBox.Show("Please enter marks.");
                return;
            }

            int marks;
            if (!int.TryParse(textMarks.Text, out marks))
            {
                MessageBox.Show("Invalid marks value.");
                return;
            }

            Mark newMark = new Mark
            {
                Marks = marks,
                StId = Convert.ToInt32(comboBox2.SelectedValue),
                SuId = Convert.ToInt32(comboBox1.SelectedValue),
                ExId = Convert.ToInt32(comboBox3.SelectedValue)
            };

            MarkController markController = new MarkController();
            markController.AddMarks(newMark);

            MessageBox.Show("Mark added successfully!");

            // Refresh grid and clear inputs
            LoadMarks();
            textMarks.Text="";
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
                m.MId,
                m.Marks,
                Student = m.StId, // You could join to get name if needed
                Subject = m.SuId,
                Exam = m.ExId
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
    }


}
