using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace DB_LAB_PROJECT
{
    public partial class HealthBeauty : Form
    {
        /*int loggedInUserID = GlobalVariables.LoggedInUserID;*/
        private int currentUserId = GlobalVariables.LoggedInUserID; // Set this to the logged-in user's ID
        private int[] ProductId = { 17, 18, 19, 20, 21, 22, 23, 24 };
        // Database connection string
        private string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
        public HealthBeauty()
        {
            InitializeComponent();
        }

        private void HealthBeauty_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            // Navigate back to ProductBrowsing Form
            ProductBrowsing productBrowsingForm = new ProductBrowsing();
            this.Hide(); // Hide the current form
            productBrowsingForm.Show(); // Show the Product Browsing Form
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Checkout checkoutForm = new Checkout();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button10_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button11_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button12_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button13_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button14_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button16_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }

        private void button18_Click(object sender, EventArgs e)
        {
            // Navigate back to ProductBrowsing Form
            ProductBrowsing productBrowsingForm = new ProductBrowsing();
            this.Hide(); // Hide the current form
            productBrowsingForm.Show(); // Show the Product Browsing Form
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }
        //Add To Cart Function
        private void AddProductToCart(int productId)
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                connection.Open();

                // Query to get product details
                string productQuery = "SELECT PRODUCTNAME, PRICE FROM PRODUCT WHERE PRODUCTID = :ProductId";
                using (OracleCommand productCommand = new OracleCommand(productQuery, connection))
                {
                    productCommand.Parameters.Add(new OracleParameter(":ProductId", productId));

                    using (OracleDataReader reader = productCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string productName = reader["PRODUCTNAME"].ToString();
                            decimal price = reader.GetDecimal(reader.GetOrdinal("PRICE"));

                            // Check if the product already exists in the cart for the current user
                            string checkCartQuery = @"
                            SELECT CARTID, QUANTITY, PRICE 
                            FROM CART 
                            WHERE PRODUCTID = :ProductId AND USERID = :UserId";

                            using (OracleCommand checkCartCommand = new OracleCommand(checkCartQuery, connection))
                            {
                                checkCartCommand.Parameters.Add(new OracleParameter(":ProductId", productId));
                                checkCartCommand.Parameters.Add(new OracleParameter(":UserId", currentUserId));

                                using (OracleDataReader cartReader = checkCartCommand.ExecuteReader())
                                {
                                    if (cartReader.Read())
                                    {
                                        // Product exists in the cart, update quantity and price
                                        int existingQuantity = cartReader.GetInt32(cartReader.GetOrdinal("QUANTITY"));
                                        decimal existingPrice = cartReader.GetDecimal(cartReader.GetOrdinal("PRICE"));
                                        decimal newTotalPrice = existingPrice + (existingPrice / existingQuantity); // Increment price based on quantity

                                        // Update the cart with new quantity and price
                                        string updateQuery = @"
                                        UPDATE CART 
                                        SET QUANTITY = :NewQuantity, PRICE = :NewTotalPrice 
                                        WHERE CARTID = :CartId";

                                        using (OracleCommand updateCommand = new OracleCommand(updateQuery, connection))
                                        {
                                            updateCommand.Parameters.Add(new OracleParameter(":NewQuantity", existingQuantity + 1)); // Increment quantity by 1
                                            updateCommand.Parameters.Add(new OracleParameter(":NewTotalPrice", newTotalPrice));
                                            updateCommand.Parameters.Add(new OracleParameter(":CartId", cartReader.GetInt32(cartReader.GetOrdinal("CARTID"))));

                                            updateCommand.ExecuteNonQuery();
                                        }

                                        MessageBox.Show("Product quantity updated in cart.");
                                    }
                                    else
                                    {
                                        // Product does not exist in the cart, insert new record
                                        string insertQuery = @"
                                        INSERT INTO CART (CARTID, PRODUCTID, PRODUCTNAME, PRICE, QUANTITY, USERID)
                                        VALUES ((SELECT NVL(MAX(CARTID), 0) + 1 FROM CART), :ProductId, :ProductName, :Price, :Quantity, :UserId)";

                                        using (OracleCommand insertCommand = new OracleCommand(insertQuery, connection))
                                        {
                                            insertCommand.Parameters.Add(new OracleParameter(":ProductId", productId));
                                            insertCommand.Parameters.Add(new OracleParameter(":ProductName", productName));
                                            insertCommand.Parameters.Add(new OracleParameter(":Price", price));
                                            insertCommand.Parameters.Add(new OracleParameter(":Quantity", 1)); // Start with quantity 1
                                            insertCommand.Parameters.Add(new OracleParameter(":UserId", currentUserId));

                                            insertCommand.ExecuteNonQuery();
                                        }

                                        MessageBox.Show("Product added to cart successfully.");
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Product not found.");
                        }
                    }
                }
            }

            // Navigate to Cart Management Form
            CartManagement cartManagementForm = new CartManagement();
            this.Hide(); // Hide the current form
            cartManagementForm.Show(); // Show the Cart Management Form
        }
        //------------------------------------------------------
        private void button10_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            // Navigate to Checkout Form
            Form8 checkoutForm = new Form8();
            this.Hide(); // Hide the current form
            checkoutForm.Show(); // Show the Checkout Form
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[0]); // Product ID 17
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[1]); // Product ID 18
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[2]); // Product ID 19
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[3]); // Product ID 20
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[4]); // Product ID 21
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[5]); // Product ID 22
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[6]); // Product ID 23
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            AddProductToCart(ProductId[7]); // Product ID 24
        }
    }
}
