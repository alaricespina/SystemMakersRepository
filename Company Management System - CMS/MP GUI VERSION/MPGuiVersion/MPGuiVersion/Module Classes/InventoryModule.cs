using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace MPGuiVersion
{
    public static class InventoryModule
    {

        public static void addItem(Item ret)
        {
            SqlConnection conn;
            string message;
            bool state;
            DatabaseConnection.connectToSQL(out conn, out state, out message);
            DatabaseConnection.addItem(conn, ret);
            DatabaseConnection.disconnectSQL(conn, out state);

        }
        public static void deleteItem(int target_id)
        {
            SqlConnection conn;
            bool status;
            string output;

            DatabaseConnection.connectToSQL(out conn, out status, out output);
            DatabaseConnection.deleteItem(conn, target_id);
            DatabaseConnection.disconnectSQL(conn, out status);
        }
        public static void modifyItem(Item I)
        {
            SqlConnection conn;
            string message;
            bool state;

            DatabaseConnection.connectToSQL(out conn, out state, out message);
            DatabaseConnection.modifyItem(conn, I);
            DatabaseConnection.disconnectSQL(conn, out state);

        }
    }
}
