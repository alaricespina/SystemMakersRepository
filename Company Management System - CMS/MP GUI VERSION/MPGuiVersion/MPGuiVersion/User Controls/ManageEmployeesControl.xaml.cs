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
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageEmployeesControl : UserControl
    {
        SqlConnection conn;
        public ManageEmployeesControl()
        {
            InitializeComponent();
            getDataFromServer();
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView EmployeeData = DatabaseConnection.getDatas(this.conn, "Employees");
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = EmployeeData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void ManageEmailsBtn_Click(object sender, RoutedEventArgs e)
        {
            EmailWindow EW = new EmailWindow();
            EW.Show();
        }

        private void DeleteEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selected = (DataRowView)EmployeeList.SelectedItem;
                int target_id = Convert.ToInt32(selected["employeeID"].ToString());
                EmployeeModule.deleteEmployee(target_id);
            }
            catch (Exception E)
            {
                MessageBox.Show($"An Error has occurred\n{E.Message}\n, please check if you have selected an employee");
            }
            

            getDataFromServer();
            //MessageBox.Show($"{target_id}");

   
        }

        private void ManageHealthRecordsBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageEmployeeHealthRecordsWindow MEHRW = new ManageEmployeeHealthRecordsWindow();
            MEHRW.Show();
        }

        private void ManageProfilesBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageEmployeeProfilesWindow MEPW = new ManageEmployeeProfilesWindow();
            MEPW.Show();
        }

        private void SearchEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            int EID = Convert.ToInt32(EID_TextBox.Text);
            bool found = false;

            foreach (DataRowView row in EmployeeList.ItemsSource)
            {
                var str = row["employeeID"].ToString();
                bool check = (EID == Convert.ToInt32(str));
                if (check)
                {
                    EmployeeList.SelectedItem = row;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Employee ID Not Found");
            }
        }
    }

    
}
