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

      
        private void MainForm_Load(object sender, EventArgs e)
        {


        }

  

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {

            string username = textBox3.Text.Trim();  // Get username from textbox3
            string password = textBox2.Text.Trim();  // Get password from textbox2

            // Step 2: Check if fields are empty
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both username and password.");  // Show error message
                return;  // Stop here if fields are empty
            }

            // Step 3: Open connection to your SQLite database
            using (var connection = DataConnect.GetConnection())  // Call your custom method to get DB connection
            {
                connection.Open();  // Open the database connection

                // Step 4: Write SQL query to find matching user
                string query = @"
            SELECT Role, StId
            FROM Users
            WHERE Username = @Username AND Password = @Password";  // Match user with correct password

                // Step 5: Create the SQLite command with the SQL and connection
                using (var cmd = new SQLiteCommand(query, connection))
                {
                    // Step 6: Add parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    // Step 7: Run the query and read the result
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())  // ✅ User found
                        {
                            string role = reader.GetString(0);  // Get the Role (Admin, Student, etc.)
                            int? stId = reader.IsDBNull(1) ? (int?)null : reader.GetInt32(1);  // Get Student ID if it's not null

                            // Step 8: Show success message
                            MessageBox.Show($"Login successful as {role}");

                            // Step 9: Hide the login form
                            this.Hide();

                            // Step 10: Open the Main Dashboard and pass role + student ID
                            MainDashbordForm dashboard = new MainDashbordForm(role, stId);
                            dashboard.Show();
                        }
                        else
                        {
                            // ❌ No matching user found
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }


        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged_2(object sender, EventArgs e)
        {

        }
    }
}
