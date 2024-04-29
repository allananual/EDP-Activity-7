using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity4
{
    public partial class EnterEmail : Form
    {
        // Define a public static property to hold the email address
        public static string email_address { get; private set; }

        // Use public MySQL connection
        public Connection myConnection;

        public EnterEmail()
        {
            InitializeComponent();
            myConnection = new Connection();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void ForgotPasswordRedirect()
        {
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.ShowDialog();
            this.Close();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                string email = email_textbox.Text;
                string stm = "SELECT emp_email FROM users WHERE emp_email = @Email";

                // Use the connection from the Connection class
                using (MySqlCommand cmd = new MySqlCommand(stm, myConnection.Conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email_textbox.Text);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            // Set the value of the public static property
                            email_address = email_textbox.Text;

                            // Existing email                        
                            rdr.Read();
                            string dbEmail = rdr["emp_email"].ToString();

                            if (email == dbEmail)
                            {
                                // Successful login, navigate to the dashboard
                                ForgotPassRedirect();
                                return; // Return after successful redirect
                            }
                        }

                        // Email does not exist or incorrect
                        MessageBox.Show("Account does not exist! Incorrect email.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MySqlException ex) // Handle MySQL exceptions
            {
                MessageBox.Show("MySQL Error!" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CancelButton_Click(object sender, EventArgs e)
        {
            LoginRedirect();
        }

        private void ForgotPassRedirect()
        {
            this.Hide();
            ForgetPassword frm = new ForgetPassword();
            frm.ShowDialog();
            this.Close();
        }

        private void LoginRedirect()
        {
            this.Hide();
            LoginForm frm = new LoginForm();
            frm.ShowDialog();
            this.Close();
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void email_textbox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
