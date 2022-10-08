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

    public partial class MainMenu : Window
    {
        SqlConnection conn;
        bool connected;
        string connection_message;
        public MainMenu()
        {
            InitializeComponent();
            DatabaseConnection.connectToSQL(out this.conn, out this.connected, out this.connection_message);
        }

        private void EmployeeModuleBtn_Click(object sender, RoutedEventArgs e)
        {

            bool HasPermission = DatabaseConnection.checkPrivilege(this.conn, "EmployeeModule");
            if (!HasPermission)
            {
                MessageBox.Show("You do not have permission to View the Employee Module");
                return;
            }

            Reset_Button_Pannel();

            EmployeeModuleBtn.Height = 100;
            AddEmployees.Visibility = Visibility.Visible;
            ManageEmployees.Visibility = Visibility.Visible;
            PayrollSheet.Visibility = Visibility.Visible;
        }

        private void BookkeepingModuleBtn_Click(object sender, RoutedEventArgs e)
        {
            bool HasPermission = DatabaseConnection.checkPrivilege(this.conn, "BookkeepingModule");
            if (!HasPermission)
            {
                MessageBox.Show("You do not have permission to View the Bookkeeping Module");
                return;
            }

            Reset_Button_Pannel();

            BookkeepingModuleBtn.Height = 100;
            AddTransaction.Visibility = Visibility.Visible;
            ManageTransaction.Visibility = Visibility.Visible;
        }

        private void InventoryModuleBtn_Click(object sender, RoutedEventArgs e)
        {
            bool HasPermission = DatabaseConnection.checkPrivilege(this.conn, "InventoryModule");
            if (!HasPermission)
            {
                MessageBox.Show("You do not have permission to View the Inventory Module");
                return;
            }

            Reset_Button_Pannel();

            InventoryModuleBtn.Height = 100;
            AddItems.Visibility = Visibility.Visible;
            ManageItems.Visibility = Visibility.Visible;
            ItemSummary.Visibility = Visibility.Visible;
        }

        private void TaskModuleBtn_Click(object sender, RoutedEventArgs e)
        {
            bool HasPermission = DatabaseConnection.checkPrivilege(this.conn, "TaskModule");
            if (!HasPermission)
            {
                MessageBox.Show("You do not have permission to View the Task Module");
                return;
            }
            Reset_Button_Pannel();

            TaskModuleBtn.Height = 100;
            AddTask.Visibility = Visibility.Visible;
            ManageTask.Visibility = Visibility.Visible;
            TaskSummary.Visibility = Visibility.Visible;
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Reset_Button_Pannel();
            this.Hide();
            LoginWindow LW = new LoginWindow();
            this.Close();
            LW.Show();           

        }

        private void PermissionsBtn_Click(object sender, RoutedEventArgs e)
        {
            bool HasPermission = DatabaseConnection.checkPrivilege(this.conn, "Permissions");
            if (!HasPermission)
            {
                MessageBox.Show("You do not have permission to check the permissions tab");
                return;
            }

            Reset_Button_Pannel();
            var PC = new User_Controls.PermissionsControl();
            CMSModules.Content = PC;
        }

        private void Reset_Button_Pannel()
        {
            int ButtonDefaultHeight = 60;

            // Employee Module
            AddEmployees.Visibility = Visibility.Collapsed;
            ManageEmployees.Visibility = Visibility.Collapsed;
            PayrollSheet.Visibility = Visibility.Collapsed;

            // Bookkeeping Module
            AddTransaction.Visibility = Visibility.Collapsed;
            ManageTransaction.Visibility = Visibility.Collapsed;

            // Inventory Module
            AddItems.Visibility = Visibility.Collapsed;
            ManageItems.Visibility = Visibility.Collapsed;
            ItemSummary.Visibility = Visibility.Collapsed;

            // Task Module
            AddTask.Visibility = Visibility.Collapsed;
            ManageTask.Visibility = Visibility.Collapsed;
            TaskSummary.Visibility = Visibility.Collapsed;

            EmployeeModuleBtn.Height = ButtonDefaultHeight;
            BookkeepingModuleBtn.Height = ButtonDefaultHeight;
            InventoryModuleBtn.Height = ButtonDefaultHeight;
            TaskModuleBtn.Height = ButtonDefaultHeight;
        }

        private void AddEmployees_Click(object sender, RoutedEventArgs e)
        {
            var AEC = new User_Controls.AddEmployeeControl();
            CMSModules.Content = null;
            CMSModules.Content = AEC;
        }

        private void ManageEmployees_Click(object sender, RoutedEventArgs e)
        {
            var MEC = new User_Controls.ManageEmployeesControl();
            CMSModules.Content = null;
            CMSModules.Content = MEC;
        }

        private void PayrollSheet_Click(object sender, RoutedEventArgs e)
        {
            var PSC = new User_Controls.PayrollSheetControl();
            CMSModules.Content = PSC;
        }

        private void AddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var ATC = new User_Controls.AddTransactionControl();
            CMSModules.Content = ATC;
        }

        private void ManageTransaction_Click(object sender, RoutedEventArgs e)
        {
            var MTC = new User_Controls.ManageTransactionsControl();
            CMSModules.Content = MTC;
        }

        private void AddItems_Click(object sender, RoutedEventArgs e)
        {
            var AIC = new User_Controls.AddItemsControl();
            CMSModules.Content = AIC;
        }

        private void ManageItems_Click(object sender, RoutedEventArgs e)
        {
            var MIC = new User_Controls.ManageItemsControl();
            CMSModules.Content = MIC;
        }

        private void ItemSummary_Click(object sender, RoutedEventArgs e)
        {
            var ISC = new User_Controls.ItemSummaryControl();
            CMSModules.Content = ISC;
        }

        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            var ATC = new User_Controls.AddTaskControl();
            CMSModules.Content = ATC;
        }

        private void ManageTask_Click(object sender, RoutedEventArgs e)
        {
            var MTC = new User_Controls.ManageTaskControl();
            CMSModules.Content = MTC;
        }

        private void TaskSummary_Click(object sender, RoutedEventArgs e)
        {
            var TSC = new User_Controls.TaskSummaryControl();
            CMSModules.Content = TSC;
        }

    }
}
