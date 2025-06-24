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
    public partial class StaffForm : Form
    {
        public StaffForm()
        {
            InitializeComponent();
            LoadStaff();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string staffName = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(staffName))
            {
                MessageBox.Show("Please enter staff name.");
                return;

            }

            Staff staff = new Staff
            {
                StaffName = staffName,

            };

            StaffController staffcontroller = new StaffController();
            int newStaffId = staffcontroller.AddStaffAndReturnId(staff);

            if (newStaffId <= 0)
            {
                MessageBox.Show("Failed to add staff.");
                return;
            }


            Usercontroller userController = new Usercontroller();
            if (userController.CreateLecturerUser(staffName, newStaffId, out string username, out string password))
            {

                MessageBox.Show($"Lecturer added!\nUsername: {username}\nPassword: {password}");
            }
            else
            {
                MessageBox.Show("   Staff added, but user account creation failed.");
            }


            textBox1.Text = "";
            LoadStaff();


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }


        private void LoadStaff()
        {


            StaffController  staffcontroller = new StaffController();
            var staffList = staffcontroller.GetAllStaff();

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = staffList;

            dataGridView1.Columns["StaffId"].HeaderText = "staff id";
            dataGridView1.Columns["StaffName"].HeaderText = "staff name";


        }

    }
}

