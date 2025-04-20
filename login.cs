using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
namespace DB_LAB_PROJECT
{
    public static class GlobalVariables
    {
        // Static variable to hold UserID globally
        public static int LoggedInUserID { get; set; }
    }


    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
            LoadRoles();
        }
        private void LoadRoles()
        {
            // Preload roles into the ComboBox
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Manager");
            comboBox1.Items.Add("Customer");
            comboBox1.Items.Add("Cashier");
            comboBox1.Items.Add("Inventory_Staff");
            comboBox1.Items.Add("Delivery_Personnel");
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Redirect to the Sign Up Form
            SIGNUP signUpForm = new SIGNUP();
            this.Hide(); // Hide the current form
            signUpForm.Show(); // Show the Sign Up Form
        }

        private void login_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            // Validate that username and password are entered
            string username = textBox1.Text;
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter both Username and Password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that a role is selected
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get selected role
            string userRole = comboBox1.SelectedItem?.ToString();

            // Define table name based on user role
            string tableName = null;

            if (userRole == "Manager")
                tableName = "Manager";
            else if (userRole == "Customer")
                tableName = "Customer";
            else if (userRole == "Cashier")
                 tableName = "Cashier";
            else if (userRole == "Inventory_Staff")
                 tableName = "Inventory_Staff";
             else if (userRole == "Delivery_Personnel")
                 tableName = "delivery_personnel";

            if (authUser(username, password, tableName))
            {
                // Redirect based on user role
                if (userRole == "Manager")
                {
                    Form11Admin adminDashboard = new Form11Admin();
                    this.Hide();
                    adminDashboard.Show();
                }
                else if (userRole == "Customer")
                {
                    ProductBrowsing productBrowsing = new ProductBrowsing();
                    this.Hide();
                    productBrowsing.Show();
                }
                else if (userRole == "Inventory_Staff")
                {
                    Inventory productBrowsing = new Inventory();
                    this.Hide();
                    productBrowsing.Show();
                }
                else if (userRole == "Delivery_Personnel")
                {
                    OrderTracking10 productBrowsing = new OrderTracking10();
                    this.Hide();
                    productBrowsing.Show();
                }
                else if (userRole == "Cashier")
                {
                    Form8 productBrowsing = new Form8();
                    this.Hide();
                    productBrowsing.Show();
                }

            }
            else
            {
                MessageBox.Show("Invalid Username or Password.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


           
        }

        private bool authUser(string username, string password, string tableName)
        {
            try
            {
                // Construct the query to fetch both userID and password
                string query = $"SELECT userID, password FROM {tableName} WHERE UPPER(Username) = UPPER(:username)";
                string conStr = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

                using (OracleConnection conn = new OracleConnection(conStr))
                {
                    conn.Open();

                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        // Use parameters to prevent SQL injection
                        cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

                        using (OracleDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if a record was found
                            if (reader.Read())
                            {
                                string dbPassword = reader.GetString(1); // Fetch the password from DB

                                // Compare the stored password with the input password
                                if (dbPassword == password)
                                {
                                    // If login is successful, fetch the userID
                                    int userID = reader.GetInt32(0); // Fetch userID from the DB

                                    // Store the userID in the global variable
                                    GlobalVariables.LoggedInUserID = userID;
                                    MessageBox.Show($"Logged-in User ID: {userID}");

                                    return true; // Authentication successful
                                }
                                else
                                {
                                    MessageBox.Show("Invalid Password", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                            else
                            {
                                MessageBox.Show("Username not found", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false; // Authentication failed
        }


        //private bool authUser(string username, string password, string tableName)
        //{

        //    try
        //    {
        //        // Construct the query with case-insensitive username comparison
        //        string query = $"SELECT password FROM {tableName} WHERE UPPER(Username) = UPPER(:username)";
        //        string conStr = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

        //        // Use OracleConnection and OracleCommand for database interaction
        //        using (OracleConnection conn = new OracleConnection(conStr))
        //        {
        //            conn.Open();

        //            using (OracleCommand cmd = new OracleCommand(query, conn))
        //            {
        //                // Use parameters to prevent SQL injection
        //                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = username;

        //                // Execute the query
        //                using (OracleDataReader reader = cmd.ExecuteReader())
        //                {
        //                    if (reader.Read())
        //                    {
        //                        string dbPassword = reader.GetString(0);
        //                        return dbPassword == password; // Compare the stored password with the input password
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return false; // Return false if authentication fails
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            Form1 welcomeForm = new Form1();
            this.Hide(); // Hide the current form
            welcomeForm.Show(); // Show the Welcome Form
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
