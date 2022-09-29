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
    public partial class MedicalRecordWindow : Window
    {
        public MedicalRecordWindow()
        {
            InitializeComponent();
        }

        public void resetEntries()
        {
            FirstName.Text = "";
            LastName.Text = "";
            EmployeeID.Text = "";
            Male.IsChecked = false;
            Female.IsChecked = false;
            Asthma.IsChecked = false;
            Cancer.IsChecked = false;
            Cardiac.IsChecked = false;
            Diabetes.IsChecked = false;
            Hypertension.IsChecked = false;
            Psychiatric.IsChecked = false;
            Epilepsy.IsChecked = false;
            ChestPain7.IsChecked = false;
            Respiratory7.IsChecked = false;
            CardiacDisease7.IsChecked = false;
            Cardiovascular7.IsChecked = false;
            Hematological7.IsChecked = false;
            Lymphatic7.IsChecked = false;
            WeightGain7.IsChecked = false;
            WeightLoss7.IsChecked = false;
            isTakingMeds.IsChecked = false;
            notTakingMeds.IsChecked = false;
            hasMedAllergies.IsChecked = false;
            noMedAllergies.IsChecked = false;
            unsureMedAllergies.IsChecked = false;
            hasUsedTobacco.IsChecked = false;
            notUsedTobacco.IsChecked = false;
            hasUsedIllegalDrugs.IsChecked = false;
            hasNotUsedIllegalDrugs.IsChecked = false;
            alcoholDaily.IsChecked = false;
            alcoholWeekly.IsChecked = false;
            alcoholOccasionally.IsChecked = false;
            alcoholMonthly.IsChecked = false;
            alcoholNever.IsChecked = false;

        }
        private void ResetFieldsButton_Click(object sender, RoutedEventArgs e)
        {
            resetEntries(); 
        }
        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {           

            try
            {
                EmployeeHealthRecord EHR = new EmployeeHealthRecord();
                EHR.FirstName = FirstName.Text;
                EHR.LastName = LastName.Text;
                EHR.EmployeeID = Convert.ToInt32(EmployeeID.Text);

                EHR.HasAsthma = (bool)Asthma.IsChecked;
                EHR.HasCancer = (bool)Cancer.IsChecked;
                EHR.HasCardiacDisease = (bool)Cardiac.IsChecked;
                EHR.HasDiabetes = (bool)Diabetes.IsChecked;
                EHR.HasHypertension = (bool)Hypertension.IsChecked;
                EHR.HasPsychiatricDisorder = (bool)Psychiatric.IsChecked;
                EHR.HasEpilepsy = (bool)Epilepsy.IsChecked;

                EHR.HasChestPain7 = (bool)ChestPain7.IsChecked;
                EHR.HasRespiratory7 = (bool)Respiratory7.IsChecked;
                EHR.HasCardiacDisease7 = (bool)CardiacDisease7.IsChecked;
                EHR.HasCardiovascular7 = (bool)Cardiovascular7.IsChecked;
                EHR.HasHematological7 = (bool)Hematological7.IsChecked;
                EHR.HasLymphatic7 = (bool)Lymphatic7.IsChecked;
                EHR.HasWeightGain7 = (bool)WeightGain7.IsChecked;
                EHR.HasWeightLoss7 = (bool)WeightLoss7.IsChecked;

                EHR.IsTakingMeds = (bool)isTakingMeds.IsChecked;
                EHR.HasMedicationAllergies = (bool)hasMedAllergies.IsChecked;
                EHR.UsedTabacco = (bool)hasUsedTobacco.IsChecked;
                EHR.UsedIllegalDrugs = (bool)hasUsedIllegalDrugs.IsChecked;

                if ((bool)Male.IsChecked)
                {
                    EHR.Gender = 1;
                }
                else
                {
                    EHR.Gender = 2;
                }

                if ((bool)alcoholDaily.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Daily";
                }
                else if ((bool)alcoholWeekly.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Weekly";
                }
                else if ((bool)alcoholOccasionally.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Ocassionally";
                }
                else if ((bool)alcoholMonthly.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Monthly";
                }
                else if ((bool)alcoholNever.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Never";
                }
                EmployeeModule.addNewEmployeeHealthRecord(EHR);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }
            
            this.Close();
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                EmployeeHealthRecord EHR = new EmployeeHealthRecord();
                EHR.FirstName = FirstName.Text;
                EHR.LastName = LastName.Text;
                EHR.EmployeeID = Convert.ToInt32(EmployeeID.Text);

                EHR.HasAsthma = (bool)Asthma.IsChecked;
                EHR.HasCancer = (bool)Cancer.IsChecked;
                EHR.HasCardiacDisease = (bool)Cardiac.IsChecked;
                EHR.HasDiabetes = (bool)Diabetes.IsChecked;
                EHR.HasHypertension = (bool)Hypertension.IsChecked;
                EHR.HasPsychiatricDisorder = (bool)Psychiatric.IsChecked;
                EHR.HasEpilepsy = (bool)Epilepsy.IsChecked;

                EHR.HasChestPain7 = (bool)ChestPain7.IsChecked;
                EHR.HasRespiratory7 = (bool)Respiratory7.IsChecked;
                EHR.HasCardiacDisease7 = (bool)CardiacDisease7.IsChecked;
                EHR.HasCardiovascular7 = (bool)Cardiovascular7.IsChecked;
                EHR.HasHematological7 = (bool)Hematological7.IsChecked;
                EHR.HasLymphatic7 = (bool)Lymphatic7.IsChecked;
                EHR.HasWeightGain7 = (bool)WeightGain7.IsChecked;
                EHR.HasWeightLoss7 = (bool)WeightLoss7.IsChecked;

                EHR.IsTakingMeds = (bool)isTakingMeds.IsChecked;
                EHR.HasMedicationAllergies = (bool)hasMedAllergies.IsChecked;
                EHR.UsedTabacco = (bool)hasUsedTobacco.IsChecked;
                EHR.UsedIllegalDrugs = (bool)hasUsedIllegalDrugs.IsChecked;

                if ((bool)Male.IsChecked)
                {
                    EHR.Gender = 1;
                }
                else
                {
                    EHR.Gender = 2;
                }

                if ((bool)alcoholDaily.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Daily";
                }
                else if ((bool)alcoholWeekly.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Weekly";
                }
                else if ((bool)alcoholOccasionally.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Ocassionally";
                }
                else if ((bool)alcoholMonthly.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Monthly";
                }
                else if ((bool)alcoholNever.IsChecked)
                {
                    EHR.FrequencyAlcoholConsumption = "Never";
                }


                EmployeeModule.modifyEmployeeHealthRecord(EHR);
            }
            catch (Exception E)
            {
                MessageBox.Show($"An Error has occurred\n{E.Message}\n, please check the entered values");
            }
            this.Close();
        }
    }
}
