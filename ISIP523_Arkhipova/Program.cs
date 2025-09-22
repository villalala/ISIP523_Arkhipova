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
        public void vvod()
        {
            Console.WriteLine($"Код: ");
            Console.WriteLine($"Название: ");
            Console.WriteLine($"Цена: ");
            Console.WriteLine($"Количества ");
            Console.WriteLine($"В наличии: ");
            Console.WriteLine($"Категория: ");
        }
    }
     
    internal class Program
    {
        static List<Product> products = new List<Product>();
        static void Main(string[] args)
        {
            
            while (true)
            {
                Console.WriteLine($"Меню");
                Console.WriteLine($"1. Добавить товар");
                Console.WriteLine($"2. Удалить товар ");
                Console.WriteLine($"3. Заказать поставку товара");
                Console.WriteLine($"4. Продать товар");
                Console.WriteLine($"5. Поиск товара по коду");
                Console.WriteLine($"6. Поиск товара по названию");
                Console.WriteLine($"7. Поиск товара по категории");
            
                string n = Console.ReadLine();

                switch(n)
                {
                    case "1":
                        Console.WriteLine("");
                        break;
                    case "2":
                        Console.WriteLine();
                        break;
                    case "3":
                }

            }
        }
    }
