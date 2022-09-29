using System;
using System.Collections.Generic;

namespace CPE106_MachineProblem
{
    public class EmployeeModule
    {
        string text;
        int EID = 1;
        int choice = 99;
        List<Employee> EmployeeList = new List<Employee>();
        public void Operations()
        {
            while (choice != 0)
            {
                text = "Employee Module\n";
                text += "[1] Add Employee\n";
                text += "[2] Remove Employee\n";
                text += "[3] Search Employee\n";
                text += "[4] Generate Payslip for Employee\n";
                text += "[5] Print Summary of Employee\n";
                text += "[6] Generate Payroll Sheet\n";
                text += "[0] Main Menu";
                Console.WriteLine(text);
                choice = Convert.ToInt32(Console.ReadLine());
                this.evaluate(choice);
            }
            
        }

        public void evaluate(int choice)
        {
            Console.Clear();
            switch (choice)
            {
                case 1:
                    this.addEmployee();
                    break;
                case 2:
                    this.removeEmployee();
                    break;
                case 3:
                    this.searchEmployee();
                    break;
                case 4:
                    this.generatePayslip();
                    break;
                case 5:
                    this.printEmployeeList();

                    Console.WriteLine("Press Any Key to Continue. . .");
                    Console.ReadKey();
                    break;
                case 6:
                    this.generatePayrollSheet();
                    break;
                default:
                    choice = 0;
                    break;
            }
            Console.Clear();
        }

        public void addEmployee()
        {
            text = "Add an Employee\n";
            text = "Enter the Employee Details:\n";
            Console.WriteLine(text);
            Console.Write("First Name:");
            string fn = Console.ReadLine();
            Console.Write("Middle Name:");
            string mn = Console.ReadLine();
            Console.Write("Last Name:");
            string ln = Console.ReadLine();
            Console.Write("Department:");
            string dep = Console.ReadLine();
            Console.Write("Position:");
            string pos = Console.ReadLine();
            Console.Write("Salary:");
            double pay = Convert.ToDouble(Console.ReadLine());

            Employee newEmployee = new Employee(fn, mn, ln, dep, pos, pay);
            newEmployee.setID(EID);
            EID++;
            EmployeeList.Add(newEmployee);

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();

        }

        public void removeEmployee()
        {
            this.printEmployeeList();
            text = "Remove Employee\n";
            text += "Enter the Employee ID of the Employee to be removed\n";
            Console.WriteLine(text);
            int idToRemove = Convert.ToInt32(Console.ReadLine());
            Employee refEmp = new Employee();
            foreach (Employee E in EmployeeList)
            {
                if (E.similarID(idToRemove))
                {
                    refEmp = E;
                    break;
                }
            }

            EmployeeList.Remove(refEmp);

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void searchEmployee()
        {
            text = "Add an Employee\n";
            text = "Enter the Employee Details:\n";
            Console.WriteLine(text);
            Console.WriteLine("First Name:");
            string fn = Console.ReadLine();
            Console.WriteLine("Middle Name:");
            string mn = Console.ReadLine();
            Console.WriteLine("Last Name:");
            string ln = Console.ReadLine();
            Console.WriteLine("Department:");
            string dep = Console.ReadLine();
            Console.WriteLine("Position:");
            string pos = Console.ReadLine();
            Console.WriteLine("Salary:");
            double pay = Convert.ToDouble(Console.ReadLine());

            Employee searchEmployee = new Employee(fn, mn, ln, dep, pos, pay);
            if (EmployeeList.Contains(searchEmployee))
            {
                int index = EmployeeList.IndexOf(searchEmployee);
                Console.WriteLine($"Employee found at {index} ");
            } else
            {
                Console.WriteLine("Employee not found");
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void generatePayslip()
        {
            text = "Generate Payslip Function\n";
            text += "Enter the ID of the Employee:";
            Console.WriteLine(text);
            int payslipID = Convert.ToInt32(Console.ReadLine());
            foreach (Employee E in EmployeeList)
            {
                if (E.similarID(payslipID))
                {
                    text = "Payslip\n";
                    text += $"{E.payslipString()}\n";
                    text += "Would you like to print this slip?";
                    Console.WriteLine(text);
                    Console.ReadKey();
                    break;
                }
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void printEmployeeList()
        {
            foreach(Employee E in EmployeeList)
            {
                E.printDetails();
                Console.Write("\n");
            }



        }

        public void generatePayrollSheet()
        {
            text = "Generate Payroll Sheet Function\n";
            text += "Enter the file name of the payroll sheet\t";
            text += "[Default : CMSPayrollSheet.csv]";
            Console.WriteLine(text);
            string name = Console.ReadLine();

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }


    }
}