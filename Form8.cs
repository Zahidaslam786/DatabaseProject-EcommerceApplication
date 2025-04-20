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
    public partial class Form8 : Form
    {
        private string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
        private int currentUserId = GlobalVariables.LoggedInUserID;

        public Form8()
        {
            InitializeComponent();
            LoadCartDetails();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void LoadCartDetails()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT PRODUCTNAME, PRICE, QUANTITY 
                FROM CART 
                WHERE USERID = :UserId";

                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter(":UserId", currentUserId));

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            // Clear existing items
                            decimal totalCost = 0; // Initialize total cost

                            // Loop through the results and add them to the ListView
                            while (reader.Read())
                            {
                                string productName = reader["PRODUCTNAME"].ToString();
                                decimal price = reader.GetDecimal(reader.GetOrdinal("PRICE"));
                                int quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"));

                                // Calculate the total cost
                                totalCost += price * quantity;

                                // Create a ListViewItem
                                ListViewItem item = new ListViewItem(productName);
                                item.SubItems.Add(price.ToString()); // Format price as currency
                                item.SubItems.Add(quantity.ToString());

                                // Add the item to the ListView
                                listView1.Items.Add(item);
                            }

                            // Display the total cost
                            label4.Text = "Total: " + totalCost.ToString(); // Format as currency
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                // Handle Oracle-specific exceptions
                MessageBox.Show("Database error: " + ex.Message);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        //private void LoadCartDetails()
        //   {
        //       try
        //       {
        //           using (OracleConnection connection = new OracleConnection(connectionString))
        //           {
        //               connection.Open();

        //               string query = @"
        //           SELECT PRODUCTNAME, PRICE, QUANTITY 
        //           FROM CART 
        //           WHERE USERID = :UserId";

        //               using (OracleCommand command = new OracleCommand(query, connection))
        //               {
        //                   command.Parameters.Add(new OracleParameter(":UserId", currentUserId));

        //                   using (OracleDataReader reader = command.ExecuteReader())
        //                   {
        //                       // Clear existing items
        //                       listView2.Items.Clear();

        //                       // Loop through the results and add them to the ListView
        //                       while (reader.Read())
        //                       {
        //                           string productName = reader["PRODUCTNAME"].ToString();
        //                           decimal price = reader.GetDecimal(reader.GetOrdinal("PRICE"));
        //                           int quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"));

        //                           // Create a ListViewItem
        //                           ListViewItem item = new ListViewItem(productName);
        //                           item.SubItems.Add(quantity.ToString());
        //                           item.SubItems.Add(price.ToString()); // Format as currency

        //                           // Add the item to the ListView
        //                           listView2.Items.Add(item);
        //                       }
        //                   }
        //               }
        //           }
        //       }
        //       catch (OracleException ex)
        //       {
        //           // Handle Oracle-specific exceptions
        //           MessageBox.Show("Database error: " + ex.Message);
        //       }
        //       catch (Exception ex)
        //       {
        //           // Handle general exceptions
        //           MessageBox.Show("An error occurred: " + ex.Message);
        //       }
        //   }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Create an instance of OrderConfirmationForm
            OrderConfirmation9 orderConfirmationForm = new OrderConfirmation9();

            // Display the OrderConfirmationForm
            orderConfirmationForm.Show();

            // Optional: Hide the current form if needed
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
