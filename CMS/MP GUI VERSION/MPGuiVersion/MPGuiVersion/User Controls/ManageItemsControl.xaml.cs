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
    /// Interaction logic for ManageItemsControl.xaml
    /// </summary>
    public partial class ManageItemsControl : UserControl
    {
        SqlConnection conn;
        public ManageItemsControl()
        {
            InitializeComponent();
            getDataFromServer();
        }

        private void getDataFromServer()
        {
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out this.conn, out status, out output);

            DataView InventoryData = DatabaseConnection.getDatas(this.conn, "Items");
            ItemList.ItemsSource = null;
            ItemList.ItemsSource = InventoryData;

            DatabaseConnection.disconnectSQL(this.conn, out status);
        }

        private void SearchItemBtn_Click(object sender, RoutedEventArgs e)
        {
            int IID = Convert.ToInt32(ItemID.Text);
            bool found = false;

            foreach (DataRowView row in ItemList.ItemsSource)
            {
                var str = row["itemID"].ToString();
                bool check = IID == Convert.ToInt32(str);
                if (check)
                {
                    ItemList.SelectedItem = row;
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                MessageBox.Show("Item ID Not Found");
            }
        }

        private void ResetFieldsBtn_Click(object sender, RoutedEventArgs e)
        {
            ItemID.Text = "";
            ItemName.Text = "";
            Quantity.Text = "";
            UnitPrice.Text = "";
            Description.Text = "";
        }

        private void DeleteItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selected = (DataRowView)ItemList.SelectedItem;
                int target_id = Convert.ToInt32(selected["itemID"].ToString());
                InventoryModule.deleteItem(target_id);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check if you have selected an item");
            }

            getDataFromServer();
        }

        private void ModifyItemBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Item I = new Item();
                I.ItemID = Convert.ToInt32(ItemID.Text);
                I.ItemName = ItemName.Text;
                I.Quantity = Convert.ToInt32(Quantity.Text);
                I.UnitPrice = Convert.ToDouble(UnitPrice.Text);
                I.Description = Description.Text;
                InventoryModule.modifyItem(I);
            }
            catch
            {
                MessageBox.Show("An Error has occurred, please check the entered values");
            }
            

            getDataFromServer();
        }
    }
}
