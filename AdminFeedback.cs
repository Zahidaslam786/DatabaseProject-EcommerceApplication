using Oracle.ManagedDataAccess.Client; 
using System;
using System.Data;
using System.Windows.Forms;

namespace DB_LAB_PROJECT
{
    public partial class AdminFeedback : Form
    {
        // Database connection string
        private string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

        public AdminFeedback()
        {
            InitializeComponent();
            LoadFeedbackData();
        }

        // Load feedback data into the ListView
        private void LoadFeedbackData()
        {
            // SQL query to retrieve all feedback
            string query = @"SELECT F.USERID, F.DESCRIPTION FROM FEEDBACK F JOIN CUSTOMER C ON F.USERID = C.USERID";

            // Clear the ListView to prevent duplicates
            listView2.Items.Clear();

            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            // Loop through the data and populate the ListView
                            while (reader.Read())
                            {
                                ListViewItem item = new ListViewItem(reader["USERID"].ToString()); // USERID column
                                item.SubItems.Add(reader["DESCRIPTION"].ToString()); // DESCRIPTION column
                                listView2.Items.Add(item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while loading feedback: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Event handler for when the Back button is clicked
        private void button1_Click(object sender, EventArgs e)
        {
            // Navigate back to the previous screen
            Inventory managerDash = new Inventory();
            this.Hide();
            managerDash.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form11Admin L = new Form11Admin();
            this.Hide();
            L.Show();
        }
    }
}
