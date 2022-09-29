using System;
using System.Collections.Generic;
using System.Text;

namespace MPGuiVersion
{
    public class Transaction
    {
        private int transactionID;
        private string fullName;
        private string description;
        private bool isCredit = true;
        private bool isDebit = false;
        private double amount;


        public string Name { get => fullName; set => fullName = value; }
        public string Summary { get => description; set => description = value; }
        public bool TransactionC { get => isCredit; set => isCredit = value; }
        public bool TransactionD { get => isDebit; set => isDebit = value; }
        public double Amount { get => amount; set => amount = value; }
        public int TransactionID { get => transactionID; set => transactionID = value; }
    }
}