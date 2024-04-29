using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Activity4
{
    public partial class LoginForm : Form
    {
        // Define a public static property to hold the email address
        public string EmailAddress { get; private set; }

        // Use public MySQL connection
        public Connection myConnection;

        public LoginForm()
        {
            InitializeComponent();
            myConnection = new Connection();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Create an instance of the Connection class
                Connection conn = new Connection();

                string email = email_textbox.Text;
                string password = password_textbox.Text;
                string stm = "SELECT emp_email, emp_pass FROM users WHERE emp_email = @Email";

                // Use the connection from the Connection class
                using (MySqlCommand cmd = new MySqlCommand(stm, conn.Conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.HasRows)
                        {
                            // Read the password from the database
                            rdr.Read();
                            string dbPassword = rdr["emp_pass"].ToString();

                            if (password == dbPassword)
                            {
                                // Set the value of the public static property
                                EmailAddress = email_textbox.Text;

                                ActiveStatus(email);

                                // Redirect to Dashboard
                                DashboardRedirect();                                
                            }
                            else // Incorrect password
                            {
                                MessageBox.Show("Incorrect password! Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else // Incorrect email
                        {
                            MessageBox.Show("Incorrect email! Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (MySqlException ex) // Handle MySQL exceptions
            {
                Console.WriteLine("MySQL Error!" + ex.Message);
            }
        }

        private void ActiveStatus(string email)
        {
            string stm = "UPDATE users SET status = 'Active' WHERE emp_email = @Email";

            try
            {
                // conn.Open();

                using (MySqlCommand cmd = new MySqlCommand(stm, myConnection.Conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DashboardRedirect()
        {
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.ShowDialog();
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
            // InactiveStatus(EmailAddress); Update status before logging out
        }

        private void ForgotPassLabel_Click(object sender, EventArgs e)
        {
            FindEmailRedirect();
        }

        private void FindEmailRedirect()
        {
            this.Hide();
            EnterEmail frm = new EnterEmail();
            frm.ShowDialog();
            this.Close();
        }
    }
}
