using System;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;

namespace DB_LAB_PROJECT
{
    public partial class otpverify : Form
    {
        // Declare a global variable to hold the generated OTP
        private int generatedOTP;

        public otpverify()
        {
            InitializeComponent();
        }

        // This function is called when the user clicks "Send OTP" button
        private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
            {
                MessageBox.Show("Please enter a valid email address.");
                return;
            }

            try
            {
                // Generate a 4-digit OTP and store it in the generatedOTP variable
                Random random = new Random();
                generatedOTP = random.Next(1000, 9999); // Store OTP

                // Send the OTP to the entered email
                SendOTPUsingAnotherMethod(email, generatedOTP);

                MessageBox.Show("OTP sent successfully!");
            }
            catch (SmtpException smtpEx)
            {
                MessageBox.Show($"SMTP Error: {smtpEx.StatusCode}\nDetails: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"General Error: {ex.Message}");
            }
        }

        // This function is called to verify the OTP entered by the user
        private void button2_Click(object sender, EventArgs e)
        {
            string enteredOTP = textBox2.Text.Trim();

            // Compare the entered OTP with the generated OTP
            if (int.TryParse(enteredOTP, out int otp) && otp == generatedOTP)
            {
                MessageBox.Show("OTP verified, Registered Successfully!");
                login loginForm = new login();
                this.Hide(); // Hide the current form
                loginForm.Show(); // Show the Login Form
            }
            else
            {
                MessageBox.Show("Invalid OTP. Please try again.");
            }
        }

        // Helper function to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        // Function to send OTP using Gmail SMTP
        private void SendOTPUsingAnotherMethod(string email, int otp)
        {
            try
            {
                // Email details
                string to = email;
                string from = "dblabproject@gmail.com"; // Replace with your email
                string pass = "lerb cvej iwjn yzqx"; // Replace with your app password
                string otpMessage = "OTP for the verification = " + otp;

                MailMessage message = new MailMessage();
                message.To.Add(to);
                message.From = new MailAddress(from);
                message.Body = otpMessage;
                message.Subject = "OTP VERIFICATION";

                // Setting up the SMTP client
                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    EnableSsl = true,
                    Port = 587, // Port for Gmail SMTP
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(from, pass)
                };

                // Send the email
                smtp.Send(message);

                MessageBox.Show("Verification OTP sent successfully! Check your Gmail.");
                textBox2.Enabled = true; // Enable OTP input textbox
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Event handler for textBox1 TextChanged (optional)
        private void textBox1_TextChanged(object sender, EventArgs e) { }

        // Event handler for textBox2 TextChanged (optional)
        private void textBox2_TextChanged(object sender, EventArgs e) { }
    }
}
