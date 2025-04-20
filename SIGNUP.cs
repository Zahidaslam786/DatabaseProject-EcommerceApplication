using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DB_LAB_PROJECT
{
    public partial class SIGNUP : Form
    {
        public SIGNUP()
        {
            InitializeComponent();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            // Redirect to Login Form
            login loginForm = new login();
            this.Hide(); // Hide the current form
            loginForm.Show(); // Show the Login Form
        }

        /*  private void button2_Click(object sender, EventArgs e)
          {
              // Ensure all required data is entered
              string username = textBox1.Text;
              string email = textBox2.Text;
              string password = textBox3.Text;
              string confirmPassword = textBox4.Text;

              string role = comboBox1.SelectedItem?.ToString();

              // Check if any field is empty
              if (string.IsNullOrWhiteSpace(username) ||
                  string.IsNullOrWhiteSpace(email) ||
                  string.IsNullOrWhiteSpace(password) ||
                  string.IsNullOrWhiteSpace(confirmPassword) ||
                  string.IsNullOrWhiteSpace(role))
              {
                  MessageBox.Show("Please fill in all the fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }

              // Check if passwords match
              if (password != confirmPassword)
              {
                  MessageBox.Show("Passwords do not match. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }

              // Registration successful
              MessageBox.Show("Registration successful! Redirecting to Login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



              // Redirect to Welcome Form
              Form1 welcomeForm = new Form1();
              this.Hide(); // Hide the current form
              welcomeForm.Show(); // Show the Welcome Form
          }
  */

        /*   private void button2_Click(object sender, EventArgs e)
           {
               // Collect user inputs

               string username = textBox1.Text;
               string email = textBox2.Text;
               string password = textBox3.Text;
               string confirmPassword = textBox4.Text;
               string role = comboBox1.SelectedItem?.ToString();

               // Check for empty fields
               if (string.IsNullOrWhiteSpace(username) ||
                   string.IsNullOrWhiteSpace(email) ||
                   string.IsNullOrWhiteSpace(password) ||
                   string.IsNullOrWhiteSpace(confirmPassword) ||
                   string.IsNullOrWhiteSpace(role))
               {
                   MessageBox.Show("Please fill in all the fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return;
               }

               // Check if passwords match
               if (password != confirmPassword)
               {
                   MessageBox.Show("Passwords do not match. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   return;
               }
               try
               {
                   // Connection string to Oracle DB
                   string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

                   using (OracleConnection connection = new OracleConnection(connectionString))
                   {
                       connection.Open();

                       // Check if the username already exists in the specified table
                       string checkQuery = "SELECT COUNT(*) FROM customer WHERE Username = :Username";
                       using (OracleCommand checkCmd = new OracleCommand(checkQuery, connection))
                       {
                           checkCmd.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;

                           int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                           if (userExists > 0)
                           {
                               MessageBox.Show("Username already exists. Please choose a different one.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                               return;
                           }
                       }

                       // Insert new user into the Users table
                       string insertQuery = "INSERT INTO customer (UserID, Username, Password) VALUES (: :Username, :Password)";
                       using (OracleCommand cmd = new OracleCommand(insertQuery, connection))
                       {
                           cmd.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;
                           cmd.Parameters.Add("Password", OracleDbType.Varchar2).Value = password; // Store plain password

                           // Execute the insert command
                           cmd.ExecuteNonQuery();
                       }

                       // Show success message
                       MessageBox.Show("Registration successful! Redirecting to Login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       // Redirect to Login Form
                       login loginForm = new login();
                       this.Hide(); // Hide the current form
                       loginForm.Show(); // Show the Login Form
                   }
               }
               catch (Exception ex)
               {
                   // Handle any exceptions
                   MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }
           }
   */

        private void button2_Click(object sender, EventArgs e)
        {
            otpverify loginForm1 = new otpverify();
            this.Hide(); // Hide the current form
            loginForm1.Show(); // Show the Login Form

            // Collect user inputs
            string username = textBox1.Text;
            string email = textBox2.Text;
            string password = textBox3.Text;
            string confirmPassword = textBox4.Text;
            string role = comboBox1.SelectedItem?.ToString();

            // Check for empty fields
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword) ||
                string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("Please fill in all the fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if passwords match
            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Connection string to Oracle DB
                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Generate a unique UserID (incremental)
                    int userId = GetNextUserId(connection,role);

                    // Check if the username already exists in the specified table
                    string checkQuery = "SELECT COUNT(*) FROM "+role+" WHERE Username = :Username";
                    using (OracleCommand checkCmd = new OracleCommand(checkQuery, connection))
                    {
                        checkCmd.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;

                        int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());
                        if (userExists > 0)
                        {
                            MessageBox.Show("Username already exists. Please choose a different one.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Insert new user into the Users table
                    string insertQuery = "INSERT INTO "+role+" (UserID, Username, Password) VALUES (:UserID, :Username, :Password)";
                    using (OracleCommand cmd = new OracleCommand(insertQuery, connection))
                    {
                        cmd.Parameters.Add("UserID", OracleDbType.Int32).Value = userId; // Set UserID as integer
                        cmd.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;
                        cmd.Parameters.Add("Password", OracleDbType.Varchar2).Value = password; // Store plain password

                        // Execute the insert command
                        cmd.ExecuteNonQuery();
                    }
                    // Show success message
                    //MessageBox.Show("Registration successful! Redirecting to Login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Redirect to Login Form
                   
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to get the next UserID
        private int GetNextUserId(OracleConnection connection,string role)
        {
            // Get the maximum UserID from the customer table
            string maxIdQuery = "SELECT NVL(MAX(UserID), 0) + 1 FROM "+role;
            using (OracleCommand maxIdCmd = new OracleCommand(maxIdQuery, connection))
            {
                return Convert.ToInt32(maxIdCmd.ExecuteScalar());
            }
        }
        //    try
        //    {
        //        // Connection string to Oracle DB
        //        string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

        //        using (OracleConnection connection = new OracleConnection(connectionString))
        //        {
        //            connection.Open();

        //            // Determine the table based on the role
        //            string tableName = GetTableNameBasedOnRole(role);

        //            if (tableName == null)
        //            {
        //                MessageBox.Show("Invalid role selected.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                return;
        //            }

        //            // Check if the username or email already exists in the specified table
        //            string checkQuery = $"SELECT COUNT(*) FROM {tableName} WHERE Username = :Username OR Email = :Email";
        //            using (OracleCommand checkCmd = new OracleCommand(checkQuery, connection))
        //            {
        //                checkCmd.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;
        //                checkCmd.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;

        //                int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());
        //                if (userExists > 0)
        //                {
        //                    MessageBox.Show("Username or email already exists. Please choose a different one.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //            }

        //            // Insert new user into the respective table
        //            string insertQuery = $"INSERT INTO {tableName} (Username, Email, Password, Role) VALUES (:Username, :Email, :Password, :Role)";
        //            using (OracleCommand cmd = new OracleCommand(insertQuery, connection))
        //            {
        //                cmd.Parameters.Add("Username", OracleDbType.Varchar2).Value = username;
        //                cmd.Parameters.Add("Email", OracleDbType.Varchar2).Value = email;
        //                cmd.Parameters.Add("Password", OracleDbType.Varchar2).Value = password; // Store plain password
        //                cmd.Parameters.Add("Role", OracleDbType.Varchar2).Value = role;

        //                // Execute the insert command
        //                cmd.ExecuteNonQuery();
        //            }

        //            // Show success message
        //            MessageBox.Show("Registration successful! Redirecting to Login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            // Redirect to Login Form
        //            login loginForm = new login();
        //            this.Hide(); // Hide the current form
        //            loginForm.Show(); // Show the Login Form
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    // Collect user inputs
        //    string username = textBox1.Text;
        //    string email = textBox2.Text;
        //    string password = textBox3.Text;
        //    string confirmPassword = textBox4.Text;
        //    string role = comboBox1.SelectedItem?.ToString();

        //    // Check for empty fields
        //    if (string.IsNullOrWhiteSpace(username) ||
        //        string.IsNullOrWhiteSpace(email) ||
        //        string.IsNullOrWhiteSpace(password) ||
        //        string.IsNullOrWhiteSpace(confirmPassword) ||
        //        string.IsNullOrWhiteSpace(role))
        //    {
        //        MessageBox.Show("Please fill in all the fields.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    // Validate email format


        //    // Check if passwords match
        //    if (password != confirmPassword)
        //    {
        //        MessageBox.Show("Passwords do not match. Please try again.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    try
        //    {
        //        // Create database connection
        //        string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";  // Make sure to replace with your actual connection string
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            // Open the connection
        //            connection.Open();

        //            // Check if the username or email already exists
        //            string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR Email = @Email";
        //            using (SqlCommand checkCmd = new SqlCommand(checkQuery, connection))
        //            {
        //                checkCmd.Parameters.AddWithValue("@Username", username);
        //                checkCmd.Parameters.AddWithValue("@Email", email);

        //                int userExists = (int)checkCmd.ExecuteScalar();
        //                if (userExists > 0)
        //                {
        //                    MessageBox.Show("Username or email already exists. Please choose a different one.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //                    return;
        //                }
        //            }

        //            // Insert new user into the Users table
        //            string insertQuery = "INSERT INTO Users (Username, Email, Password, Role) VALUES (@Username, @Email, @Password, @Role)";
        //            using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
        //            {
        //                // Add parameters to the query
        //                cmd.Parameters.AddWithValue("@Username", username);
        //                cmd.Parameters.AddWithValue("@Email", email);
        //                cmd.Parameters.AddWithValue("@Password", password); // Store plain password
        //                cmd.Parameters.AddWithValue("@Role", role);

        //                // Execute the command
        //                cmd.ExecuteNonQuery();
        //            }

        //            // Registration successful message
        //            MessageBox.Show("Registration successful! Redirecting to Login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        //            // Redirect to Login Form
        //            login loginForm = new login();
        //            this.Hide(); // Hide the current form
        //            loginForm.Show(); // Show the Login Form
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions
        //        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }

        //    // Registration successful
        //    MessageBox.Show("Registration successful! Redirecting to Login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);



        //    // Redirect to Login Form
        //    login loginForm = new login();
        //    this.Hide(); // Hide the current form
        //    loginForm.Show(); // Show the Login Form
        //}

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private string GetTableNameBasedOnRole(string role)
        {
            // Map role to respective table
            switch (role)
            {
                case "Admin":
                    return "Admin";
                case "Customer":
                    return "Customer";
                case "Cashier":
                    return "Cashier";
                case "Manager":
                    return "Manager";
                case "Inventory_Staff":
                    return "Inventory_Staff";
                case "Delivery_Personnel":
                    return "Delivery_Personnel";
                default:
                    return null;
            }
        }

        private void SIGNUP_Load(object sender, EventArgs e)
        {
            // Load roles into the ComboBox
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("Customer");
            comboBox1.Items.Add("Cashier");
            comboBox1.Items.Add("Manager");
            comboBox1.Items.Add("Inventory_Staff");
            comboBox1.Items.Add("Delivery_Personnel");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
