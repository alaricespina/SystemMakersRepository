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

using System.Data.SqlClient;


namespace MPGuiVersion
{
    /// <summary>
    /// Interaction logic for SignupForm.xaml
    /// </summary>
    public partial class SignupForm : Window
    {
        SqlConnection sql_conn;
        bool connected = false;
        public SignupForm()
        {
            InitializeComponent();
        }

  

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            string output;
            string first_name = FNBox.Text;
            string middle_name = MNBox.Text;
            string last_name = LNBox.Text;
            string email_address = EmailBox.Text;
            string password_text = PassBox.Password;
            string confirm_password_text = ConfirmPassBox.Password;

            

            if (password_text != confirm_password_text)
            {
                MessageBox.Show("Password does not match!");
                return;
            }

            DatabaseConnection.connectToSQL(out this.sql_conn, out this.connected, out output);

            if (!connected)
            {
                MessageBox.Show("Cannot Connect to SQL Server");
                this.Close();
                return;
            }

            
            DatabaseConnection.SignInUser(this.sql_conn, email_address, password_text, first_name, middle_name, last_name);

            MessageBox.Show($"Congratulations {first_name} you have registered your account");
            //MessageBox.Show($"You have succesfully registered {email_address}\n{search_comm}");
            DatabaseConnection.disconnectSQL(this.sql_conn, out this.connected);
            this.Close();
        }

        private void Back_to_login(object sender, RoutedEventArgs e)
        {
            this.sql_conn.Close();
            this.Close();
        }

        private void Reset_fields(object sender, RoutedEventArgs e)
        {
            FNBox.Text = "";
            MNBox.Text = "";
            LNBox.Text = "";
            EmailBox.Text = "";
            PassBox.Password = "";
            ConfirmPassBox.Password = "";
        }
    }
}
