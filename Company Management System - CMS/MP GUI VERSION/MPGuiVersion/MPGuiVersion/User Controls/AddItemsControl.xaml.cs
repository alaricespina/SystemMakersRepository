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
    /// Interaction logic for AddItemsControl.xaml
    /// </summary>
    public partial class AddItemsControl : UserControl
    {
        public AddItemsControl()
        {
            InitializeComponent();
        }

        private void AddItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Item I = new Item();
                I.ItemID = Convert.ToInt32(ItemID.Text);
                I.ItemName = ItemName.Text;
                I.Quantity = Convert.ToInt32(Quantity.Text);
                I.UnitPrice = Convert.ToDouble(UnitPrice.Text);
                I.Description = Description.Text;
                InventoryModule.addItem(I);
            }
            catch
            {
                MessageBox.Show("An Error has occured, please check the values entered");
            }
            resetEntries();

        }

        private void resetEntries()
        {
            ItemID.Text = "";
            ItemName.Text = "";
            Quantity.Text = "";
            UnitPrice.Text = "";
            Description.Text = "";
        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            resetEntries();
        }
    }
}
