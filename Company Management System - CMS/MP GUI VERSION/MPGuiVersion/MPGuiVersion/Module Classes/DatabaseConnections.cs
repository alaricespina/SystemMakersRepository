using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

using System.Data;
using System.Data.SqlClient;

namespace MPGuiVersion
{
    public static class DatabaseConnection
    {
        public static string ComputerName = "";
        public static string DatabaseName = "";
        public static void connectToSQL(out SqlConnection sql_conn, out bool connected, out string message)
        {
            // Try - Except Block, just in case SQL Connection Fails
            try
            {
                //ComputerName = "ASUS-ACE";
                //DatabaseName = "CMSData";

                // Connection String based on Server Connector by Visual Studio
                string connection_string = $"Data Source={ComputerName};Initial Catalog={DatabaseName};Integrated Security=True";

                // Creates new Connection Object based on connection string
                sql_conn = new SqlConnection(connection_string);

                // Opens the Connection
                sql_conn.Open();

                //MessageBox.Show("Connection with the CMSData was succesfully established");
                connected = true; // Used for testing purposes
                message = "Connected";
            }
            catch (Exception ex)
            {
                // Connection Failed
                sql_conn = null;
                message = $"An Exception has occurred: \n{ex.Message}";
                connected = false;

            }

        }

        public static void disconnectSQL(SqlConnection sql_conn, out bool closed)
        {
            try
            {
                sql_conn.Close();
                closed = true;
            }                
            catch 
            {
                closed = false;
            }
        }

        public static DataView getDatas(SqlConnection conn, string type)
        {
            SqlCommand SP_getCommand = null;

            switch(type){
                case "Employees":
                    SP_getCommand = new SqlCommand("getAllEmployees", conn);
                    break;
                case "Items":
                    SP_getCommand = new SqlCommand("getAllItems", conn);
                    break;
                case "Tasks":
                    SP_getCommand = new SqlCommand("getAllTasks", conn);
                    break;
                case "Transactions":
                    SP_getCommand = new SqlCommand("getAllTransactions", conn);
                    break;
                case "Health Records":
                    SP_getCommand = new SqlCommand("getAllEmployeeHealthRecords", conn);
                    break;
                case "Profiles":
                    SP_getCommand = new SqlCommand("getAllEmployeeProfiles", conn);
                    break;
                case "Permissions":
                    SP_getCommand = new SqlCommand("getAllPrivilege", conn);
                    break;
                default:
                    break;
            }

            SP_getCommand.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter SDA = new SqlDataAdapter(SP_getCommand);
            DataTable DT = new DataTable();
            SDA.Fill(DT);
            return DT.DefaultView;

        }

        public static void modifyEmployeePermissions(SqlConnection conn, EmployeePermissions EP)
        {
            SqlCommand SP_ModifyPrivilege = new SqlCommand("modifyUserPrivilege", conn);
            SP_ModifyPrivilege.CommandType = CommandType.StoredProcedure;
            SP_ModifyPrivilege.Parameters.AddWithValue("@BookkeepingModule", EP.accessBookkeeping);
            SP_ModifyPrivilege.Parameters.AddWithValue("@EmployeeModule", EP.accessEmployees);
            SP_ModifyPrivilege.Parameters.AddWithValue("@TaskModule", EP.accessTasks);
            SP_ModifyPrivilege.Parameters.AddWithValue("@InventoryModule", EP.accessInventory);
            SP_ModifyPrivilege.Parameters.AddWithValue("@Permissions", EP.accessPermissions);
            SP_ModifyPrivilege.Parameters.AddWithValue("@email", EP.email);
            SP_ModifyPrivilege.Parameters.AddWithValue("@password", EP.password);

            SP_ModifyPrivilege.ExecuteNonQuery();
        }

