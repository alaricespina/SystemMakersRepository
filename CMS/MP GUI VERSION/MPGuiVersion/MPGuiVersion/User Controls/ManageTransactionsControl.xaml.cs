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
    /// Interaction logic for ManageEmployees.xaml
    /// </summary>
    public partial class ManageTransactionsControl : UserControl
    {
        SqlConnection conn;
        public ManageTransactionsControl()
        {
            InitializeComponent();
            getDataFromServer();
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView TransactionData = DatabaseConnection.getDatas(this.conn, "Transactions");
            TransactionList.ItemsSource = null;
            TransactionList.ItemsSource = TransactionData;
            

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        public void getTotalStats(out double totalCredit, out double totalDebit) {
            getDataFromServer();

            totalCredit = 0.00;
            totalDebit = 0.00;

            foreach (DataRowView row in TransactionList.ItemsSource)
            {
                if (row["isCredit"].ToString() == "True")
                {
                    totalCredit += Convert.ToDouble(row["amount"].ToString());
                }
                else if (row["isDebit"].ToString() == "True")
                {
                    totalDebit += Convert.ToDouble(row["amount"].ToString());
                }
            }

        }

        private void DeleteTransaction_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                DataRowView selected = (DataRowView)TransactionList.SelectedItem;
                int target_id = Convert.ToInt32(selected["transactionID"].ToString());

                BookkeepingModule BM = new BookkeepingModule();
                BM.deleteTransaction(target_id);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check if you have selected a transaction");
            }
            

            getDataFromServer();
            //MessageBox.Show($"{target_id}");

   
        }

        private void SearchTransaction_Click(object sender, RoutedEventArgs e)
        {
            int TID = Convert.ToInt32(TransactionID.Text);
            bool found = false;

            foreach (DataRowView row in TransactionList.ItemsSource)
            {
                var str = row["transactionID"].ToString();
                bool check = (TID == Convert.ToInt32(str));
                if (check)
                {
                    TransactionList.SelectedItem = row;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Transaction ID Not Found");
            }
        }

        private void PrintTransactions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TransactionList.SelectAllCells();
                TransactionList.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                ApplicationCommands.Copy.Execute(null, TransactionList);
                TransactionList.UnselectAllCells();
                SaveFileDialog exportDialog = new SaveFileDialog();

                exportDialog.Filter = "Text file (*.txt)|*.txt|CSV file (*.csv)|*.csv";
                exportDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                exportDialog.ShowDialog();

                string result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                File.AppendAllText(exportDialog.FileName, result, UnicodeEncoding.UTF8);
            } catch
            {
                MessageBox.Show("An Error has occurred, please try again");
            }
            
        }
    }

    
}
