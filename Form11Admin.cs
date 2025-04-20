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
    public partial class Form11Admin : Form
    {
        public Form11Admin()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Inventory managerDash = new Inventory(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show(); 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Discount managerDash = new Discount(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AdminFeedback managerDash = new AdminFeedback(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            login login = new login();
            this.Hide();
            login.Show();
        }
    }
}