        public static void deleteEmployee(SqlConnection conn, int EID)
        {
            SqlCommand SP_deleteEmployee = new SqlCommand("deleteEmployee", conn);
            SP_deleteEmployee.CommandType = CommandType.StoredProcedure;

            SP_deleteEmployee.Parameters.AddWithValue("@EmployeeID", EID);
            SP_deleteEmployee.ExecuteNonQuery();
        }
        public static void addEmployees(SqlConnection conn, Employee E)
        {
            //EXECUTE [createEmployee] 1, "Rence Joseph", "Romero", "Cadsawan", "", 1, "Software", "Senior Dev", "rjrcadsawan@mymail.mapua.edu.ph", "200000"
            //@EmployeeID @FirstName @MiddleName @LastName @Suffix @Sex @Department @Position @EmailAddress @Salary

 
            SqlCommand SP_addemployees = new SqlCommand("createEmployee", conn);
            SP_addemployees.CommandType = CommandType.StoredProcedure;

            SP_addemployees.Parameters.AddWithValue("@employeeID", E.EmployeeID);
            SP_addemployees.Parameters.AddWithValue("@FirstName", E.FirstName);
            SP_addemployees.Parameters.AddWithValue("@MiddleName", E.MiddleName);
            SP_addemployees.Parameters.AddWithValue("@LastName", E.LastName);
            SP_addemployees.Parameters.AddWithValue("@Suffix", E.Suffix);

            int gender = 0;

            if (E.Sex == "Male"){
                gender = 1;
            } else if (E.Sex == "Female")
            {
                gender = 2;
            }

            SP_addemployees.Parameters.AddWithValue("@Sex", gender);
            SP_addemployees.Parameters.AddWithValue("@Department", E.Department);
            SP_addemployees.Parameters.AddWithValue("@Position", E.Position);
            SP_addemployees.Parameters.AddWithValue("@EmailAddress", E.EmailAddress);
            SP_addemployees.Parameters.AddWithValue("@Salary", E.Salary);
            SP_addemployees.ExecuteNonQuery();

        }

        public static void addItem(SqlConnection conn, Item I)
        {
            SqlCommand SP_addItem = new SqlCommand("createItems", conn);
            SP_addItem.CommandType = CommandType.StoredProcedure;

            SP_addItem.Parameters.AddWithValue("@itemID", I.ItemID);
            SP_addItem.Parameters.AddWithValue("@itemName", I.ItemName);
            SP_addItem.Parameters.AddWithValue("@quantity", I.Quantity);
            SP_addItem.Parameters.AddWithValue("@unitPrice", I.UnitPrice);
            SP_addItem.Parameters.AddWithValue("@totalPrice", I.TotalPrice);
            SP_addItem.Parameters.AddWithValue("@Description", I.Description);

            SP_addItem.ExecuteNonQuery();
        }

        public static void modifyItem(SqlConnection conn, Item I)
        {
            SqlCommand SP_modifyItem = new SqlCommand("modifyItems", conn);
            SP_modifyItem.CommandType = CommandType.StoredProcedure;

            SP_modifyItem.Parameters.AddWithValue("@itemID", I.ItemID);
            SP_modifyItem.Parameters.AddWithValue("@itemName", I.ItemName);
            SP_modifyItem.Parameters.AddWithValue("@quantity", I.Quantity);
            SP_modifyItem.Parameters.AddWithValue("@unitPrice", I.UnitPrice);
            SP_modifyItem.Parameters.AddWithValue("@totalPrice", I.TotalPrice);
            SP_modifyItem.Parameters.AddWithValue("@description", I.Description);

            SP_modifyItem.ExecuteNonQuery();
        }

        public static void addTransaction(SqlConnection conn, Transaction T)
        {
            SqlCommand SP_addTransaction = new SqlCommand("createTransaction", conn);
            SP_addTransaction.CommandType = CommandType.StoredProcedure;

            SP_addTransaction.Parameters.AddWithValue("@TransactionID", T.TransactionID);
            SP_addTransaction.Parameters.AddWithValue("@fullName", T.Name);
            SP_addTransaction.Parameters.AddWithValue("@isCredit", T.TransactionC);
            SP_addTransaction.Parameters.AddWithValue("@isDebit", T.TransactionD);
            SP_addTransaction.Parameters.AddWithValue("@amount", T.Amount);
            SP_addTransaction.Parameters.AddWithValue("@Description", T.Summary);

            SP_addTransaction.ExecuteNonQuery();
        }

