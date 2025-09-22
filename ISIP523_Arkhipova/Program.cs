using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    enum Category
    {
        electronics,
        clothing,
        food,
        books,
    }

    class Product
    {
        private static int newId = 1000;

        public string Code;
        public string Name;
        public double Price;
        public int Quantity;
        public Category Category;

        public Product(string name, double price, int kolvo, Category categor)
        {
            Code = "1" + (newId++).ToString();
            Name = name;
            Price = price;
            Quantity = kolvo;
            Category = categor;
        }
        public bool chek()
        {
            return Quantity > 0;
        }
        public void print()
        {
            Console.WriteLine($"{Code} | {Name} | {Price}руб | {Quantity}шт. | {Category}");
        }
    }
     
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> products = new List<Product>();
    }
}
}
