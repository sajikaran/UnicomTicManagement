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
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
            //if (dataGridView1.CurrentRow == null)
            //{
            //    MessageBox.Show("Select a staff to delete.");
            //    return;
            //}

            //int staffId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StaffId"].Value);

            //DialogResult confirm = MessageBox.Show("Are you sure to delete?", "Confirm", MessageBoxButtons.YesNo);
            //if (confirm == DialogResult.Yes)
            //{
                //    StaffController controller = new StaffController();
                //    controller.DeleteStaff(staffId);
                //    MessageBox.Show("Staff deleted.");
                //    LoadStaff();
                //}
            //}

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //    string staffName = textBox3.Text.Trim();
            //    if (string.IsNullOrWhiteSpace(staffName))
            //    {
            //        MessageBox.Show("Please enter Staff name.");
            //        return;
            //    }

            //    StaffController controller = new StaffController();
            //    Staffz


            //    if (newStaffId <= 0)
            //    {
            //        MessageBox.Show("Failed to add staff.");
            //        return;
            //    }

            //    Usercontroller userCtrl = new Usercontroller();
            //    if (userCtrl.CreateStaffUser(staffName, newStaffId, out string username, out string password))
            //    {
            //        MessageBox.Show($"Staff Added!\nUsername: {username}\nPassword: {password}", "Success");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Staff added, but user account creation failed.");
            //    }

            //    LoadStaff(); // Reload grid
            //    textBox3.Clear();
            //}

            //private void button3_Click(object sender, EventArgs e)
            //{
            //    if (dataGridView1.CurrentRow == null)
            //    {
            //        MessageBox.Show("Select a staff to update.");
            //        return;
            //    }

            //    int staffId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["StaffId"].Value);
            //    string staffName = textBox3.Text.Trim();

            //    if (string.IsNullOrWhiteSpace(staffName))
            //    {
            //        MessageBox.Show("Enter staff name.");
            //        return;
            //    }

            //    Staff updated = new Staff { StaffId = staffId, StaffName = staffName };
            //    StaffController controller = new StaffController();
            //    controller.UpdateStaff(updated);
            //    MessageBox.Show("Staff updated.");
            //    LoadStaff();
            //}
        }
    }
}