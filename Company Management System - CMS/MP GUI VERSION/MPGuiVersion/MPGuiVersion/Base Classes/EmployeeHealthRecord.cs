using System;
using System.Collections.Generic;
using System.Text;

namespace MPGuiVersion
{
    public class EmployeeHealthRecord
    {
        private int employeeID;
        private string firstName;
        private string lastName;
        private int gender;
        private string frequencyAlcoholConsumption;
        private bool hasAsthma;
        private bool hasCancer;
        private bool hasCardiacDisease;
        private bool hasDiabetes;
        private bool hasHypertension;
        private bool hasPsychiatricDisorder;
        private bool hasEpilepsy;
        private bool hasChestPain7;
        private bool hasRespiratory7;
        private bool hasCardiacDisease7;
        private bool hasCardiovascular7;
        private bool hasHematological7;
        private bool hasLymphatic7;
        private bool hasWeightGain7;
        private bool hasWeightLoss7;
        private bool isTakingMeds;
        private bool hasMedicationAllergies;
        private bool usedTabacco;
        private bool usedIllegalDrugs;
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public int Gender { get => gender; set => gender = value; }
        public string FrequencyAlcoholConsumption { get => frequencyAlcoholConsumption; set => frequencyAlcoholConsumption = value; }
        public bool HasAsthma { get => hasAsthma; set => hasAsthma = value; }
        public bool HasCancer { get => hasCancer; set => hasCancer = value; }
        public bool HasCardiacDisease { get => hasCardiacDisease; set => hasCardiacDisease = value; }
        public bool HasDiabetes { get => hasDiabetes; set => hasDiabetes = value; }
        public bool HasHypertension { get => hasHypertension; set => hasHypertension = value; }
        public bool HasPsychiatricDisorder { get => hasPsychiatricDisorder; set => hasPsychiatricDisorder = value; }
        public bool HasEpilepsy { get => hasEpilepsy; set => hasEpilepsy = value; }
        public bool HasChestPain7 { get => hasChestPain7; set => hasChestPain7 = value; }
        public bool HasRespiratory7 { get => hasRespiratory7; set => hasRespiratory7 = value; }
        public bool HasCardiacDisease7 { get => hasCardiacDisease7; set => hasCardiacDisease7 = value; }
        public bool HasCardiovascular7 { get => hasCardiovascular7; set => hasCardiovascular7 = value; }
        public bool HasHematological7 { get => hasHematological7; set => hasHematological7 = value; }
        public bool HasLymphatic7 { get => hasLymphatic7; set => hasLymphatic7 = value; }
        public bool HasWeightGain7 { get => hasWeightGain7; set => hasWeightGain7 = value; }
        public bool HasWeightLoss7 { get => hasWeightLoss7; set => hasWeightLoss7 = value; }
        public bool IsTakingMeds { get => isTakingMeds; set => isTakingMeds = value; }
        public bool HasMedicationAllergies { get => hasMedicationAllergies; set => hasMedicationAllergies = value; }
        public bool UsedTabacco { get => usedTabacco; set => usedTabacco = value; }
        public bool UsedIllegalDrugs { get => usedIllegalDrugs; set => usedIllegalDrugs = value; }
        public int EmployeeID { get => employeeID; set => employeeID = value; }
    }
}
