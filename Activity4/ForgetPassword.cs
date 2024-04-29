using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity4
{
    public partial class ForgetPassword : Form
    {
        // Define a public static property to hold the email address
        public static string email_address;

        // Use public MySQL connection
        public Connection myConnection;

        public ForgetPassword()
        {
            InitializeComponent();
            myConnection = new Connection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ResetPassButton_Click(object sender, EventArgs e)
        {
            string newPassword = newPass.Text;
            string confirmedPassword = confirmNewPass.Text;

            // Check if passwords match
            if (newPassword != confirmedPassword)
            {
                MessageBox.Show("Passwords do not match! Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if password meets minimum length requirement of 8 characters
            if (newPassword.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string email = EnterEmail.email_address;

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Email address not provided! Unable to reset password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string stm = "UPDATE users SET emp_pass = @Password WHERE emp_email = @Email";

                // Use the connection from the Connection class
                using (MySqlCommand cmd = new MySqlCommand(stm, myConnection.Conn))
                {
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@Email", email);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Successful password update, back to login
                        MessageBox.Show("Password updated! Please log in again.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginRedirect();
                    }
                    else
                    {
                        // No rows affected (email not found)
                        MessageBox.Show("Email not found! Password update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MySqlException ex)
            {
                // Handle MySQL exceptions
                MessageBox.Show("MySQL Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginRedirect()
        {
            this.Hide();
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
            this.Close();
        }
    }
}
