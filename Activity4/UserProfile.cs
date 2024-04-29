using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System;
using System.Data;
using System.Net.Mail;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using Activity4.Activity4;
using System.Data.Common;

namespace Activity4
{
    public partial class UserProfile : Form
    {
        // Use public MySQL connection
        private Connection myConnection;

        public UserProfile()
        {
            InitializeComponent();
            myConnection = new Connection(); // Initialize the connection
        }
        public void SetUserValues(string userID, string email, string password, string username, string first_name, string surname, string phone_num, string acc_status)
        {
            email_textbox.Text = email;
            password_textbox.Text = password;
            username_textbox.Text = username;
            firstname_textbox.Text = first_name;
            surname_textbox.Text = surname;
            phone_textbox.Text = phone_num;        
        }

        private void LogOut_Click(object sender, EventArgs e)
        {
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();
            LoginRedirect();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();
            LoginRedirect();
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
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();
            Application.Exit();
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

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            if (allFilled())
            {
                try
                {
                    // Create an instance of the Connection class
                    Connection database = new Connection();

                    if (database.Conn.State == ConnectionState.Open) // Check if the connection is open
                    {
                        // Proceed with updating the user information
                        string updateQuery = "UPDATE users SET username = @username, first_name = @first_name, " +
                                             "surname = @surname, phone = @phone, emp_pass = @emp_pass " +
                                             "WHERE emp_email = @emp_email";
                        MySqlCommand updateCommand = new MySqlCommand(updateQuery, database.Conn);

                        // Add parameters for the update operation
                        updateCommand.Parameters.AddWithValue("@username", username_textbox.Text);
                        updateCommand.Parameters.AddWithValue("@first_name", firstname_textbox.Text);
                        updateCommand.Parameters.AddWithValue("@surname", surname_textbox.Text);
                        updateCommand.Parameters.AddWithValue("@phone", phone_textbox.Text);
                        updateCommand.Parameters.AddWithValue("@emp_email", email_textbox.Text);
                        updateCommand.Parameters.AddWithValue("@emp_pass", password_textbox.Text);

                        try
                        {
                            int rowsAffected = updateCommand.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("User information updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                clearTextbox();
                                AccountListRedirect();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update user information!");
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error in updating user information!" + ex.Message);
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

        private void AccountListRedirect()
        {
            this.Hide();
            AccountList frm = new AccountList();
            frm.ShowDialog();
            this.Close();
        }

        private void Accounts_label_Click(object sender, EventArgs e)
        {
            AccountListRedirect();
        }

        private void Dashboard_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.ShowDialog();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Reports_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportGeneratorInventory frm = new ReportGeneratorInventory();
            frm.ShowDialog();
            this.Close();
        }

        private void About_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            AboutProgram frm = new AboutProgram();
            frm.ShowDialog();
            this.Close();
        }
    }
}
