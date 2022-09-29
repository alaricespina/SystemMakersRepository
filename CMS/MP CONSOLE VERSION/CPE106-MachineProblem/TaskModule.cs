using System;
using System.Collections.Generic;

namespace CPE106_MachineProblem
{
    public class TaskModule
    {
        string text = "";
        List<Task> TaskList = new List<Task>();
        List<Task> FinishedTaskList = new List<Task>();
        int choice = 99;
        public void Operations()
        {
            while (choice != 0)
            {
                
                Console.Clear();
                text = "Tasks Module\n";
                text += "[1] Add Task\n";
                text += "[2] Remove Task\n";
                text += "[3] Search Task\n";
                text += "[4] Print Summary of Tasks\n";
                text += "[5] Assign Tasks to Employees\n";
                text += "[6] Task History\n";
                text += "[7] Update Job Status\n";
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
                    this.addTask();
                    break;
                case 2:
                    this.removeTask();
                    break;
                case 3:
                    this.searchTask();
                    break;
                case 4:
                    this.printSummary();
                    break;
                case 5:
                    this.assignTask();
                    break;
                case 6:
                    this.printHistory();
                    break;
                case 7:
                    this.updateStatus();
                    break;

            }   
        }

        public void addTask()
        {
            Console.WriteLine("Add Task Module");
            Console.WriteLine("Task Name:");
            string task_name = Console.ReadLine();
            Console.WriteLine("Due Date:");
            string due_date = Console.ReadLine();
            Console.WriteLine("Budget:");
            double budget = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Materials Needed:");
            string materials_needed = Console.ReadLine();
            Console.WriteLine("Description:");
            string description = Console.ReadLine();
            Console.WriteLine("Employees Needed:");
            int employees_needed = Convert.ToInt32(Console.ReadLine());
            
            Task newTask = new Task(task_name, due_date, employees_needed, description, materials_needed, budget);
            TaskList.Add(newTask);

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void removeTask()
        {
            Console.WriteLine("Remove Task Function");
            Console.WriteLine("Enter Task Name:");
            string task_name = Console.ReadLine();
            Task searchTask = new Task();
            
            foreach (Task T in TaskList)
            {
                if (T.compareName(task_name))
                {
                    searchTask = T;
                    break;
                }
            }

            TaskList.Remove(searchTask);

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();

        }

        public void searchTask()
        {
            Console.WriteLine("Remove Task Function");
            Console.WriteLine("Enter Task Name:");
            string task_name = Console.ReadLine();
            Task searchTask = new Task();
            bool found = false;
            foreach (Task T in TaskList)
            {
                if (T.compareName(task_name))
                {
                    searchTask = T;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Console.WriteLine($"Task named: {task_name} was found");
                searchTask.printDetails();
            } else
            {
                Console.WriteLine($"Task named: {task_name} was not found on the list");
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();


        }

        public void printSummary()
        {
            Console.WriteLine("Tasks Scheduled");
            foreach (Task T in TaskList)
            {
                T.printDetails();
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void assignTask()
        {

            Console.WriteLine("Assign Task Function");
            Console.WriteLine("Enter Task Name:");
            string task_name = Console.ReadLine();
            Task searchTask = new Task();
            bool found = false;
            foreach (Task T in TaskList)
            {
                if (T.compareName(task_name))
                {
                    searchTask = T;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Console.WriteLine($"Task named: {task_name} was found");
                int EID = 99;
                int task_index = 0;
                while (EID != -1)
                {
                    Console.WriteLine("Enter the EID of the employee to be assigned:");
                    Console.WriteLine("Enter [-1] to stop");
                    EID = Convert.ToInt32(Console.ReadLine());
                    task_index = TaskList.IndexOf(searchTask);
                    TaskList[task_index].AssignEmployee(EID);
                }

            }
            else
            {
                Console.WriteLine($"Task named: {task_name} was not found on the list");
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void printHistory()
        {
            Console.WriteLine("Finished Tasks History");
            foreach (Task T in TaskList)
            {
                T.printDetails();
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        public void updateStatus()
        {
            Console.WriteLine("Update Task Status Function");
            Console.WriteLine("Enter Task Name:");
            string task_name = Console.ReadLine();
            Task searchTask = new Task();
            bool found = false;
            foreach (Task T in TaskList)
            {
                if (T.compareName(task_name))
                {
                    searchTask = T;
                    found = true;
                    break;
                }
            }

            if (found)
            {
                Console.WriteLine($"Task named: {task_name} was found");
                searchTask.setDone();
                TaskList.Remove(searchTask);
                FinishedTaskList.Add(searchTask);
            }
            else
            {
                Console.WriteLine($"Task named: {task_name} was not found on the list");
            }

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();
        }

        
    }
}