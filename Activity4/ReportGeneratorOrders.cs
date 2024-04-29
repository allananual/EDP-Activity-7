using Activity4.Activity4;
using ClosedXML.Excel;
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

namespace Activity4
{
    public partial class ReportGeneratorOrders : Form
    {
        public Connection myConnection;

        public ReportGeneratorOrders()
        {
            InitializeComponent();
            myConnection = new Connection();
            OrdersGridView.CellContentClick += OrdersGridView_CellContentClick;
            this.Load += OrdersList_Load;
        }

        private void OrdersGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void OrdersList_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                if (myConnection.Conn.State == ConnectionState.Closed)
                    myConnection.Conn.Open();

                string query = "SELECT * FROM purchase_history";

                using (MySqlCommand cmd = new MySqlCommand(query, myConnection.Conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable db = new DataTable();
                    adapter.Fill(db);
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = db;

                    OrdersGridView.DataSource = bindingSource;

                    OrdersGridView.Columns["purchase_id"].HeaderText = "Purchase ID";
                    OrdersGridView.Columns["product_id"].HeaderText = "Product ID";
                    OrdersGridView.Columns["product_name"].HeaderText = "Product Name";
                    OrdersGridView.Columns["supplier_name"].HeaderText = "Supplier Name";
                    OrdersGridView.Columns["purchase_date"].HeaderText = "Purchase Date";
                    OrdersGridView.Columns["quantity"].HeaderText = "Quantity";
                    OrdersGridView.Columns["total_price"].HeaderText = "Total Price";

                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("MySQL Error: " + ex.Message);
            }
            finally
            {
                if (myConnection.Conn.State == ConnectionState.Open)
                    myConnection.CloseConnection();
            }
        }

        private void ExportFile()
        {
            if (OrdersGridView.SelectedRows.Count > 0)
            {
                DataTable selectedRowsData = new DataTable();

                foreach (DataGridViewColumn column in OrdersGridView.Columns)
                {
                    selectedRowsData.Columns.Add(column.Name);
                }

                foreach (DataGridViewRow selectedRow in OrdersGridView.SelectedRows)
                {
                    DataRow newRow = selectedRowsData.Rows.Add();
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        newRow[cell.ColumnIndex] = cell.Value;
                    }
                }

                ExcelExport(selectedRowsData);
            }
            else
            {
                MessageBox.Show("Please select one or more rows to print.");
            }
        }

        private void ExcelExport(DataTable dataTable)
        {
            try
            {
                // Generate a unique file name based on current date and time
                string fileName = $"Product Purchases Report ({DateTime.Now.ToString("yyyy-MM-dd HH-mm")}).xlsx";
                string saveFilePath = $"C:\\Users\\Acer\\Desktop\\3rd year ['23-'24]\\event driven programming\\Activity6\\{fileName}";

                string templateFilePath = "C:\\Users\\Acer\\Desktop\\3rd year ['23-'24]\\event driven programming\\Activity6\\Templates\\Product Purchases.xlsx";

                using (var workbook = new XLWorkbook(templateFilePath))
                {
                    var worksheet = workbook.Worksheet(1);

                    int startRow = 17;
                    int startColumn = 2;

                    // Insert data from the DataTable into the worksheet starting from the first empty row
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            string columnName = dataTable.Columns[i].ColumnName;

                            if (columnName == "purchase_id" || columnName == "product_id" || columnName == "quantity")
                            {
                                if (int.TryParse(dataRow[i].ToString(), out int integerValue))
                                {
                                    worksheet.Cell(startRow, startColumn + i).SetValue(integerValue);
                                    worksheet.Cell(startRow, startColumn + i).Style.NumberFormat.NumberFormatId = 1;
                                }
                                else
                                {
                                    worksheet.Cell(startRow, startColumn + i).Value = dataRow[i].ToString();
                                }
                            }
                            else if (columnName == "total_price")
                            {
                                if (double.TryParse(dataRow[i].ToString(), out double doubleValue))
                                {
                                    worksheet.Cell(startRow, startColumn + i).SetValue(doubleValue);
                                }
                                else
                                {
                                    worksheet.Cell(startRow, startColumn + i).Value = dataRow[i].ToString();
                                }
                            }
                            else if (columnName == "purchase_date")
                            {
                                if (DateTime.TryParse(dataRow[i].ToString(), out DateTime dateValue))
                                {
                                    worksheet.Cell(startRow, startColumn + i).SetValue(dateValue);
                                    worksheet.Cell(startRow, startColumn + i).Style.DateFormat.Format = "dd MMM yyyy";
                                }
                                else
                                {
                                    worksheet.Cell(startRow, startColumn + i).Value = dataRow[i].ToString();
                                }
                            }
                            else
                            {
                                worksheet.Cell(startRow, startColumn + i).Value = dataRow[i].ToString();
                            }
                        }
                        startRow++;
                    }

                    worksheet.Cell(11, 4).Value = DateTime.Now.ToString("MMM dd, yyyy");

                    workbook.SaveAs(saveFilePath);
                    MessageBox.Show($"Successfully exported to {saveFilePath}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while exporting to Excel: {ex.ToString()}");
            }
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            ExportFile();
        }

        private void ExportBtn_Click(object sender, EventArgs e)
        {
            ExportFile();
        }

        private void guna2ShadowPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportGeneratorInventory frm = new ReportGeneratorInventory();
            frm.ShowDialog();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ReportGeneratorSales frm = new ReportGeneratorSales();
            frm.ShowDialog();
            this.Close();
        }

        private void sales_box_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            AccountStatus acc_status = new AccountStatus(myConnection);
            acc_status.InactiveStatus();
            Application.Exit();
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

        private void Dashboard_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.ShowDialog();
            this.Close();
        }

        private void Accounts_label_Click(object sender, EventArgs e)
        {
            this.Hide();
            AccountList frm = new AccountList();
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

        private void ProfileRedirect()
        {
            this.Hide();
            UserProfile frm = new UserProfile();
            frm.ShowDialog();
            this.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            ProfileRedirect();
        }
    }
}
