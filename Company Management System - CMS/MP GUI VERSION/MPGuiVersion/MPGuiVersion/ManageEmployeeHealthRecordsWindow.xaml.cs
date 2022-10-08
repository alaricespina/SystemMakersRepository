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
    /// Interaction logic for ManageEmployeeHealthRecordsWindow.xaml
    /// </summary>
    public partial class ManageEmployeeHealthRecordsWindow : Window
    {
        SqlConnection conn;
        public ManageEmployeeHealthRecordsWindow()
        {

            InitializeComponent();
            getDataFromServer();
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView EmployeeData = DatabaseConnection.getDatas(this.conn, "Health Records");
            EmployeeHRList.ItemsSource = null;
            EmployeeHRList.ItemsSource = EmployeeData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void DeleteEmployeeHRBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DataRowView selected = (DataRowView)EmployeeHRList.SelectedItem;
                int target_id = Convert.ToInt32(selected["employeerecordID"].ToString());
                EmployeeModule.deleteEmployeeHealthRecord(target_id);
            }
            catch (Exception E)
            {
                MessageBox.Show($"An Error has occurred\n{E.Message}\n, please check if you have selected a health record");
            }
           

            getDataFromServer();
        }

        private void AddEmployeeHRBtn_Click(object sender, RoutedEventArgs e)
        {
            MedicalRecordWindow MRW = new MedicalRecordWindow();
            MRW.Show();

            getDataFromServer();
        }

        private void ModifyEmployeeHRBtn_Click(object sender, RoutedEventArgs e)
        {
            DataRowView selected = (DataRowView)EmployeeHRList.SelectedItem;
            MedicalRecordWindow MRW = new MedicalRecordWindow();

            MRW.EmployeeID.Text = selected["employeerecordID"].ToString();
            MRW.FirstName.Text = selected["firstName"].ToString();
            MRW.LastName.Text = selected["lastName"].ToString();
            MRW.Male.IsChecked = Convert.ToBoolean(selected["gender"].ToString());
            MRW.Female.IsChecked = selected["gender"].ToString() == "2";
            MRW.Asthma.IsChecked = Convert.ToBoolean(selected["hasAsthma"].ToString());
            MRW.Cancer.IsChecked = Convert.ToBoolean(selected["hasCancer"].ToString());
            MRW.Cardiac.IsChecked = Convert.ToBoolean(selected["hasCardiacDisease"].ToString());
            MRW.Diabetes.IsChecked = Convert.ToBoolean(selected["hasDiabetes"].ToString());
            MRW.Hypertension.IsChecked = Convert.ToBoolean(selected["hasHypertension"].ToString());
            MRW.Psychiatric.IsChecked = Convert.ToBoolean(selected["hasPsychiatricDisorder"].ToString());
            MRW.Epilepsy.IsChecked = Convert.ToBoolean(selected["hasEpilepsy"].ToString());
            MRW.ChestPain7.IsChecked = Convert.ToBoolean(selected["hasChestPain7"].ToString());
            MRW.Respiratory7.IsChecked = Convert.ToBoolean(selected["hasRespiratory7"].ToString());
            MRW.CardiacDisease7.IsChecked = Convert.ToBoolean(selected["hasCardiacDisease7"].ToString());
            MRW.Hematological7.IsChecked = Convert.ToBoolean(selected["hasHematological7"].ToString());
            MRW.Lymphatic7.IsChecked = Convert.ToBoolean(selected["hasLymphatic7"].ToString());
            MRW.WeightGain7.IsChecked = Convert.ToBoolean(selected["hasWeightGain7"].ToString());
            MRW.WeightLoss7.IsChecked = Convert.ToBoolean(selected["hasWeightLoss7"].ToString());

            MRW.isTakingMeds.IsChecked = Convert.ToBoolean(selected["isTakingMeds"].ToString());
            MRW.notTakingMeds.IsChecked = !Convert.ToBoolean(selected["isTakingMeds"].ToString());

            MRW.hasMedAllergies.IsChecked = Convert.ToBoolean(selected["hasAsthma"].ToString());
            MRW.noMedAllergies.IsChecked = Convert.ToBoolean(selected["hasAsthma"].ToString());
            MRW.unsureMedAllergies.IsChecked = Convert.ToBoolean(selected["hasAsthma"].ToString());

            MRW.hasUsedTobacco.IsChecked = Convert.ToBoolean(selected["usedTabacco"].ToString());
            MRW.notUsedTobacco.IsChecked = !Convert.ToBoolean(selected["usedTabacco"].ToString());

            MRW.hasUsedIllegalDrugs.IsChecked = Convert.ToBoolean(selected["usedIllegalDrugs"].ToString());
            MRW.hasNotUsedIllegalDrugs.IsChecked = !Convert.ToBoolean(selected["usedIllegalDrugs"].ToString());

            switch (selected["frequencyAlcoholConsumption"].ToString())
            {
                case "Daily":
                    MRW.alcoholDaily.IsChecked = true;
                    break;
                case "Weekly":
                    MRW.alcoholWeekly.IsChecked = true;
                    break;
                case "Occassionally":
                    MRW.alcoholOccasionally.IsChecked = true;
                    break;
                case "Monthly":
                    MRW.alcoholMonthly.IsChecked = true;
                    break;
                case "Never":
                    MRW.alcoholNever.IsChecked = true;
                    break;
            }

            MRW.SubmitButton.Visibility = Visibility.Collapsed;
            MRW.ModifyButton.Visibility = Visibility.Visible;

            getDataFromServer();

            MRW.Show();

            getDataFromServer();

        }

        private void SearchEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            int EID = Convert.ToInt32(EmployeeID.Text);
            bool found = false;

            foreach (DataRowView row in EmployeeHRList.ItemsSource)
            {
                var str = row["employeerecordID"].ToString();
                bool check = (EID == Convert.ToInt32(str));
                if (check)
                {
                    EmployeeHRList.SelectedItem = row;
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
