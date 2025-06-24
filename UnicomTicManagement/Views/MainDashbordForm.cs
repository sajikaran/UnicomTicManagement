using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnicomTicManagement.Models;

namespace UnicomTicManagement.Views
{
    public partial class MainDashbordForm : Form
    {

        private Form currentForm = null;

        private string _role;
        private int? _studentId;
        public MainDashbordForm(string role, int? studentId = null)

        {
            InitializeComponent();
            _role = role;
            _studentId = studentId;
           




            switch (_role)
            {
                case "Admin":
                    // Full access, do nothing
                    break;

                case "Lecturer":

                    
                    button1.Visible = false;
                    button2.Visible = false;
                    
                    button4.Visible = false;
                    button6.Visible = false;
                    button8.Visible = false; 
                    button9.Visible = false;


                    break;

                case "Staff":



                    button1.Visible = false;
                    button2.Visible = false;
                    
                    button4.Visible = false;
                    button6.Visible = false;
                    button8.Visible = false;
                    button9.Visible = false;

                    break;

                case "Student":
                    button1.Visible = false;
                    button2.Visible = false;
                    
                    button4.Visible = false;
                    button5.Enabled = false;
                    button6.Visible = false;
                    button7.Enabled = false;
                    button8.Visible = false;
                    button9.Visible = false;
                    break;
            }


        }



        private void LoadFormIntoPanel(Form formToLoad)
        {

            if (currentForm != null)
                currentForm.Close();

            currentForm = formToLoad;

            formToLoad.TopLevel = false;
            formToLoad.FormBorderStyle = FormBorderStyle.None;
            formToLoad.Dock = DockStyle.Fill;

            panel2.Controls.Clear();              // ✅ this is your center panel
            panel2.Controls.Add(formToLoad);      // ✅ add the form to center panel
            formToLoad.Show();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new StudentForm());


        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new CourseForm());


            }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new SubjectForm(_role));

            
        

            }

        private void button4_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new TimeTableForm());


            
        
            }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new RoomForm1cs());

        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new ExamForm());

        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadFormIntoPanel(new MarkForm());

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {


        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {


        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }

}



