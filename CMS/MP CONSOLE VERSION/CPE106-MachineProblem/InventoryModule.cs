using System;
using System.Collections.Generic;

namespace CPE106_MachineProblem
{
    public class InventoryModule
    {
        string text = "";
        List<Item> ItemList = new List<Item>();
        int choice = 99;
        int width = 15;
        public void Operations()
        {
            while (choice != 0)
            {
                
                Console.Clear();
                text = "Inventory Module\n";
                text += "[1] Add Item\n";
                text += "[2] Remove Item\n";
                text += "[3] Search Item\n";
                text += "[4] Print Summary of Items\n";
                text += "[0] Main Menu";
                Console.WriteLine(text); 
                choice = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                this.evaluate(choice);
            }
            
        }

        void evaluate(int choice)
        {
            switch (choice)
            {
                case 1:
                    this.addItem();
                    break;
                case 2:
                    this.removeItem();
                    break;
                case 3:
                    this.searchItem();
                    break;
                case 4:
                    this.printItemList();
                    Console.WriteLine("Press Any Key to Continue. . .");
                    Console.ReadKey();
                    break;
            }   
        }

        public void addItem()
        {
            Console.WriteLine("Add an Item\n");
            Console.Write("Item Name: ");
            string Item_Name = Console.ReadLine();
            Console.Write("Item Description: ");
            string Item_Description = Console.ReadLine();
            Console.Write("Item Quantity: ");
            int Item_Quantity = Convert.ToInt32(Console.ReadLine());
            Console.Write("Unit Price (P): ");
            double Item_Unit_Price = Convert.ToDouble(Console.ReadLine());

            Item newItem = new Item(Item_Name, Item_Description, Item_Quantity, Item_Unit_Price);
            ItemList.Add(newItem);

            Console.WriteLine($"\nItem {Item_Name} was added to the ItemList");
            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();

        }

        public void removeItem()
        {
            this.printItemList();
            Console.WriteLine("Enter the Index of the item to be deleted");
            ItemList.RemoveAt(Convert.ToInt32(Console.ReadLine()));

            Console.WriteLine("Press Any Key to Continue. . .");
            Console.ReadKey();

        }

        private void printItemListHeader(int width)
        {

            string[] headers = {"Index", "Name", "Quantity", "Unit Price", "Item Price", "Description"};
            foreach (String S in headers)
            {
                Console.Write(S.PadRight(width));
            }
            Console.Write("\n");
        }

        private void printSearchListHeader(int width)
        {

            string[] headers = { "Name", "Quantity", "Unit Price", "Item Price", "Description" };
            foreach (String S in headers)
            {
                Console.Write(S.PadRight(width));
            }
            Console.Write("\n");
        }

        public void printItemList()
        {
            int iterator = 0;

            printItemListHeader(width);

            foreach(Item X in ItemList)
            {
                Console.Write(Convert.ToString(iterator).PadRight(width));
                Console.Write(X.name.PadRight(width));
                Console.Write(Convert.ToString(X.pcs).PadRight(width));
                Console.Write(Convert.ToString(X.u_p).PadRight(width));
                Console.Write(Convert.ToString(X.i_p).PadRight(width));
                Console.Write(X.desc);
                Console.Write("\n");

                iterator++;
            }

        }

        public void searchItem()
        {
            Console.WriteLine("Search Function\n");
            Console.WriteLine("Specify the details of the item that you want to search\n");
            Console.Write("Item Name: ");
            string searchItemName = Console.ReadLine();
            Item searchItem = new Item();
            List<Item> searchResults = new List<Item>();
            int found = 0;

            foreach (Item I in ItemList)
            {
                if (I.similarTo(searchItemName))
                {
                    searchItem = I;
                    searchResults.Add(searchItem);
                    found += 1;
                }
            }

            if (found > 0)
            {
                Console.WriteLine($"\nThere are {found} item/s similar to {searchItemName}\n");
                printSearchListHeader(width);
                foreach (Item I in searchResults)
                {
                    
                    Console.Write(I.name.PadRight(width));
                    Console.Write(Convert.ToString(I.pcs).PadRight(width));
                    Console.Write(Convert.ToString(I.u_p).PadRight(width));
                    Console.Write(Convert.ToString(I.i_p).PadRight(width));
                    Console.Write(I.desc);
                    Console.Write("\n");
                }
            } 
            else
            {
                Console.WriteLine($"{searchItemName} is not on the list");
            }

            


            Console.WriteLine("\nPress Any Key to Continue. . .");
            Console.ReadKey();
        }
    }
}