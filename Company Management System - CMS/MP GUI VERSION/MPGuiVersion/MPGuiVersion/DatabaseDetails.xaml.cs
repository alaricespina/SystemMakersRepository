using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace MPGuiVersion
{
    /// <summary>
    /// Interaction logic for DatabaseDetails.xaml
    /// </summary>
    public partial class DatabaseDetails : Window
    {
        SqlConnection conn;
        public DatabaseDetails()
        {
            InitializeComponent();
        }

        private void SetDetails_Click(object sender, RoutedEventArgs e)
        {
            bool connected;
            string message;
            try
            {
                DatabaseConnection.ComputerName = ServerName.Text;
                DatabaseConnection.DatabaseName = DataBaseName.Text;
                DatabaseConnection.connectToSQL(out this.conn, out connected, out message);
                if (!connected)
                {
                    MessageBox.Show($"Error Details\n\n{message}");
                    throw new NotSupportedException();

                }
                DatabaseConnection.disconnectSQL(this.conn, out connected);
                LoginWindow LW = new LoginWindow();
                this.Hide();
                LW.Show();
                this.Close();
            } catch
            {
                MessageBox.Show($"An Error has occured:\nPlease check the details and try again");
            }
            
        }
    }
}
