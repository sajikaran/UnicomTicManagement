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
        private string _role;
        private int? _studentId;

        public RoomForm1cs()
        {
;
           


            InitializeComponent();
            LoadRooms();
            LoadTimeSlots();
            LoadSubject();

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
                    comboBox1.Enabled = false; // You must define this method
                    break;
            }

        }

        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {




        }

        private void button1_Click(object sender, EventArgs e)
        {
            string roomName = textRoom.Text.Trim();

            if (string.IsNullOrWhiteSpace(roomName) ||
                comboBox2.SelectedValue == null ||
                comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Please fill all fields.");
                return;
            }

            Room newRoom = new Room
            {
                RName = roomName,
                SuId = Convert.ToInt32(comboBox2.SelectedValue),
                TId = Convert.ToInt32(comboBox1.SelectedValue)
            };

            RoomController controller = new RoomController();
            controller.AddRoom(newRoom);

            MessageBox.Show("Room added successfully!");

            LoadRooms();
            textRoom.Clear();
            comboBox2.Text = "";
            comboBox1.Text = "";
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
                SuID = r.SuId,
                TID = r.TId,
                RoomID = r.RId,
                RoomName = r.RName
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a room to update.");
                return;
            }

            int roomId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["RoomID"].Value);
            string roomName = textRoom.Text.Trim();

            if (string.IsNullOrWhiteSpace(roomName))
            {
                MessageBox.Show("Please enter a room name.");
                return;
            }

            Room updatedRoom = new Room
            {
                RId = roomId,
                RName = roomName,
                SuId = Convert.ToInt32(comboBox2.SelectedValue),
                TId = Convert.ToInt32(comboBox1.SelectedValue)
            };

            RoomController controller = new RoomController();
            controller.UpdateRoom(updatedRoom);

            MessageBox.Show("Room updated successfully!");
            LoadRooms();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var row = dataGridView1.SelectedRows[0];

                textRoom.Text = row.Cells["RoomName"].Value.ToString();
                comboBox2.SelectedValue = Convert.ToInt32(row.Cells["SubjectID"].Value);
                comboBox1.SelectedValue = Convert.ToInt32(row.Cells["TimeTableID"].Value);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textRoom_TextChanged(object sender, EventArgs e)
        {

        }

        private void RoomForm1cs_Load(object sender, EventArgs e)
        {

        }
    }
}
