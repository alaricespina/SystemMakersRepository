using System;

namespace CPE106_MachineProblem
{
    public class Employee
    {
        string first_name;
        string middle_name;
        string last_name;
        int employee_id;
        string position;
        string department;
        double monthly_salary;

        public Employee()
        {

        }
        public Employee (string fn, string mn, string ln, string dep, string pos, double pay) 
        {
            this.first_name = fn;
            this.middle_name = mn;
            this.last_name = ln;
            this.department = dep;
            this.position = pos;
            this.monthly_salary = pay;
        }

        public void setID(int id)
        {
            this.employee_id = id;
        }

        public void removeID(int id)
        {
            this.employee_id = 0;
        }

        public int getID()
        {
            return this.employee_id;
        }

        public void printDetails()
        {
            Console.Write($"{this.employee_id} {this.first_name} {this.middle_name} {this.last_name} {this.department} {this.position}");

        }

        public bool similarID(int ID)
        {
            return (this.employee_id == ID);
        }

        public string payslipString()
        {
            string result;
            result = $"{this.first_name}\t{this.middle_name}\t{this.last_name}\n";
            result += $"{this.position} {this.department} Department\t{this.employee_id}\n";
            result += $"Salary: P {this.monthly_salary}\n";
            result += $"Contributions:\nSSS\tP 0\nPhilHealth\t0\nPagIbig\t0\n";

            return result;
        }
    }
}
