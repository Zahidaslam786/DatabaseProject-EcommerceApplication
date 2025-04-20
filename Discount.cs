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
    public partial class Discount : Form
    {
        public Discount()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form11Admin managerDash = new Form11Admin(); // Ensure ManagerDashboard form exists
            this.Hide(); // Hide the current form
            managerDash.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }
    }
}
