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

        public static string Code;
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
        static void add()
        {
            try
            {
                Console.WriteLine("");
                string name = Console.ReadLine();
                Console.WriteLine("");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("");
                int quent = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("");
                Console.Write("Введите количество товара: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Доступные категории:");
                Console.WriteLine("0. Electronics");
                Console.WriteLine("1. Clothing");
                Console.WriteLine("2. Food");
                Console.WriteLine("3. Books");
                Console.Write("Выберите категорию (0-3): ");
                Category categor = (Category)Convert.ToInt32(Console.ReadLine());

                Product newproduct = new Product(name, (double)price, quent, categor);
                products.Add(newproduct);
                Console.WriteLine($"" { newproduct.Code}
                ;
            }
            }
        static void remove()
        {
            Console.Clear();
            Console.WriteLine("");
            if (products.Count == 0)
            {
                Console.WriteLine("");
                return;
            }
            prlist();
            Console.WriteLine("");
            string code = Console.ReadLine();

            Product toremove = null;
            foreach (Product product in products) {
                {
                    if (product.Code == code)
                    {
                        toremove = product;
                        break;
                    }
                }
                if (toremove != null)
                {
                    products.Remove(toremove);
                    Console.WriteLine("");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        static void zakaz()
        {
            Console.Clear();
            Console.WriteLine("");
            if (products.Count == 0)
            {
                Console.WriteLine("");
                return;
            }

            prlist();
            Console.WriteLine("");
            string code = Console.ReadLine();

            Product product = null;
            foreach (Product p in products)
            {
                if (p.Code == code)
                {
                    product = p;
                    break;
                }
            }
            if (product != null) {
                Console.WriteLine("");
                if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
        }
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

                    switch (n)
                    {
                        case "1":
                            add();
                            break;
                        case "2":
                            Console.WriteLine();
                            break;
                        case "3":

                            break;
                        case "4":
                            break;
                        case "5":
                            break;
                        case "6":
                            break;
                        case "7":
                            break;
                    }

                }
            }
        }
    }
}