using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Activity4
{
    using MySql.Data.MySqlClient;
    using System;
    using System.Data;
    using System.Windows.Forms;

    namespace Activity4
    {
        public class AccountStatus
        {
            private readonly Connection myConnection;

            public AccountStatus(Connection connection)
            {
                myConnection = connection;
            }

            public void InactiveStatus()
            {
                string stm = "UPDATE users SET status = 'Inactive'";

                try
                {
                    // Open the connection if it's closed
                    if (myConnection.Conn.State == ConnectionState.Closed)
                        myConnection.Conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand(stm, myConnection.Conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    // Always ensure to close the connection after use
                    if (myConnection.Conn.State == ConnectionState.Open)
                        myConnection.CloseConnection();
                }
            }
        }
    }

}
