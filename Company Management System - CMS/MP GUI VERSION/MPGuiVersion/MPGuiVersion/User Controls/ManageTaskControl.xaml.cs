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
    /// Interaction logic for ManageTaskControl.xaml
    /// </summary>
    public partial class ManageTaskControl : UserControl
    {
        SqlConnection conn;
        public ManageTaskControl()
        {
            InitializeComponent();
            getDataFromServer();
        }

        private void resetEntries()
        {
            TaskID.Text = "";
            TaskName.Text = "";
            Budget.Text = "";
            DueDate.SelectedDate = DateTime.MinValue;
            isDone.IsChecked = false;
            notDone.IsChecked = false;
            Materials.Text = "";
            EmployeeNum.Text = "";
            Description.Text = "";
            EmployeesAssigned.Text = "";
            EmployeeID.Text = "";
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView TaskData = DatabaseConnection.getDatas(this.conn, "Tasks");
            DataView EmployeeData = DatabaseConnection.getDatas(this.conn, "Employees");

            TaskList.ItemsSource = null;
            TaskList.ItemsSource = TaskData;
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = EmployeeData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void SearchTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            int TID = Convert.ToInt32(TaskID.Text);
            bool found = false;

            foreach (DataRowView row in TaskList.ItemsSource)
            {
                var str = row["taskID"].ToString();
                bool check = TID == Convert.ToInt32(str);
                if (check)
                {
                    TaskList.SelectedItem = row;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Task ID Not Found");
            }
        }

        private void SearchEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            int EID = Convert.ToInt32(EmployeeID.Text);
            bool found = false;

            foreach (DataRowView row in EmployeeList.ItemsSource)
            {
                var str = row["employeeID"].ToString();
                bool check = EID == Convert.ToInt32(str);
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

        private void DeleteTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selected = (DataRowView)TaskList.SelectedItem;
                int target_id = Convert.ToInt32(selected["taskID"].ToString());
                TaskModule.deleteTask(target_id);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check if you have selected a task");
            }
            getDataFromServer();
        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            resetEntries();
        }

        private void ModifyTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task T = new Task();
                T.TaskID = Convert.ToInt32(TaskID.Text);
                T.TaskName = TaskName.Text;
                T.Budget = Convert.ToDouble(Budget.Text);
                T.DueDate = DueDate.ToString();
                T.IsDone = (bool)isDone.IsChecked;
                T.MaterialsNeeded = Materials.Text;
                T.EmployeesNeeded = Convert.ToInt32(EmployeeNum.Text);
                T.Description = Description.Text;
                T.AssignedEmployees = EmployeesAssigned.Text;
                TaskModule.modifyTask(T);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }


            
            resetEntries();
            getDataFromServer();
        }

        private void AssignEmployeeBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task T = new Task();
                T.TaskID = Convert.ToInt32(TaskID.Text);
                T.AssignedEmployees = EmployeesAssigned.Text;
                TaskModule.assignEmployee(T);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }
            
            resetEntries();
            getDataFromServer();
        }
    }
}
