using Activity4.Activity4;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Activity4
{
    public partial class Dashboard : Form
    {
        // Use public MySQL connection
        public Connection myConnection;

        public Dashboard()
        {
            InitializeComponent();
            myConnection = new Connection();
        }

        private void guna2ShadowPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2ShadowPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label13_Click_1(object sender, EventArgs e)
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

        private void label10_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }

        private void ProfileRedirect()
        {
            this.Hide();
            UserProfile frm = new UserProfile();
            frm.ShowDialog();
            this.Close();
        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void user_icon_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }

        private void Accounts_label_Click(object sender, EventArgs e)
        {
            AccountsRedirect();
        }

        private void AccountsRedirect()
        {
            this.Hide();
            AccountList frm = new AccountList();
            frm.ShowDialog();
            this.Close();
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
