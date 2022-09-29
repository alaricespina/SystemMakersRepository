using System;

namespace CPE106_MachineProblem
{
    public class Transaction
    {
        double debit = 0;
        double credit = 0;
        string note;
        string date;

        public Transaction(double d, double c,string note, string date)
        {
            this.debit = d;
            this.credit = c;
            this.note = note;
            this.date = date;
        }

        public void printSummary()
        {
            Console.Write($"{this.date} {this.note} {this.debit} {this.credit}\n");
        }

        public double getDebit()
        {
            return this.debit;
        }

        public double getCredit()
        {
            return this.credit;
        }
    }
}