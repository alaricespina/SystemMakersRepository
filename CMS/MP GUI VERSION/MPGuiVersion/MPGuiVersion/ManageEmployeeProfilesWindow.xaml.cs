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
    /// Interaction logic for ManageEmployeeProfilesWindow.xaml
    /// </summary>
    public partial class ManageEmployeeProfilesWindow : Window
    {
        SqlConnection conn;

        public ManageEmployeeProfilesWindow()
        {
            InitializeComponent();
            getDataFromServer();
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView EmployeeData = DatabaseConnection.getDatas(this.conn, "Profiles");
            EmployeePList.ItemsSource = null;
            EmployeePList.ItemsSource = EmployeeData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void DeleteEmployeePBtn_Click(object sender, RoutedEventArgs e)
        {
           
                       
            try
            {
                DataRowView selected = (DataRowView)EmployeePList.SelectedItem;
                int target_id = Convert.ToInt32(selected["employeeprofID"].ToString());
                EmployeeModule.deleteEmployeeProfile(target_id);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check if you have selected a profile");
            }
            getDataFromServer();
        }

        private void AddEmployeePBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeProfileWindow EPW = new EmployeeProfileWindow();
            EPW.Show();

            getDataFromServer();
        }

        private void ModifyEmployeePBtn_Click(object sender, RoutedEventArgs e)
        {

            DataRowView selected = (DataRowView)EmployeePList.SelectedItem;
            int target_id = Convert.ToInt32(selected["employeeprofID"].ToString());

            EmployeeProfileWindow EPW = new EmployeeProfileWindow();
            EPW.EmployeeID.Text = selected["employeeprofID"].ToString();

            EPW.FirstName.Text = selected["firstName"].ToString();
            EPW.MiddleName.Text = selected["middleName"].ToString();
            EPW.LastName.Text = selected["lastName"].ToString();
            EPW.Suffix.Text = selected["suffix"].ToString();
            EPW.DateofBirth.SelectedDate = DateTime.Today;

            EPW.EmailAddress.Text = selected["emailAddress"].ToString();
            EPW.MobileNum.Text = selected["mobileNum"].ToString();
            EPW.Landline.Text = selected["landline"].ToString();
            EPW.MotherNumber.Text = selected["motherNum"].ToString();
            EPW.FatherNumber.Text = selected["fatherNum"].ToString();

            EPW.BuildingStreet.Text = selected["buildingNumber"].ToString();
            EPW.CityState.Text = selected["city"].ToString();
            EPW.CountryZipCode.Text = selected["zip"].ToString();

            if (selected["sex"].ToString() == "1"){
                EPW.RBMale.IsChecked = true;
                EPW.RBFemale.IsChecked = false;
            } else
            {
                EPW.RBFemale.IsChecked = true;
                EPW.RBMale.IsChecked = false;
            }
           
            switch (selected["maritalStatus"].ToString())
            {
                case "1":
                    EPW.RBSingle.IsChecked = true;
                    break;
                case "2":
                    EPW.RBMarried.IsChecked = true;
                    break;
                case "3":
                    EPW.RBDivorced.IsChecked = true;
                    break;
                case "4":
                    EPW.RBWidow.IsChecked = true;
                    break;
            }
                       

            EPW.ec1FirstName.Text = selected["ec1firstName"].ToString();
            EPW.ec1MiddleName.Text = selected["ec1middleName"].ToString();
            EPW.ec1LastName.Text = selected["ec1lastName"].ToString();
            EPW.ec1ContactNumber.Text = selected["ec1number"].ToString();

            EPW.ec2FirstName.Text = selected["ec2firstName"].ToString(); 
            EPW.ec2MiddleName.Text = selected["ec2middleName"].ToString(); 
            EPW.ec2LastName.Text = selected["ec2lastName"].ToString(); 
            EPW.ec2ContactNumber.Text = selected["ec2number"].ToString(); 

            EPW.ec3FirstName.Text = selected["ec3firstName"].ToString(); 
            EPW.ec3MiddleName.Text = selected["ec3middleName"].ToString(); 
            EPW.ec3LastName.Text = selected["ec3lastName"].ToString(); 
            EPW.ec3ContactNumber.Text = selected["ec3number"].ToString(); 
            

            EPW.AddProfileBtn.Visibility = Visibility.Collapsed;
            EPW.ModifyProfileBtn.Visibility = Visibility.Visible;

            getDataFromServer();

            EPW.Show();

            getDataFromServer();



        }

        private void SearchEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            int EID = Convert.ToInt32(EmployeeID.Text);
            bool found = false;

            foreach (DataRowView row in EmployeePList.ItemsSource)
            {
                var str = row["employeeprofID"].ToString();
                bool check = EID == Convert.ToInt32(str);
                if (check)
                {
                    EmployeePList.SelectedItem = row;
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
