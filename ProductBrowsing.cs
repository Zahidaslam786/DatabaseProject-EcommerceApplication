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
    public partial class ProductBrowsing : Form
    {
        public ProductBrowsing()
        {
            InitializeComponent();
        }

        public void LoadNewProducts()
        {
            try
            {
                string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
                using (OracleConnection con = new OracleConnection(connectionString))
                {
                    con.Open();
                    string query = "SELECT PRODUCTNAME, PRICE FROM PRODUCT ORDER BY PRODUCTID DESC";
                    OracleCommand cmd = new OracleCommand(query, con);
                    OracleDataReader reader = cmd.ExecuteReader();

                    // Clear any previous products before displaying new ones
                    //flowLayoutPanelNewProducts.Controls.Clear();

                    while (reader.Read())
                    {
                        string productName = reader["PRODUCTNAME"].ToString();
                        string price = reader["PRICE"].ToString();

                        // Create a new label for product name
                        Label nameLabel = new Label();
                        nameLabel.Text = productName;
                        nameLabel.AutoSize = true;
                        nameLabel.Font = new System.Drawing.Font("Arial", 10);

                        // Create a new label for price
                        Label priceLabel = new Label();
                        priceLabel.Text = $"Price: {price}";
                        priceLabel.AutoSize = true;
                        priceLabel.Font = new System.Drawing.Font("Arial", 10);

                        // Create the 'Add to Cart' button
                        Button addToCartButton = new Button();
                        addToCartButton.Text = "Add to Cart";
                        addToCartButton.AutoSize = true;
                        addToCartButton.Click += (sender, e) =>
                        {
                            MessageBox.Show($"{productName} added to cart.");
                        };

                        // Add the controls to a panel
                        FlowLayoutPanel productPanel = new FlowLayoutPanel();
                        productPanel.Controls.Add(nameLabel);
                        productPanel.Controls.Add(priceLabel);
                        productPanel.Controls.Add(addToCartButton);

                        // Add the product panel to the FlowLayoutPanel
                        ///flowLayoutPanelNewProducts.Controls.Add(productPanel);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading new products: " + ex.Message);
            }
        }

        private void flowLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ProductBrowsing_Load(object sender, EventArgs e)
        {
            LoadNewProducts(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Electronics electronicsForm = new Electronics(); // Ensure Electronics form exists
            this.Hide(); // Hide the current form
            electronicsForm.Show(); // Show Electronics form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HealthBeauty healthBeautyForm = new HealthBeauty(); // Ensure HealthBeauty form exists
            this.Hide(); // Hide the current form
            healthBeautyForm.Show(); // Show Health & Beauty form
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Fashion fashionForm = new Fashion(); // Ensure Fashion form exists
            this.Hide(); // Hide the current form
            fashionForm.Show(); // Show Fashion form
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BabyKidsToys babyKidsToysForm = new BabyKidsToys(); // Ensure BabyKidsToys form exists
            this.Hide(); // Hide the current form
            babyKidsToysForm.Show(); // Show Baby, Kids, & Toys form
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SportsOutdoors sportsOutdoorsForm = new SportsOutdoors(); // Ensure SportsOutdoors form exists
            this.Hide(); // Hide the current form
            sportsOutdoorsForm.Show(); // Show Sports & Outdoors form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HealthBeauty healthBeautyForm = new HealthBeauty(); // Ensure HealthBeauty form exists
            this.Hide(); // Hide the current form
            healthBeautyForm.Show(); // Show Health & Beauty form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Fashion fashionForm = new Fashion(); // Ensure Fashion form exists
            this.Hide(); // Hide the current form
            fashionForm.Show(); // Show Fashion form
        }

        private void label8_Click(object sender, EventArgs e)
        {
           
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            HealthBeauty healthBeautyForm = new HealthBeauty(); // Ensure HealthBeauty form exists
            this.Hide(); // Hide the current form
            healthBeautyForm.Show(); // Show Health & Beauty form
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            this.Hide(); // Hide the current Welcome Form
            loginForm.Show(); // Show the Login Form
        }

        private void button18_Click(object sender, EventArgs e)
        {
            login loginForm = new login();
            this.Hide(); // Hide the current Welcome Form
            loginForm.Show(); // Show the Login Form
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Electronics electronicsForm = new Electronics(); // Ensure Electronics form exists
            this.Hide(); // Hide the current form
            electronicsForm.Show(); // Show Electronics form
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Fashion fashionForm = new Fashion(); // Ensure Fashion form exists
            this.Hide(); // Hide the current form
            fashionForm.Show(); // Show Fashion form
        }

        private void button11_Click(object sender, EventArgs e)
        {
            HealthBeauty healthBeautyForm = new HealthBeauty(); // Ensure HealthBeauty form exists
            this.Hide(); // Hide the current form
            healthBeautyForm.Show(); // Show Health & Beauty form
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Fashion fashionForm = new Fashion(); // Ensure Fashion form exists
            this.Hide(); // Hide the current form
            fashionForm.Show(); // Show Fashion form
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            HealthBeauty healthBeautyForm = new HealthBeauty(); // Ensure HealthBeauty form exists
            this.Hide(); // Hide the current form
            healthBeautyForm.Show(); // Show Health & Beauty form
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SportsOutdoors sportsOutdoorsForm = new SportsOutdoors(); // Ensure SportsOutdoors form exists
            this.Hide(); // Hide the current form
            sportsOutdoorsForm.Show(); // Show Sports & Outdoors form
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            HealthBeauty healthBeautyForm = new HealthBeauty(); // Ensure HealthBeauty form exists
            this.Hide(); // Hide the current form
            healthBeautyForm.Show(); // Show Health & Beauty form
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            BabyKidsToys babyKidsToysForm = new BabyKidsToys(); // Ensure BabyKidsToys form exists
            this.Hide(); // Hide the current form
            babyKidsToysForm.Show(); // Show Baby, Kids, & Toys form
        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Form8 form = new Form8();
            this.Hide();
            form.Show();
        }
    }
}
