using System;

namespace CPE106_MachineProblem
{
    public class Item
    {
        string Name;
        string Description;
        int Quantity;
        Double unit_price;
        Double item_price;

        public string name => Name;
        public string desc => Description;
        public int pcs => Quantity;
        public double u_p => unit_price;
        public double i_p => item_price;

        public Item()
        {

        }
        public Item(string name, string description, int quantity, double unit_price)
        {
            this.Name = name;
            this.Description = description;
            this.Quantity = quantity;
            this.unit_price = unit_price;
            this.item_price = unit_price * quantity;
        }

        public bool similarTo(string name)
        {
            return (name == this.Name);

        }
    }
}
