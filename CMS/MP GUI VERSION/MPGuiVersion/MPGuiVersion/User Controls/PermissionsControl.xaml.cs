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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace MPGuiVersion.User_Controls
{
    /// <summary>
    /// Interaction logic for PermissionsControl.xaml
    /// </summary>
    public partial class PermissionsControl : UserControl
    {
        SqlConnection conn;
        public PermissionsControl()
        {
            InitializeComponent();
            getDataFromServer();
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView PermissionsData = DatabaseConnection.getDatas(this.conn, "Permissions");
            PermissionList.ItemsSource = null;
            PermissionList.ItemsSource = PermissionsData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void resetEntries()
        {
            EmailAddress.Text = "";
            Password.Password = "";
            EmployeeModuleCB.IsChecked = false;
            InventoryModule.IsChecked = false;
            BookkeepingModule.IsChecked = false;
            TaskModule.IsChecked = false;
            Permissions.IsChecked = false;
        }

        private void UpdateInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            getDataFromServer();

            

            try
            {
                EmployeePermissions EPerm = new EmployeePermissions();
                EPerm.email = EmailAddress.Text;
                EPerm.password = Password.Password;
                EPerm.accessBookkeeping = (bool)BookkeepingModule.IsChecked;
                EPerm.accessEmployees = (bool)EmployeeModuleCB.IsChecked;
                EPerm.accessTasks = (bool)TaskModule.IsChecked;
                EPerm.accessInventory = (bool)InventoryModule.IsChecked;
                EPerm.accessPermissions = (bool)Permissions.IsChecked;
                EmployeeModule.modifyPermissions(EPerm);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }
            getDataFromServer();

            resetEntries();
        }

        private void GetDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            getDataFromServer();

            string email = EmailAddress.Text;
            string pass = Password.Password;
            bool found = false;

            if (PermissionList.SelectedItems.Count ==  0) {
                foreach (DataRowView row in PermissionList.ItemsSource)
                {
                    var email_str = row["Email"].ToString();
                    var pass_str = row["Password"].ToString();

                    bool check = email == email_str && pass == pass_str;
                    if (check)
                    {
                        PermissionList.SelectedItem = row;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    MessageBox.Show("Email and Password Details Not Found");              }
            }                       

            DataRowView selected = (DataRowView)PermissionList.SelectedItem;

            EmployeeModuleCB.IsChecked = Convert.ToBoolean(selected["EmployeeModule"].ToString());
            InventoryModule.IsChecked = Convert.ToBoolean(selected["InventoryModule"].ToString());
            BookkeepingModule.IsChecked = Convert.ToBoolean(selected["BookkeepingModule"].ToString());
            TaskModule.IsChecked = Convert.ToBoolean(selected["TaskModule"].ToString());
            Permissions.IsChecked = Convert.ToBoolean(selected["Permissions"].ToString());


        }
    }
}
