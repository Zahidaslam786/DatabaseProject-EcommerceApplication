using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace DB_LAB_PROJECT
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            LoadProducts();
        }
        private void LoadProducts()
        {
            try
            {
                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT PRODUCTNAME, PRICE, QUANTITY, CATID FROM PRODUCT";
                    OracleCommand cmd = new OracleCommand(query, con);
                    OracleDataReader reader = cmd.ExecuteReader();

                    listView1.Items.Clear(); // Clear the ListView before adding new items
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem(reader["PRODUCTNAME"].ToString());
                        item.SubItems.Add(reader["PRICE"].ToString());
                        item.SubItems.Add(reader["QUANTITY"].ToString());
                        item.SubItems.Add(reader["CATID"].ToString());
                        listView1.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Inventory managerDash = new Inventory(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                string productName = textBox2.Text;
                decimal price = Convert.ToDecimal(textBox4.Text);
                int quantity = Convert.ToInt32(textBox3.Text);
                int categoryId = 1; // Set category ID (adjust as per your logic)

                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();

                    // Get the next unique PRODUCTID
                    string getIdQuery = "SELECT NVL(MAX(PRODUCTID), 0) + 1 FROM PRODUCT";
                    OracleCommand getIdCmd = new OracleCommand(getIdQuery, con);
                    int productId = Convert.ToInt32(getIdCmd.ExecuteScalar());

                    // Insert the new product
                    string insertQuery = "INSERT INTO PRODUCT (PRODUCTID, PRODUCTNAME, PRICE, CATID, QUANTITY) VALUES (:ProductId, :ProductName, :Price, :CatId, :Quantity)";
                    OracleCommand insertCmd = new OracleCommand(insertQuery, con);
                    insertCmd.Parameters.Add(":ProductId", productId);
                    insertCmd.Parameters.Add(":ProductName", productName);
                    insertCmd.Parameters.Add(":Price", price);
                    insertCmd.Parameters.Add(":CatId", categoryId);
                    insertCmd.Parameters.Add(":Quantity", quantity);

                    int rowsAffected = insertCmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Product added successfully!");
                        LoadProducts(); // Refresh the ListView to include the new product
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form is ProductBrowsing productBrowsing)
                            {
                                productBrowsing.LoadNewProducts(); // Method to refresh the product list
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding product: " + ex.Message);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
