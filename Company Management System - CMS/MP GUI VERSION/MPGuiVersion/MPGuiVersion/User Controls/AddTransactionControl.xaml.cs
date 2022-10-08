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
    /// Interaction logic for AddTransactionControl.xaml
    /// </summary>
    public partial class AddTransactionControl : UserControl
    {
        BookkeepingModule BM;
        public AddTransactionControl()
        {
            InitializeComponent();
            this.BM = new BookkeepingModule();
            updateData();

        }

        public void updateData()
        {
            double totalCred;
            double totalDeb;

            this.BM.calculateTotal();
            this.BM.TotalValues(out totalCred, out totalDeb);

            TotalCreditLbl.Content = $"P {totalCred}";
            TotalDebitLbl.Content = $"P {totalDeb}";
        }

        private void AddTransactionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Transaction ret = new Transaction();
                ret.TransactionID = Convert.ToInt32(TransactionIDbox.Text);
                ret.Name = TransactionNamebox.Text;
                ret.TransactionD = (bool)DebitOption.IsChecked;
                ret.TransactionC = (bool)CreditOption.IsChecked;
                ret.Amount = Convert.ToDouble(Amount.Text);
                ret.Summary = Descriptionbox.Text;
                this.BM.addTransaction(ret);
            }
            catch
            {
                MessageBox.Show("An Error has occured, please check the values entered");
            }
            
            updateData();


        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            TransactionIDbox.Text = "";
            TransactionNamebox.Text = "";
            DebitOption.IsChecked = false;
            CreditOption.IsChecked = false;
            Amount.Text = "";
            Descriptionbox.Text = "";
        }
    }
}
