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
    public partial class CartManagement : Form
    {
        private string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";

        private int currentUserId = GlobalVariables.LoggedInUserID;
        public CartManagement()
        {
            InitializeComponent();
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
                            listView2.Items.Clear();

                            // Loop through the results and add them to the ListView
                            while (reader.Read())
                            {
                                string productName = reader["PRODUCTNAME"].ToString();
                                decimal price = reader.GetDecimal(reader.GetOrdinal("PRICE"));
                                int quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"));

                                // Create a ListViewItem
                                ListViewItem item = new ListViewItem(productName);
                                item.SubItems.Add(quantity.ToString());
                                item.SubItems.Add(price.ToString()); // Format as currency

                                // Add the item to the ListView
                                listView2.Items.Add(item);
                            }
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

        private void CartManagement_Load(object sender, EventArgs e)
        {
            LoadCartDetails();
        }

        private void button9_Click(object sender, EventArgs e)//For BACK 
        {
            ProductBrowsing checkoutForm = new ProductBrowsing();
            checkoutForm.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 checkoutForm = new Form8();
            checkoutForm.Show();
            this.Hide();
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
