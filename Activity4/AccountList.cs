using Activity4.Activity4;
using Guna.UI2.WinForms;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Activity4
{
    public partial class AccountList : Form
    {
        // Use public MySQL connection
        public Connection myConnection;

        public AccountList()
        {
            InitializeComponent();
            myConnection = new Connection();

            accountListView.CellContentClick += accountListView_CellContentClick;

            // Subscribe to the Load event of the form
            this.Load += AccountList_Load;

            accountListView.RowPrePaint += accountListView_RowPrePaint;
        }

        private void AccountList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                // Ensure the connection is open before executing the query
                if (myConnection.Conn.State == ConnectionState.Closed)
                    myConnection.Conn.Open();

                string query = "SELECT * FROM users";

                // Use the public MySqlConnection from Connection class
                using (MySqlCommand cmd = new MySqlCommand(query, myConnection.Conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable db = new DataTable();
                    adapter.Fill(db);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = db;

                    accountListView.DataSource = bindingSource;

                    accountListView.Columns["emp_id"].HeaderText = "ID";
                    accountListView.Columns["emp_email"].HeaderText = "Email Address";
                    accountListView.Columns["emp_pass"].HeaderText = "Password";
                    accountListView.Columns["username"].HeaderText = "Username";
                    accountListView.Columns["first_name"].HeaderText = "First Name";
                    accountListView.Columns["surname"].HeaderText = "Surname";
                    accountListView.Columns["phone"].HeaderText = "Phone";
                    accountListView.Columns["status"].HeaderText = "Status";

                    accountListView.Columns["emp_id"].Width = 15;
                    accountListView.Columns["emp_email"].Width = 65;
                    accountListView.Columns["emp_pass"].Width = 40;
                    accountListView.Columns["username"].Width = 50;
                    accountListView.Columns["first_name"].Width = 45;
                    accountListView.Columns["surname"].Width = 40;
                    accountListView.Columns["phone"].Width = 45;
                    accountListView.Columns["status"].Width = 50;

                    // accountListView.Columns.RowTemplate.Height = 50; // Adjust the height of each row
                }
            }
            catch (MySqlException ex) // Handle MySQL exceptions
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
            }
            finally
            {
                // Always ensure to close the connection after use
                if (myConnection.Conn.State == ConnectionState.Open)
                    myConnection.CloseConnection();
            }
        }

        private void accountListView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // Check if the current row is the row containing the column headers
            if (e.RowIndex == -1)
            {
                using (SolidBrush brush = new SolidBrush(ColorTranslator.FromHtml("#8ebd8b")))
                {
                    e.Graphics.FillRectangle(brush, e.RowBounds);
                }
            }
        }

        private void accountListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && accountListView.Columns[e.ColumnIndex].Name == "UpdateUser")
            {
                string userID = accountListView.Rows[e.RowIndex].Cells["emp_id"].Value.ToString();
                string email = accountListView.Rows[e.RowIndex].Cells["emp_email"].Value.ToString();
                string password = accountListView.Rows[e.RowIndex].Cells["emp_pass"].Value.ToString();
                string username = accountListView.Rows[e.RowIndex].Cells["username"].Value.ToString();
                string first_name = accountListView.Rows[e.RowIndex].Cells["first_name"].Value.ToString();
                string surname = accountListView.Rows[e.RowIndex].Cells["surname"].Value.ToString();
                string phone_num = accountListView.Rows[e.RowIndex].Cells["phone"].Value.ToString();
                string acc_status = accountListView.Rows[e.RowIndex].Cells["status"].Value.ToString();

                UserProfile userForm = new UserProfile();
                userForm.SetUserValues(userID, email, password, username, first_name, surname, phone_num, acc_status);
                userForm.ShowDialog();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            AccountListRedirect();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();
            Application.Exit();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddAccount()
        {
            this.Hide();
            AddProfile frm = new AddProfile();
            frm.ShowDialog();
            this.Close();
        }

        private void AddIcon_Click(object sender, EventArgs e)
        {
            AddAccount();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
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

        private void DashboardRedirect()
        {
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.ShowDialog();
            this.Close();
        }

        private void Dashboard_label_Click(object sender, EventArgs e)
        {
            DashboardRedirect();
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure the connection is open before executing the query
                if (myConnection.Conn.State == ConnectionState.Closed)
                    myConnection.Conn.Open();

                // Retrieve the emp_id entered by the user
                string empId = search_textbox.Text.Trim();

                // Execute the SQL query to search for the record with the specified emp_id
                string query = "SELECT * FROM users WHERE emp_id = @EmpId";
                using (MySqlCommand cmd = new MySqlCommand(query, myConnection.Conn))
                {
                    cmd.Parameters.AddWithValue("@EmpId", empId);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable db = new DataTable();
                    adapter.Fill(db);

                    // Check if any record is found
                    if (db.Rows.Count > 0)
                    {
                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = db;

                        accountListView.DataSource = bindingSource;

                        clearTextbox();
                    }
                    else
                    {
                        MessageBox.Show("Record not found! Not existing employee.", "Search Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Always ensure to close the connection after use
                if (myConnection.Conn.State == ConnectionState.Open)
                    myConnection.CloseConnection();
            }
        }

        private void AccountListRedirect()
        {
            this.Hide();
            AccountList frm = new AccountList();
            frm.ShowDialog();
            this.Close();
        }

        private void clearTextbox()
        {
            search_textbox.Text = "";
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
