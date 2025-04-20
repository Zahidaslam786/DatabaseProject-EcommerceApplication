using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DB_LAB_PROJECT
{
    public partial class Form1 : Form
    {
     
        public Form1()
        {
            InitializeComponent();

          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
        }
      
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            // Open the Login Form
            login loginForm = new login();
            this.Hide(); // Hide the current Welcome Form
            loginForm.Show(); // Show the Login Form
        }

        private void button8_Click(object sender, EventArgs e)
        {
            // Open the Sign Up Form
            SIGNUP signUpForm = new SIGNUP();
            this.Hide(); // Hide the current Welcome Form
            signUpForm.Show(); // Show the Sign Up Form
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
