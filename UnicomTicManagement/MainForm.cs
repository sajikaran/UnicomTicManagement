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
using UnicomTicManagement.Views;

namespace UnicomTicManagement
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {


        }

        private void label2_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();


            studentForm.Show();

            this.Hide();


        }

        private void label1_Click(object sender, EventArgs e)
        {
            CourseForm courseForm = new CourseForm();
            courseForm.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            TimeTableForm time = new TimeTableForm();
            time.Show();
            this.Hide();


        }

        private void label5_Click(object sender, EventArgs e)
        {
            SubjectForm subject = new SubjectForm(0); // Assuming 0 is a placeholder for CId, adjust as necessary
            subject.Show();
            this.Hide();
        
    
        }

        private void label3_Click(object sender, EventArgs e)
        {
            ExamForm examForm = new ExamForm();
            examForm.Show();
            this.Hide();
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SubjectForm subject = new SubjectForm(0); // Assuming 0 is a placeholder for CId, adjust as necessary
            subject.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm();


            studentForm.Show();

            this.Hide();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            RoomForm1cs room=new RoomForm1cs(); 
            room.Show();
            this.Hide();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CourseForm courseForm = new CourseForm();
            courseForm.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExamForm examForm = new ExamForm();
            examForm.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            TimeTableForm time = new TimeTableForm();
            time.Show();
            this.Hide();


        }
    }
}