        public static void deleteTransaction(SqlConnection conn, int T)
        {
            SqlCommand SP_deleteTransaction = new SqlCommand("deleteTransaction", conn);
            SP_deleteTransaction.CommandType = CommandType.StoredProcedure;

            SP_deleteTransaction.Parameters.AddWithValue("@TransactionID", T);
            SP_deleteTransaction.ExecuteNonQuery();
        }

        public static void deleteTask(SqlConnection conn, int TID)
        {
            SqlCommand SP_deleteTask = new SqlCommand("deleteTask", conn);
            SP_deleteTask.CommandType = CommandType.StoredProcedure;

            SP_deleteTask.Parameters.AddWithValue("@TaskID", TID);
            SP_deleteTask.ExecuteNonQuery();
        }


        public static void addTask(SqlConnection conn, Task T)
        {
            SqlCommand SP_addTask = new SqlCommand("createTask", conn);
            SP_addTask.CommandType = CommandType.StoredProcedure;

            SP_addTask.Parameters.AddWithValue("@taskID", T.TaskID);
            SP_addTask.Parameters.AddWithValue("@taskName", T.TaskName);
            SP_addTask.Parameters.AddWithValue("@dueDate", T.DueDate);
            SP_addTask.Parameters.AddWithValue("@isDone", T.IsDone);
            SP_addTask.Parameters.AddWithValue("@budget", T.Budget);
            SP_addTask.Parameters.AddWithValue("@employeesNeeded", T.EmployeesNeeded);
            SP_addTask.Parameters.AddWithValue("@materialsNeeded", T.MaterialsNeeded);
            SP_addTask.Parameters.AddWithValue("@description", T.Description);
            

            SP_addTask.ExecuteNonQuery();
        }

        public static void deleteItem(SqlConnection conn, int target_id)
        {
            SqlCommand SP_deleteItem = new SqlCommand("deleteItem", conn);
            SP_deleteItem.CommandType = CommandType.StoredProcedure;

            SP_deleteItem.Parameters.AddWithValue("@ItemID", target_id);
            SP_deleteItem.ExecuteNonQuery();
        }

        public static void modifyTask(SqlConnection conn, Task T)
        {
            SqlCommand SP_modifyTask = new SqlCommand("modifyTask", conn);
            SP_modifyTask.CommandType = CommandType.StoredProcedure;

            SP_modifyTask.Parameters.AddWithValue("@taskID", T.TaskID);
            SP_modifyTask.Parameters.AddWithValue("@taskName", T.TaskName);
            SP_modifyTask.Parameters.AddWithValue("@dueDate", T.DueDate);
            SP_modifyTask.Parameters.AddWithValue("@isDone", T.IsDone);
            SP_modifyTask.Parameters.AddWithValue("@budget", T.Budget);
            SP_modifyTask.Parameters.AddWithValue("@employeesNeeded", T.EmployeesNeeded);
            SP_modifyTask.Parameters.AddWithValue("@materialsNeeded", T.MaterialsNeeded);
            SP_modifyTask.Parameters.AddWithValue("@description", T.Description);
            SP_modifyTask.Parameters.AddWithValue("@employeesAssigned", T.AssignedEmployees);

            SP_modifyTask.ExecuteNonQuery();
        }

        public static void assignEmployees(SqlConnection conn, Task T)
        {
            SqlCommand SP_assignEmployees = new SqlCommand("assignEmployeeOnTask", conn);
            SP_assignEmployees.CommandType = CommandType.StoredProcedure;

            SP_assignEmployees.Parameters.AddWithValue("@taskID", T.TaskID);
            SP_assignEmployees.Parameters.AddWithValue("@employeesAssigned", T.AssignedEmployees);
            SP_assignEmployees.ExecuteNonQuery();
        }

        public static void deleteEmployeeHealthRecord(SqlConnection conn, int target_id)
        {
            SqlCommand SP_deleteEmployeeHealthRecord = new SqlCommand("deleteEmployeeHealthRecord", conn);
            SP_deleteEmployeeHealthRecord.CommandType = CommandType.StoredProcedure;

            SP_deleteEmployeeHealthRecord.Parameters.AddWithValue("@employeerecordID", target_id);
            SP_deleteEmployeeHealthRecord.ExecuteNonQuery();
        }

