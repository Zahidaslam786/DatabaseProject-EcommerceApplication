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
    public partial class UpdateProduct : Form
    {
        public UpdateProduct()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            try
            {
                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";// Update credentials
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT PRODUCTNAME, PRICE, QUANTITY, PRODUCTID FROM PRODUCT";
                    OracleCommand cmd = new OracleCommand(query, con);
                    OracleDataReader reader = cmd.ExecuteReader();

                    listView1.Items.Clear(); // Clear the ListView before populating
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["PRODUCTNAME"].ToString());
                        item.SubItems.Add(reader["PRICE"].ToString());
                        item.SubItems.Add(reader["QUANTITY"].ToString());
                        item.SubItems.Add(reader["PRODUCTID"].ToString());
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory managerDash = new Inventory(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) // Save Button
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                int productId = Convert.ToInt32(textBox1.Text); // Product ID
                string newName = textBox2.Text; // New Product Name
                int newQuantity = Convert.ToInt32(textBox3.Text); // New Quantity
                decimal newPrice = Convert.ToDecimal(textBox4.Text); // New Price

                // Oracle connection
                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123"; // Update with actual credentials
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    // Check if ProductID exists
                    string checkQuery = "SELECT COUNT(*) FROM PRODUCT WHERE PRODUCTID = :ProductId";
                    OracleCommand checkCmd = new OracleCommand(checkQuery, con);
                    checkCmd.Parameters.Add(":ProductId", productId);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count == 0)
                    {
                        MessageBox.Show("Product ID does not exist.");
                        return;
                    }

                    // Update the product
                    string updateQuery = "UPDATE PRODUCT SET PRODUCTNAME = :NewName, QUANTITY = :NewQuantity, PRICE = :NewPrice WHERE PRODUCTID = :ProductId";
                    OracleCommand updateCmd = new OracleCommand(updateQuery, con);
                    updateCmd.Parameters.Add(":NewName", newName);
                    updateCmd.Parameters.Add(":NewQuantity", newQuantity);
                    updateCmd.Parameters.Add(":NewPrice", newPrice);
                    updateCmd.Parameters.Add(":ProductId", productId);

                    int rowsAffected = updateCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product updated successfully!");
                        LoadProducts();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update product.");
                    }
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid data in the fields.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        

    }
}
