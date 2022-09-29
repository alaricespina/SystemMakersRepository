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

using Microsoft.Win32;
using System.IO;

namespace MPGuiVersion.User_Controls
{
    /// <summary>
    /// Interaction logic for PayrollSheetControl.xaml
    /// </summary>
    public partial class PayrollSheetControl : UserControl
    {
        SqlConnection conn;
        public PayrollSheetControl()
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
            PayrollSheetList.ItemsSource = null;
            PayrollSheetList.ItemsSource = EmployeeData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            getDataFromServer();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                PayrollSheetList.SelectAllCells();
                PayrollSheetList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, PayrollSheetList);
                PayrollSheetList.UnselectAllCells();
                SaveFileDialog exportDialog = new SaveFileDialog();

                exportDialog.Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";
                exportDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                exportDialog.ShowDialog();


                string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                File.AppendAllText(exportDialog.FileName, result, UnicodeEncoding.UTF8);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please try again");
            }
        }
    }
}
