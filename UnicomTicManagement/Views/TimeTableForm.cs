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
                LoadTimeTable(); // refresh the UI
                textTimeSlot.Clear(); // clear the input field
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding time slot: " + ex.Message);
            }
        }


        private void LoadTimeTable()

        {
            TimetableController time=new TimetableController();
            var timeSlots = time.GetAllTimeSlots();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = timeSlots;

            dataGridView1.Columns["TId"].HeaderText = "Timetable ID";
            dataGridView1.Columns["TimeSlot"].HeaderText = "Timetable TimeSlot";


        }
    }
}
