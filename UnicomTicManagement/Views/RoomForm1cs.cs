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
    public partial class RoomForm1cs : Form
    {
        public RoomForm1cs()
        {
            InitializeComponent();
            LoadRooms();
            LoadTimeSlots();
            LoadSubject();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string RoomName = textRoom.Text.Trim();
            string TimeSlot = comboBox1.Text.Trim();
            string SubjectName = comboBox2.Text.Trim();

            // ✅ Validate input
            if (string.IsNullOrWhiteSpace(RoomName) ||
                string.IsNullOrWhiteSpace(TimeSlot) ||
                string.IsNullOrWhiteSpace(SubjectName))
            {
                MessageBox.Show("Please enter Room Name, Time Slot, and Subject.");
                return;
            }

            // ✅ Initialize controllers
            TimetableController timeController = new TimetableController();
            SubjectController subjectController = new SubjectController();
            RoomController roomController = new RoomController();

            // ✅ Get matching TimeSlot
            TimeTable existingCourse = timeController.GetAllTimeSlots()
                .FirstOrDefault(t => t.TimeSlot.Equals(TimeSlot, StringComparison.OrdinalIgnoreCase));

            // ✅ Get matching Subject
            Subject existingSubject = subjectController.GetAllSubjects()
                .FirstOrDefault(s => s.SuName.Equals(SubjectName, StringComparison.OrdinalIgnoreCase));

            if (existingCourse == null || existingSubject == null)
            {
                MessageBox.Show("Selected Time Slot or Subject is invalid.");
                return;
            }

            // ✅ Create Room object
            Room newRoom = new Room
            {
                RName = RoomName,
                TId = existingCourse.TId,
                SuId = existingSubject.SuId
            };

            try
            {
                // ✅ Add to database
                roomController.AddRoom(newRoom);
                MessageBox.Show("Room added successfully!");

                // ✅ Refresh form (if applicable)
                LoadTimeSlots();
                LoadRooms();
                LoadSubject();

                // ✅ Clear inputs
                textRoom.Text = "";
                comboBox1.SelectedIndex = -1;
                comboBox2.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding room: " + ex.Message);
            }
        }
        private void LoadTimeSlots()
        {
            TimetableController timeController = new TimetableController();
            var timeSlots = timeController.GetAllTimeSlots();
            comboBox1.DataSource = timeSlots;
            comboBox1.DisplayMember = "TimeSlot"; // Display name in the combo box
            comboBox1.ValueMember = "TId"; // Use CId as value

            // You can bind this to another DataGridView or use it as needed
        }


        private void LoadRooms()

        {
            RoomController roomController = new RoomController();
            TimetableController timeController = new TimetableController();

            var rooms = roomController.GetAllRooms();
            var timetables = timeController.GetAllTimeSlots(); // Optional, not used here yet

            // Display basic Room info
            var roomDisplayList = rooms.Select(r => new
            {
                RoomID = r.RId,
                RoomName = r.RName,

                // Optional: add timetable info if needed
            }).ToList();

            dataGridView1.DataSource = roomDisplayList;
        }


        private void LoadSubject() 
        {
            SubjectController subjectController = new SubjectController();
            var subjects = subjectController.GetAllSubjects();

            comboBox2.DataSource = subjects;
            comboBox2.DisplayMember = "SuName"; // Display name in the combo box
            comboBox2.ValueMember = "SuId";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
