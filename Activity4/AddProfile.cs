using Activity4.Activity4;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Activity4
{
    public partial class AddProfile : Form
    {
        public Connection myConnection;

        public AddProfile()
        {
            InitializeComponent();
            myConnection = new Connection();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }
            
        private bool allFilled()
        {
            // Check if all required fields are filled
            return !string.IsNullOrWhiteSpace(username_textbox.Text) &&
                   !string.IsNullOrWhiteSpace(firstname_textbox.Text) &&
                   !string.IsNullOrWhiteSpace(surname_textbox.Text) &&
                   !string.IsNullOrWhiteSpace(phone_textbox.Text) &&
                   !string.IsNullOrWhiteSpace(email_textbox.Text) &&
                   !string.IsNullOrWhiteSpace(password_textbox.Text);
        }

        private void clearTextbox()
        {
            username_textbox.Text = "";
            firstname_textbox.Text = "";
            surname_textbox.Text = "";
            phone_textbox.Text = "";
            email_textbox.Text = "";
            password_textbox.Text = "";
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();

            Application.Exit();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            if (allFilled())
            {
                try
                {
                    // Create an instance of the Connection class
                    Connection database = new Connection();

                    if (database.Conn.State == ConnectionState.Open) // Check if the connection is open
                    {
                        // Proceed with insertion
                        string insert_acc = "INSERT INTO users (username, first_name, surname, phone, emp_email, emp_pass, status) " +
                                             "VALUES (@username, @first_name, @surname, @phone, @emp_email, @emp_pass, @status)";
                        MySqlCommand insertCommand = new MySqlCommand(insert_acc, database.Conn);

                        // Add parameters for insertion
                        insertCommand.Parameters.AddWithValue("@username", username_textbox.Text);
                        insertCommand.Parameters.AddWithValue("@first_name", firstname_textbox.Text);
                        insertCommand.Parameters.AddWithValue("@surname", surname_textbox.Text);
                        insertCommand.Parameters.AddWithValue("@phone", phone_textbox.Text);
                        insertCommand.Parameters.AddWithValue("@emp_email", email_textbox.Text);
                        insertCommand.Parameters.AddWithValue("@emp_pass", password_textbox.Text);
                        insertCommand.Parameters.AddWithValue("@status", "Inactive");

                        try
                        {
                            int rowsAffected = insertCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("User was successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearTextbox();
                                AccountListRedirect();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add user!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in adding user!" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Database connection failed!");
                    }

                    database.CloseConnection(); // Close the database connection
                }
                catch (Exception ex)
                {
                    Console.WriteLine("MySQL Error!" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please fill in all required fields!");
            }
        }

        private void Dashboard_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.ShowDialog();
            this.Close();
        }

        private void AccountListRedirect()
        {
            this.Hide();
            AccountList frm = new AccountList();
            frm.ShowDialog();
            this.Close();
        }

        private void ProfileRedirect()
        {
            this.Hide();
            UserProfile frm = new UserProfile();
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

        private void LogOut_Click(object sender, EventArgs e)
        {
            // Create an instance of UserStatusUpdater
            AccountStatus acc_status = new AccountStatus(myConnection);

            // Call the InactiveStatus method
            acc_status.InactiveStatus();

            // Redirect to login
            LoginRedirect();
        }


        private void LogOutButton_Click(object sender, EventArgs e)
        {
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();
            LoginRedirect();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }
    }
}