        public static void deleteEmployeeProfile(SqlConnection conn, int target_id)
        {
            SqlCommand SP_deleteEmployeeProfile = new SqlCommand("deleteEmployeeProfile", conn);
            SP_deleteEmployeeProfile.CommandType = CommandType.StoredProcedure;

            SP_deleteEmployeeProfile.Parameters.AddWithValue("@employeeprofID", target_id);
            SP_deleteEmployeeProfile.ExecuteNonQuery();
        }

        public static void addEmployeeProfile(SqlConnection conn, EmployeeProfile EP)
        {
            SqlCommand SP_addEmployeeProfile = new SqlCommand("createEmployeeProfile", conn);
            SP_addEmployeeProfile.CommandType = CommandType.StoredProcedure;

            SP_addEmployeeProfile.Parameters.AddWithValue("@employeeprofID", EP.EmployeeID);

            Name EmployeeName = EP.EmployeeName;
            SP_addEmployeeProfile.Parameters.AddWithValue("@FirstName", EmployeeName.firstName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@MiddleName", EmployeeName.middleName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@LastName", EmployeeName.lastName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Suffix", EmployeeName.suffix);

            SP_addEmployeeProfile.Parameters.AddWithValue("@DateOfBirth", EP.DateOfBirth);

            Address EmployeeAddress = EP.Address;
            SP_addEmployeeProfile.Parameters.AddWithValue("@BuildingNumber", EmployeeAddress.BuildingNumber);
            SP_addEmployeeProfile.Parameters.AddWithValue("@City", EmployeeAddress.CityState);
            SP_addEmployeeProfile.Parameters.AddWithValue("@ZIP", EmployeeAddress.CountryZIP);

            SP_addEmployeeProfile.Parameters.AddWithValue("@MobileNum", EP.MobileNum);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Landline", EP.Landline);
            SP_addEmployeeProfile.Parameters.AddWithValue("@FatherNum", EP.FatherNum);
            SP_addEmployeeProfile.Parameters.AddWithValue("@MotherNum", EP.MotherNum);
            SP_addEmployeeProfile.Parameters.AddWithValue("@EmailAddress", EP.EmailAddress);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Sex", EP.Sex);
            SP_addEmployeeProfile.Parameters.AddWithValue("@MaritalStatus", EP.MaritalStatus);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Nationality", EP.Nationality);

            ECList EmergencyContacts = EP.EmployeeContacts;
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec1firstName", EmergencyContacts.EC1.firstName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec1middleName", EmergencyContacts.EC1.middleName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec1lastName", EmergencyContacts.EC1.lastName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec2firstName", EmergencyContacts.EC2.firstName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec2middleName", EmergencyContacts.EC2.middleName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec2lastName", EmergencyContacts.EC2.lastName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec3firstName", EmergencyContacts.EC3.firstName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec3middleName", EmergencyContacts.EC3.middleName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@Ec3lastName", EmergencyContacts.EC3.lastName);
            SP_addEmployeeProfile.Parameters.AddWithValue("@EC1number", EmergencyContacts.EC1.contactnumber);
            SP_addEmployeeProfile.Parameters.AddWithValue("@EC2number", EmergencyContacts.EC2.contactnumber);
            SP_addEmployeeProfile.Parameters.AddWithValue("@EC3number", EmergencyContacts.EC3.contactnumber);

            SP_addEmployeeProfile.ExecuteNonQuery();
        }

        public static void modifyEmployeeProfile(SqlConnection conn, EmployeeProfile EP)
        {
            SqlCommand SP_modifyEmployeeProfile = new SqlCommand("modifyEmployeeProfile", conn);
            SP_modifyEmployeeProfile.CommandType = CommandType.StoredProcedure;

            SP_modifyEmployeeProfile.Parameters.AddWithValue("@EmployeeID", EP.EmployeeID);

            Name EmployeeName = EP.EmployeeName;
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@FirstName", EmployeeName.firstName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@MiddleName", EmployeeName.middleName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@LastName", EmployeeName.lastName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Suffix", EmployeeName.suffix);

            SP_modifyEmployeeProfile.Parameters.AddWithValue("@DateOfBirth", EP.DateOfBirth);

            Address EmployeeAddress = EP.Address;
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@BuildingNumber", EmployeeAddress.BuildingNumber);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@City", EmployeeAddress.CityState);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@zip", EmployeeAddress.CountryZIP);

