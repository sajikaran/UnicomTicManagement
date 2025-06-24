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
    public partial class LecturerForm : Form
    {
        public LecturerForm()
        {
            InitializeComponent();
            LoadLectures();
            LoadSubjects();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



        private void LoadLectures()
        {
            LectureController lectureController = new LectureController();
            SubjectController subjectController = new SubjectController();

            var lectures = lectureController.GetAllLecturers();
            var subjects = subjectController.GetAllSubjects();  

            var lectDisplayList = lectures.Select(l => new
            {
                SubjectID = l.SuId,
                ID = l.LId,
                StudentName = l.LName
            }).ToList();

            dataGridView1.DataSource = lectDisplayList;
        }


        private void LoadSubjects()

        {

            SubjectController subjectController = new SubjectController();
            var subjects = subjectController.GetAllSubjects();
            SubName.DataSource = subjects;
            SubName.DisplayMember = "SubName";
            SubName.ValueMember = "SuId";
        }

        private void ADD_Click(object sender, EventArgs e)
        {
            string lecturerName = textLName.Text.Trim();
            int subjectId = Convert.ToInt32(SubName.SelectedValue);

            if (string.IsNullOrWhiteSpace(lecturerName))
            {
                MessageBox.Show("Enter Lecturer Name");
                return;
            }

            Lecturer lecturer = new Lecturer
            {
                LName = lecturerName,
                SuId = subjectId
            };

            LectureController controller = new LectureController();
            int newLecturerId = controller.AddLecturerAndReturnId(lecturer);

            if (newLecturerId <= 0)
            {
                MessageBox.Show("Failed to add lecturer.");
                return;
            }

            Usercontroller userController = new Usercontroller();
            if (userController.CreateLecturerUser(lecturerName, newLecturerId, out string username, out string password))
            {
                MessageBox.Show($"Lecturer added!\nUsername: {username}\nPassword: {password}");
            }
            else
            {
                MessageBox.Show("Lecturer added, but user account creation failed.");
            }

            LoadLectures();
            textLName.Text = "";
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                
                int selectedLId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["LId"].Value);

              
                var confirmResult = MessageBox.Show("Are you sure to delete this lecture ?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (confirmResult == DialogResult.Yes)
                {
                    
                    StudentController controller = new StudentController();
                    controller.DeleteStudent(selectedLId);

                    
                    LoadLectures(); 
                }
            }
            else
            {
                MessageBox.Show("Please select a lecture to delete.");
            }
        }

        private void Update_Click(object sender, EventArgs e)
        {

        }
    }

}
