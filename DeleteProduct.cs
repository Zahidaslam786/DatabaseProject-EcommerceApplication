using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Windows.Forms;

namespace DB_LAB_PROJECT
{
    public partial class DeleteProduct : Form
    {
        public DeleteProduct()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadProducts()
        {
            try
            {
                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123"; // Update credentials
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

        private void button1_Click(object sender, EventArgs e) // Delete Button
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Please enter a Product ID.");
                    return;
                }

                int productId = Convert.ToInt32(textBox1.Text); // Convert entered text to Product ID

                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
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

                    // Delete the product
                    string deleteQuery = "DELETE FROM PRODUCT WHERE PRODUCTID = :ProductId";
                    OracleCommand deleteCmd = new OracleCommand(deleteQuery, con);
                    deleteCmd.Parameters.Add(":ProductId", productId);

                    int rowsAffected = deleteCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product deleted successfully!");
                        LoadProducts(); // Refresh the ListView
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete product.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting product: " + ex.Message);
            }
        }

    }
}