            SP_modifyEmployeeProfile.Parameters.AddWithValue("@MobileNum", EP.MobileNum);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Landline", EP.Landline);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@FatherNum", EP.FatherNum);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@MotherNum", EP.MotherNum);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@EmailAddress", EP.EmailAddress);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Sex", EP.Sex);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@MaritalStatus", EP.MaritalStatus);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Nationality", EP.Nationality);

            ECList EmergencyContacts = EP.EmployeeContacts;
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec1firstName", EmergencyContacts.EC1.firstName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec1middleName", EmergencyContacts.EC1.middleName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec1lastName", EmergencyContacts.EC1.lastName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec2firstName", EmergencyContacts.EC2.firstName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec2middleName", EmergencyContacts.EC2.middleName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec2lastName", EmergencyContacts.EC2.lastName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec3firstName", EmergencyContacts.EC3.firstName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec3middleName", EmergencyContacts.EC3.middleName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@Ec3lastName", EmergencyContacts.EC3.lastName);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@EC1number", EmergencyContacts.EC1.contactnumber);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@EC2number", EmergencyContacts.EC2.contactnumber);
            SP_modifyEmployeeProfile.Parameters.AddWithValue("@EC3number", EmergencyContacts.EC3.contactnumber);

            SP_modifyEmployeeProfile.ExecuteNonQuery();
        }

        
        public static void addEmployeeHealthRecord(SqlConnection conn, EmployeeHealthRecord EHR)
        {
            SqlCommand SP_addEmployeeHealthRecord = new SqlCommand("createEmployeeHealthRecord", conn);
            SP_addEmployeeHealthRecord.CommandType = CommandType.StoredProcedure;

            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@EmployeeID", EHR.EmployeeID);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@FirstName", EHR.FirstName);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@LastName", EHR.LastName);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@Gender", EHR.Gender);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@FrequencyAlcoholConsumption", EHR.FrequencyAlcoholConsumption);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasAsthma", EHR.HasAsthma);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasCancer", EHR.HasCancer);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasCardiacDisease", EHR.HasCardiacDisease);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasDiabetes", EHR.HasDiabetes);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasHypertension", EHR.HasHypertension);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasPsychiatricDisorder", EHR.HasPsychiatricDisorder);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasEpilepsy", EHR.HasEpilepsy);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasChestPain7", EHR.HasChestPain7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasRespiratory7", EHR.HasRespiratory7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasCardiacDisease7", EHR.HasCardiacDisease7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasCardiovascular7", EHR.HasCardiovascular7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasHematological7", EHR.HasHematological7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasLymphatic7", EHR.HasLymphatic7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasWeightGain7", EHR.HasWeightGain7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasWeightLoss7", EHR.HasWeightLoss7);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@IsTakingMeds", EHR.IsTakingMeds);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@HasMedicationAllergies", EHR.HasMedicationAllergies);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@UsedTabacco", EHR.UsedTabacco);
            SP_addEmployeeHealthRecord.Parameters.AddWithValue("@UsedIllegalDrugs", EHR.UsedIllegalDrugs);

            SP_addEmployeeHealthRecord.ExecuteNonQuery();
        }
        
        public static void modifyEmployeeHealthRecord(SqlConnection conn, EmployeeHealthRecord EHR)
        {
            SqlCommand SP_modifyEmployeeHealthRecord = new SqlCommand("modifyEmployeeHealthRecord", conn);
            SP_modifyEmployeeHealthRecord.CommandType = CommandType.StoredProcedure;

            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@EmployeeID", EHR.EmployeeID);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@FirstName", EHR.FirstName);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@LastName", EHR.LastName);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@Gender", EHR.Gender);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@FrequencyAlcoholConsumption", EHR.FrequencyAlcoholConsumption);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasAsthma", EHR.HasAsthma);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasCancer", EHR.HasCancer);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasCardiacDisease", EHR.HasCardiacDisease);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasDiabetes", EHR.HasDiabetes);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasHypertension", EHR.HasHypertension);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasPsychiatricDisorder", EHR.HasPsychiatricDisorder);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasEpilepsy", EHR.HasEpilepsy);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasChestPain7", EHR.HasChestPain7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasRespiratory7", EHR.HasRespiratory7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasCardiacDisease7", EHR.HasCardiacDisease7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasCardiovascular7", EHR.HasCardiovascular7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasHematological7", EHR.HasHematological7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasLymphatic7", EHR.HasLymphatic7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasWeightGain7", EHR.HasWeightGain7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasWeightLoss7", EHR.HasWeightLoss7);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@IsTakingMeds", EHR.IsTakingMeds);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@HasMedicationAllergies", EHR.HasMedicationAllergies);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@UsedTabacco", EHR.UsedTabacco);
            SP_modifyEmployeeHealthRecord.Parameters.AddWithValue("@UsedIllegalDrugs", EHR.UsedIllegalDrugs);

            SP_modifyEmployeeHealthRecord.ExecuteNonQuery();
        }

        public static int logIn(SqlConnection conn, string email, string password)
        {
            SqlCommand RESET_ALL = new SqlCommand("resetSession", conn);
            RESET_ALL.CommandType = CommandType.StoredProcedure;
            RESET_ALL.ExecuteNonQuery();

            SqlCommand SP_LoginUser = new SqlCommand("logInUser", conn);
            SP_LoginUser.CommandType = CommandType.StoredProcedure;
            SP_LoginUser.Parameters.AddWithValue("@Email", email);
            SP_LoginUser.Parameters.AddWithValue("@Password", password);

            return SP_LoginUser.ExecuteNonQuery();
        }

        public static void SignInUser(SqlConnection conn, string email, string pass, string firstname, string middlename, string lastname)
        {
            SqlCommand SP_NewLogin = new SqlCommand("createNewUserLogin", conn);
            SqlCommand SP_NewPrivilege = new SqlCommand("createNewUserPrivilege", conn);

            SP_NewLogin.CommandType = CommandType.StoredProcedure;
            SP_NewPrivilege.CommandType = CommandType.StoredProcedure;

            SP_NewLogin.Parameters.AddWithValue("@firstname", firstname);
            SP_NewLogin.Parameters.AddWithValue("@middlename", middlename);
            SP_NewLogin.Parameters.AddWithValue("@lastname", lastname);
            SP_NewLogin.Parameters.AddWithValue("@email", email);
            SP_NewLogin.Parameters.AddWithValue("@password", pass);

            SP_NewPrivilege.Parameters.AddWithValue("@BookkeepingModule", 1);
            SP_NewPrivilege.Parameters.AddWithValue("@EmployeeModule", 1);
            SP_NewPrivilege.Parameters.AddWithValue("@TaskModule", 1);
            SP_NewPrivilege.Parameters.AddWithValue("@InventoryModule", 1);
            SP_NewPrivilege.Parameters.AddWithValue("@Permissions", 0);
            SP_NewPrivilege.Parameters.AddWithValue("@email", email);
            SP_NewPrivilege.Parameters.AddWithValue("@password", pass);

            SP_NewLogin.ExecuteNonQuery();
            SP_NewPrivilege.ExecuteNonQuery();

        }

        public static bool checkPrivilege(SqlConnection conn, string module)
        {
            SqlCommand SP_Privilege_Check = new SqlCommand("checkIfUserHasPrivilege", conn);
            SP_Privilege_Check.CommandType = CommandType.StoredProcedure;

            SP_Privilege_Check.Parameters.AddWithValue("@Module", module);
            try
            {
                bool result = (bool)SP_Privilege_Check.ExecuteScalar();

                //MessageBox.Show($"{result}");
                return result;
            } catch
            {
                //MessageBox.Show("False");
                return false;
            }
           

            



        }
    }
}

