using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DB_LAB_PROJECT
{
    public partial class feedback : Form
    {
        private string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
        private int currentUserId = GlobalVariables.LoggedInUserID;
        public feedback()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OrderTracking10 managerDash = new OrderTracking10(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get feedback from the textbox
            string feedbackText = textBox1.Text;

            if (string.IsNullOrWhiteSpace(feedbackText))
            {
                MessageBox.Show("Please enter some feedback before uploading.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // SQL to insert feedback into the table
            string query = "INSERT INTO FEEDBACK (USERID, DESCRIPTION) VALUES (:userId, :description)";

            // Use a connection to the database
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        // Add parameters to avoid SQL injection
                        command.Parameters.Add(new OracleParameter("userId", currentUserId));
                        command.Parameters.Add(new OracleParameter("description", feedbackText));

                        // Execute the query
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Feedback uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to upload feedback. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
