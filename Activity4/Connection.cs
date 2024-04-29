using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Activity4
{
    public class Connection
    {
        public MySqlConnection Conn { get; private set; }
        string myConnectionString;

        public Connection()
        {
            myConnectionString = "server=localhost;uid=root;pwd=angelaiko;database=grocery_inventory";

            try
            {
                Conn = new MySqlConnection(myConnectionString);
                Conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CloseConnection()
        {
            if (Conn.State == System.Data.ConnectionState.Open)
            {
                Conn.Close();
            }
        }
    }
}
