using Oracle.ManagedDataAccess.Client;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DB_LAB_PROJECT
{
    public partial class OrderTracking10 : Form
    {
        private Label label1;
        private Label label3;
        private Label label5;
        private Button button1;
        private Button button2;
        private ListView listView2;
        public ColumnHeader ProductName;
        private ColumnHeader Quantity;
        public ColumnHeader Price;
        private Label label2;

        private string connectionString = @"DATA SOURCE = //localhost:1521/XE; USER ID=LABPROJECT; PASSWORD=123";
        private Label label4;
        private Button button3;
        private int currentUserId = GlobalVariables.LoggedInUserID;

        public OrderTracking10()
        {
            InitializeComponent();
            LoadCartDetails();
        }

        private void LoadCartDetails()
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    /* string query = @"
                         SELECT PRODUCTNAME, PRICE, QUANTITY 
                         FROM CART,
                         WHERE USERID = :UserId";*/
                    
            string query = @"SELECT C.PRODUCTNAME, C.PRICE, C.QUANTITY FROM CART C JOIN CUSTOMER CU ON C.USERID = CU.USERID WHERE C.USERID = :UserId";


                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        command.Parameters.Add(new OracleParameter(":UserId", currentUserId));

                        using (OracleDataReader reader = command.ExecuteReader())
                        {
                            // Clear existing items
                            listView2.Items.Clear();

                            decimal totalCost = 0; // Initialize total cost

                            // Loop through the results and add them to the ListView
                            while (reader.Read())
                            {
                                string productName = reader["PRODUCTNAME"].ToString();
                                int quantity = reader.GetInt32(reader.GetOrdinal("QUANTITY"));
                                decimal price = reader.GetDecimal(reader.GetOrdinal("PRICE"));

                                // Calculate the total cost
                                totalCost += price * quantity;

                                // Create a ListViewItem
                                ListViewItem item = new ListViewItem(productName);
                                item.SubItems.Add(price.ToString("")); // Format price as currency
                                item.SubItems.Add(quantity.ToString());

                                // Add the item to the ListView
                                listView2.Items.Add(item);
                            }

                            label2.Text = "Order ID: " + currentUserId.ToString();
                            label3.Text = "Date: " + DateTime.Now.ToString("dd-MM-yyyy");
                            label4.Text = "Total Cost: " + totalCost;
                            label5.Text = "Current Status: " + "Order is on the Way!";
                            label5.ForeColor = Color.Green;
                            // Display the total cost (this can be in a label or message box)
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

        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.ProductName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Price = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.5F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Order Details";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Order ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(31, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Date";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(31, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Current Status";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(205, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 38);
            this.button1.TabIndex = 5;
            this.button1.Text = "Cancel Order";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(34, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(127, 38);
            this.button2.TabIndex = 6;
            this.button2.Text = "Back";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listView2
            // 
            this.listView2.BackColor = System.Drawing.Color.LightGray;
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ProductName,
            this.Quantity,
            this.Price});
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(34, 147);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(186, 125);
            this.listView2.TabIndex = 8;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // ProductName
            // 
            this.ProductName.Text = "Item Name";
            this.ProductName.Width = 82;
            // 
            // Quantity
            // 
            this.Quantity.DisplayIndex = 2;
            this.Quantity.Text = "Quantity";
            this.Quantity.Width = 54;
            // 
            // Price
            // 
            this.Price.DisplayIndex = 1;
            this.Price.Text = "Price";
            this.Price.Width = 47;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(31, 288);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Total : ";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(376, 367);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(127, 38);
            this.button3.TabIndex = 10;
            this.button3.Text = "Feedback";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // OrderTracking10
            // 
            this.BackgroundImage = global::DB_LAB_PROJECT.Properties.Resources.shopping_bag_cart_23_2148879372;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(751, 440);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "OrderTracking10";
            this.Text = "Order Tracking";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OrderConfirmation9 orderConfirmationForm = new OrderConfirmation9();
            orderConfirmationForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OracleConnection connection = new OracleConnection(connectionString))
                {
                    connection.Open();

                    // Delete items from the cart for the current user
                    string deleteQuery = "DELETE FROM CART WHERE USERID = :UserId";

                    using (OracleCommand command = new OracleCommand(deleteQuery, connection))
                    {
                        command.Parameters.Add(new OracleParameter(":UserId", currentUserId));

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Order canceled and cart cleared successfully!");

                            // Optionally, clear the ListView to reflect the empty cart
                            listView2.Items.Clear();
                            label4.Text = "Total Cost: 0";
                            label5.Text = "Current Status: No active orders.";
                            label5.ForeColor = Color.Red; // Change the status color to red
                        }
                        else
                        {
                            MessageBox.Show("No items found in the cart to clear.");
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

        private void button3_Click(object sender, EventArgs e)
        {
            feedback feedback = new feedback();
            this.Hide();
            feedback.Show();
        }
    }
}
