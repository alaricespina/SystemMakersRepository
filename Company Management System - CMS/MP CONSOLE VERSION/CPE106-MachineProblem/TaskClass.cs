using System;

namespace CPE106_MachineProblem
{
    public class Task
    {
        string name;
        string duedate;
        int[] assignedEID;
        string description;
        string materials_needed;
        double budget;
        bool finished = false;

        int employeeCount;
        int employeeIterator;

        public Task()
        {

        }
        public Task(string name, string duedate, int employeeCount, string description, string materials_needed, double budget)
        {
            this.name = name;
            this.duedate = duedate;
            this.employeeCount = employeeCount;
            this.description = description;
            this.materials_needed = materials_needed;
            this.budget = budget;

            this.assignedEID = new int[this.employeeCount];
            this.employeeIterator = 0;
        }

        public void AssignEmployee(int EID)
        {
            if (this.employeeCount > this.employeeIterator)
            {
                this.assignedEID[this.employeeIterator] = EID;
                this.employeeIterator++;
            } 
            else
            {
                Console.WriteLine("Number of Employees already assigned");
            }
            

        }

        public void printDetails()
        {
            Console.WriteLine($"Task: {this.name} Due on: {this.duedate} Budget: {this.budget} Materials Needed: {this.materials_needed} Description: {this.description}");
        }

        public bool compareName(string name)
        {
            return (this.name.ToLower() == name.ToLower());
        }

        public void setDone()
        {
            this.finished = true;
        }

        public bool isDone()
        {
            return (this.finished);
        }



    }
}
