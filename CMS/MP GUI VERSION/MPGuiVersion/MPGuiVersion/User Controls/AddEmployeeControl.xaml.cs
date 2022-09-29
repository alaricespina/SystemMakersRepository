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



namespace MPGuiVersion.User_Controls
{
    /// <summary>
    /// Interaction logic for AddEmployeeControl.xaml
    /// </summary>
    public partial class AddEmployeeControl : UserControl
    {
        public AddEmployeeControl()
        {
            InitializeComponent();
        }

        private void ClearAllEntries()
        {
            firstName.Text = "";
            middleName.Text = "";
            lastName.Text = "";
            suffix.Text = "";
            sex.Text = "";
            department.Text = "";
            position.Text = "";
            emailAddress.Text = "";
            salary.Text = "";
            employeeID.Text = "";
        }

        private void AddEmployeeProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeModule.addEmployeeProfile();
        }

        private void AddEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee ret = new Employee
                {
                    FirstName = firstName.Text,
                    MiddleName = middleName.Text,
                    LastName = lastName.Text,
                    Suffix = suffix.Text,
                    Sex = sex.Text,
                    Department = department.Text,
                    Position = position.Text,
                    EmailAddress = emailAddress.Text,
                    Salary = Convert.ToDouble(salary.Text),
                    EmployeeID = Convert.ToInt32(employeeID.Text)
                };
                EmployeeModule.addEmployee(ret);
            } catch
            {
                MessageBox.Show("An Error has occured, please check the values entered");
            }

                     

            ClearAllEntries();
        }

        private void AddHealthRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeModule.addHealthRecord();
        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearAllEntries();
        }
    }
}
