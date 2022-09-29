using System;

namespace CPE106_MachineProblem
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            bool done = false;

            while (!done)
            {
                Console.Clear();
                InventoryModule IM = new InventoryModule();
                EmployeeModule EM = new EmployeeModule();
                BookkeepingModule BM = new BookkeepingModule();
                TaskModule TM = new TaskModule();
                string text = "Console Management System\n";
                text += "Modules: \n";
                text += "[1] Inventory Module\n";
                text += "[2] Employee Module\n";
                text += "[3] Tasks Module\n";
                text += "[4] Bookkeeping Module\n";
                text += "[0] Exit\n";
                text += "Input: ";
                Console.WriteLine(text);
                int choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (choice)
                {
                    case 1:
                        IM.Operations();
                        break;
                    case 2:
                        EM.Operations();
                        break;
                    case 3:
                        TM.Operations();
                        break;
                    case 4:
                        BM.Operations();
                        break;

                    case 0:
                        Console.WriteLine("Program is Exiting");
                        Console.ReadKey();
                        Console.Clear();
                        done = true;
                        break;

                    default:
                        text = "Invalid Choice\n";
                        text += "Please enter a valid choice\n";
                        Console.WriteLine(text);
                        break;
                }

            }
        }
    }
}
