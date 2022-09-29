using System;
using System.Collections.Generic;

namespace CPE106_MachineProblem
{
    public class BookkeepingModule
    {
        string text = "";
        List<Transaction> TransactionList = new List<Transaction>();
        int choice = 99;
        double Balance;
        double totalCredit;
        double totalDebit;
        public void Operations()
        {
            while (choice != 0)
            {
                
                Console.Clear();
                text = "Bookkeeping Module\n";
                text += "[1] Show Current workbook\n";
                text += "[2] Add Transaction\n";
                text += "[0] Main Menu";
                Console.WriteLine(text);
                choice = Convert.ToInt32(Console.ReadLine());
                this.evaluate(choice);
            }
            
        }

        void evaluate(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.showWorkbook();
                    break;
                case 2:
                    this.addTransaction();
                    break;

            }   
        }

        public void showWorkbook()
        {
            Console.WriteLine("Date\tNote\t\t\tDebit\t\tCredit");
            foreach (Transaction T in TransactionList)
            {
                T.printSummary();
            }
            this.calculateBalance();
            Console.WriteLine($"Total Debit: {this.totalDebit}\t Total Credit: {this.totalCredit}\n");
            Console.WriteLine($"Balance: {this.Balance}");

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void addTransaction()
        {
            Console.WriteLine("Input Date:");
            string date = Console.ReadLine();
            Console.WriteLine("Input Transaction Notes (Paid Rent, Collected Sales)");
            string note = Console.ReadLine();
            Console.WriteLine("Debit Amount (Only if transaction is debit, 0 otherwise)");
            double debit = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Credit Amount (Only if transaction is credit, 0 otherwise)");
            double credit = Convert.ToDouble(Console.ReadLine());

            if ((debit > 0 ) & (credit > 0)){
                Console.WriteLine("Transaction cannot be both debit and credit , please retry");
            } 
            else
            {
                Transaction newTransaction = new Transaction(debit, credit, note, date);
                TransactionList.Add(newTransaction);
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        private void calculateBalance()
        {
            foreach (Transaction T in TransactionList)
            {
                this.totalCredit += T.getCredit();
                this.totalDebit += T.getDebit();
            }

            this.Balance = this.totalDebit - this.totalCredit;
        }
    }
}