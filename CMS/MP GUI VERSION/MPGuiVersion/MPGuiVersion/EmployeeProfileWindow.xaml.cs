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

namespace MPGuiVersion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EmployeeProfileWindow : Window
    {
        public EmployeeProfileWindow()
        {
            InitializeComponent();
        }

        private void resetEntries()
        {
            EmployeeID.Text = "";
            FirstName.Text = "";
            MiddleName.Text = "";
            LastName.Text = "";
            Suffix.Text = "";
            DateofBirth.SelectedDate = DateTime.Now;

            EmailAddress.Text = "";
            MobileNum.Text = "";
            Landline.Text = "";
            MotherNumber.Text = "";
            FatherNumber.Text = "";

            BuildingStreet.Text = "";
            CityState.Text = "";
            CountryZipCode.Text = "";

            RBMale.IsChecked = false;
            RBFemale.IsChecked = false;

            RBDivorced.IsChecked = false;
            RBMarried.IsChecked = false;
            RBSingle.IsChecked = false;
            RBWidow.IsChecked = false;

            ec1FirstName.Text = "";
            ec1MiddleName.Text = "";
            ec1LastName.Text = "";
            ec1ContactNumber.Text = "";

            ec2FirstName.Text = "";
            ec2MiddleName.Text = "";
            ec2LastName.Text = "";
            ec2ContactNumber.Text = "";

            ec3FirstName.Text = "";
            ec3MiddleName.Text = "";
            ec3LastName.Text = "";
            ec3ContactNumber.Text = "";
        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            resetEntries();
        }

        private void AddProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeeProfile EP = new EmployeeProfile();
            EP.EmployeeID = Convert.ToInt32(EmployeeID.Text);
            Name EN = new Name();
            EN.firstName = FirstName.Text;
            EN.middleName = MiddleName.Text;
            EN.lastName = LastName.Text;
            EN.suffix = Suffix.Text;

            EP.EmployeeName = EN;

            EP.DateOfBirth = DateofBirth.ToString();

            EP.EmailAddress = EmailAddress.Text;
            EP.MobileNum = Convert.ToInt64(MobileNum.Text);
            EP.Landline = Convert.ToInt64(Landline.Text);
            EP.MotherNum = Convert.ToInt64(MotherNumber.Text);
            EP.FatherNum = Convert.ToInt64(FatherNumber.Text);

            Address A = new Address();
            A.BuildingNumber = BuildingStreet.Text;
            A.CityState = CityState.Text;
            A.CountryZIP = CountryZipCode.Text;

            if ((bool)RBMale.IsChecked){
                EP.Sex = 1;
            } else if ((bool)RBFemale.IsChecked)
            {
                EP.Sex = 2;
            }

            if ((bool)RBSingle.IsChecked)
            {
                EP.MaritalStatus = 1;
            } else if ((bool)RBMarried.IsChecked)
            {
                EP.MaritalStatus = 2;
            } else if ((bool)RBDivorced.IsChecked)
            {
                EP.MaritalStatus = 3;
            } else if ((bool)RBWidow.IsChecked)
            {
                EP.MaritalStatus = 4;
            }

            ECList EmergencyList = new ECList();
            EmergencyContact c1 = new EmergencyContact();
            EmergencyContact c2 = new EmergencyContact();
            EmergencyContact c3 = new EmergencyContact();

            c1.firstName = ec1FirstName.Text;
            c1.middleName = ec1MiddleName.Text;
            c1.lastName = ec1LastName.Text;
            c1.contactnumber = Convert.ToInt64(ec1ContactNumber.Text);

            c2.firstName = ec2FirstName.Text;
            c2.middleName = ec2MiddleName.Text;
            c2.lastName = ec2LastName.Text;
            c2.contactnumber = Convert.ToInt64(ec2ContactNumber.Text);

            c3.firstName = ec3FirstName.Text;
            c3.middleName = ec3MiddleName.Text;
            c3.lastName = ec3LastName.Text;
            c3.contactnumber = Convert.ToInt64(ec3ContactNumber.Text);

            EmergencyList.EC1 = c1;
            EmergencyList.EC2 = c2;
            EmergencyList.EC3 = c3;

            EP.EmployeeContacts = EmergencyList;

            resetEntries();
            try
            {
                EmployeeModule.addNewEmployeeProfile(EP);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }
            
            this.Close();
        }

        private void ModifyProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeProfile EP = new EmployeeProfile();
                EP.EmployeeID = Convert.ToInt32(EmployeeID.Text);
                Name EN = new Name();
                EN.firstName = FirstName.Text;
                EN.middleName = MiddleName.Text;
                EN.lastName = LastName.Text;
                EN.suffix = Suffix.Text;

                EP.EmployeeName = EN;

                EP.DateOfBirth = DateofBirth.ToString();

                EP.EmailAddress = EmailAddress.Text;
                EP.MobileNum = Convert.ToInt64(MobileNum.Text);
                EP.Landline = Convert.ToInt64(Landline.Text);
                EP.MotherNum = Convert.ToInt64(MotherNumber.Text);
                EP.FatherNum = Convert.ToInt64(FatherNumber.Text);

                Address A = new Address();
                A.BuildingNumber = BuildingStreet.Text;
                A.CityState = CityState.Text;
                A.CountryZIP = CountryZipCode.Text;

                EP.Nationality = Nationality.Text;

                EP.Address = A;

                if ((bool)RBMale.IsChecked)
                {
                    EP.Sex = 1;
                }
                else if ((bool)RBFemale.IsChecked)
                {
                    EP.Sex = 2;
                }

                if ((bool)RBSingle.IsChecked)
                {
                    EP.MaritalStatus = 1;
                }
                else if ((bool)RBMarried.IsChecked)
                {
                    EP.MaritalStatus = 2;
                }
                else if ((bool)RBDivorced.IsChecked)
                {
                    EP.MaritalStatus = 3;
                }
                else if ((bool)RBWidow.IsChecked)
                {
                    EP.MaritalStatus = 4;
                }

                ECList EmergencyList = new ECList();
                EmergencyContact c1 = new EmergencyContact();
                EmergencyContact c2 = new EmergencyContact();
                EmergencyContact c3 = new EmergencyContact();

                c1.firstName = ec1FirstName.Text;
                c1.middleName = ec1MiddleName.Text;
                c1.lastName = ec1LastName.Text;
                c1.contactnumber = Convert.ToInt64(ec1ContactNumber.Text);

                c2.firstName = ec2FirstName.Text;
                c2.middleName = ec2MiddleName.Text;
                c2.lastName = ec2LastName.Text;
                if (ec2ContactNumber.Text.Length > 0)
                {
                    c2.contactnumber = Convert.ToInt64(ec2ContactNumber.Text);
                }
                else
                {
                    c2.contactnumber = 0;
                }


                c3.firstName = ec3FirstName.Text;
                c3.middleName = ec3MiddleName.Text;
                c3.lastName = ec3LastName.Text;
                if (ec3ContactNumber.Text.Length > 0)
                {
                    c3.contactnumber = Convert.ToInt64(ec3ContactNumber.Text);
                }
                else
                {
                    c3.contactnumber = 0;
                }

                EmergencyList.EC1 = c1;
                EmergencyList.EC2 = c2;
                EmergencyList.EC3 = c3;

                EP.EmployeeContacts = EmergencyList;

                resetEntries();
                EmployeeModule.modifyEmployeeProfile(EP);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }
            
            this.Close();
        }

    }
}
