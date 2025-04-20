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
    public partial class Inventory : Form
    {
        public Inventory()
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

        private void button4_Click(object sender, EventArgs e)
        {
            Form11Admin managerDash = new Form11Admin(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show(); 
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddProduct managerDash = new AddProduct(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteProduct managerDash = new DeleteProduct(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateProduct managerDash = new UpdateProduct(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }
    }
}
