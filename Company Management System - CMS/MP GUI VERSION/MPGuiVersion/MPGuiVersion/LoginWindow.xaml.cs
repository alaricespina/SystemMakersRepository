using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data.SqlClient;

namespace MPGuiVersion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        SqlConnection sql_conn;
        bool connected;

        public LoginWindow()
        {          
            InitializeComponent();
            
            
        }
        
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string output;
            DatabaseConnection.connectToSQL(out this.sql_conn, out this.connected, out output);
            string email_text = EmailBox.Text;
            string pass_text = PassBox.Password;
            int result = DatabaseConnection.logIn(this.sql_conn, email_text, pass_text);

            if (result == 2)
            {
                this.showMainMenu();
            } else
            {
                MessageBox.Show($"An Error has Occurred, please double check your login details\nResult:{result}");
            }

            // SQL Command Setup
            
        }
        
        private void Signup_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Signup Button was clicked");
            SignupForm SF = new SignupForm();
            SF.Show();
        }

        private void showMainMenu()
        {
            this.Hide(); // Hide This login window
            this.sql_conn.Close(); // Closes SQL Connection
            MainMenu MM = new MainMenu();
            MM.Show(); // Shows Main Menu
            this.Close(); // Closes Upon Close of Main Menu
        }

    }
}
