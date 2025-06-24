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
    public partial class TimeTableForm : Form
    {
        public TimeTableForm()
        {
            InitializeComponent();
            LoadTimeTable();
            textTimeSlot.Clear();

        }

        private void TimeTableForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string timeSlot = textTimeSlot.Text.Trim();

            if (string.IsNullOrEmpty(timeSlot))
            {
                MessageBox.Show("Time slot cannot be empty.");
                return;
            }

            try
            {
                TimetableController controller = new TimetableController();
                controller.AddTimeSlot(new TimeTable { TimeSlot = timeSlot });

                MessageBox.Show("Time slot added successfully.");
                LoadTimeTable();
                textTimeSlot.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding time slot: " + ex.Message);
            }
        }


        private void LoadTimeTable()

        {
            TimetableController time = new TimetableController();
            var timeSlots = time.GetAllTimeSlots();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = timeSlots;

            dataGridView1.Columns["TId"].HeaderText = "Timetable ID";
            dataGridView1.Columns["TimeSlot"].HeaderText = "Timetable TimeSlot";


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textTimeSlot_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a time slot to delete.");
                return;
            }

            var confirm = MessageBox.Show("Are you sure you want to delete the selected time slot?",
                                          "Confirm Delete", MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
                return;

            int selectedId = (int)dataGridView1.SelectedRows[0].Cells["TId"].Value;

            try
            {
                TimetableController controller = new TimetableController();
                controller.DeleteTimeSlot(selectedId);

                MessageBox.Show("Time slot deleted successfully.");
                LoadTimeTable(); // Refresh grid
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting time slot: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row to update.");
                return;
            }

            int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["TId"].Value);
            string updatedTimeSlot = textTimeSlot.Text.Trim();

            if (string.IsNullOrEmpty(updatedTimeSlot))
            {
                MessageBox.Show("Please enter a time slot.");
                return;
            }

            TimeTable updated = new TimeTable
            {
                TId = selectedId,
                TimeSlot = updatedTimeSlot
            };

            TimetableController controller = new TimetableController();
            controller.UpdateTimeSlot(updated);

            MessageBox.Show("Time slot updated successfully.");
            LoadTimeTable();
            textTimeSlot.Clear();
        }

    }
}





