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
    /// Interaction logic for AddTaskControl.xaml
    /// </summary>
    public partial class AddTaskControl : UserControl
    {
        public AddTaskControl()
        {
            InitializeComponent();
        }

        private void AddTaskBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Task T = new Task();
                T.TaskID = Convert.ToInt32(TaskID.Text);
                T.TaskName = TaskName.Text;
                T.DueDate = DueDate.ToString();
                T.Budget = Convert.ToDouble(Budget.Text);
                T.IsDone = false;
                T.MaterialsNeeded = MaterialsNeeded.Text;
                T.Description = Description.Text;
                T.EmployeesNeeded = Convert.ToInt32(EmployeesNeeded.Text);
                TaskModule.addTask(T);
            }
            catch
            {
                MessageBox.Show("An Error has occured, please check the values entered");
            }
            
            resetEntries();
        }

        private void resetEntries()
        {
            TaskID.Text = "";
            TaskName.Text = "";
            DueDate.SelectedDate = DateTime.MinValue;
            Budget.Text = "";
            MaterialsNeeded.Text = "";
            Description.Text = "";
            EmployeesNeeded.Text = "";          

        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            resetEntries();
        }
    }
}
