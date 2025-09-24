using System;
using System.Collections.Generic;

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

        public Product(string name, double price, int quantity, Category category)
        {
            Code = "1" + (newId++).ToString();
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }

        public bool chek()
        {
            return Quantity > 0;
        }

        public void PrintShort()
        {
            Console.WriteLine($"{Code} | {Name} | {Price}руб | {Quantity}шт. | {Category}");
        }

        public void print()
        {
            Console.WriteLine($"Код: {Code}");
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Количество: {Quantity}");
            Console.WriteLine($"В наличии: {(chek() ? "Да" : "Нет")}");
            Console.WriteLine($"Категория: {Category}");
        }
    }

    internal class Program
    {
        static List<Product> products = new List<Product>();

        static void add()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("ДОБАВЛЕНИЕ ТОВАРА");

                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();

                Console.Write("Введите цену товара: ");
                double price = Convert.ToDouble(Console.ReadLine());

                Console.Write("Введите количество товара: ");
                int quantity = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Доступные категории:");
                Console.WriteLine("0. Electronics");
                Console.WriteLine("1. Clothing");
                Console.WriteLine("2. Food");
                Console.WriteLine("3. Books");
                Console.Write("Выберите категорию (0-3): ");
                Category category = (Category)Convert.ToInt32(Console.ReadLine());

                Product newProduct = new Product(name, price, quantity, category);
                products.Add(newProduct);
                Console.WriteLine($"Товар успешно добавлен! Код: {newProduct.Code}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void remove()
        {
            Console.Clear();
            Console.WriteLine("УДАЛЕНИЕ ТОВАРА");
            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст!");
                return;
            }

            prlist();
            Console.Write("Введите код товара для удаления: ");
            string code = Console.ReadLine();

            Product toRemove = null;
            foreach (Product product in products)
            {
                if (product.Code == code)
                {
                    toRemove = product;
                    break;
                }
            }

            if (toRemove != null)
            {
                products.Remove(toRemove);
                Console.WriteLine($"Товар '{toRemove.Name}' удален!");
            }
            else
            {
                Console.WriteLine("Товар с таким кодом не найден!");
            }
        }

        static void zakaz()
        {
            Console.Clear();
            Console.WriteLine("ЗАКАЗ ПОСТАВКИ");
            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст!");
                return;
            }

            prlist();
            Console.Write("Введите код товара: ");
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

            if (product != null)
            {
                Console.Write($"Текущее количество: {product.Quantity}. Введите количество для добавления: ");
                if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                {
                    product.Quantity += amount;
                    Console.WriteLine($"Поставка добавлена! Новое количество: {product.Quantity}");
                }
                else
                {
                    Console.WriteLine("Ошибка ввода количества!");
                }
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        static void sell()
        {
            Console.Clear();
            Console.WriteLine("ПРОДАЖА ТОВАРА");
            if (products.Count == 0)
            {
                Console.WriteLine("Список товаров пуст!");
                return;
            }

            prlist();
            Console.Write("Введите код товара: ");
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

            if (product != null)
            {
                if (!product.chek())
                {
                    Console.WriteLine("Товара нет в наличии!");
                    return;
                }

                Console.Write($"Доступно: {product.Quantity} шт. Введите количество для продажи: ");
                if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
                {
                    if (amount <= product.Quantity)
                    {
                        product.Quantity -= amount;
                        double total = product.Price * amount;
                        Console.WriteLine($"Продано {amount} шт. на сумму {total} руб.");
                        Console.WriteLine($"Остаток: {product.Quantity} шт.");
                    }
                    else
                    {
                        Console.WriteLine("Недостаточно товара на складе!");
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода количества!");
                }
            }
            else
            {
                Console.WriteLine("Товар не найден!");
            }
        }

        

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("МЕНЮ");
                Console.WriteLine("1. Добавить товар");
                Console.WriteLine("2. Удалить товар");
                Console.WriteLine("3. Заказать поставку товара");
                Console.WriteLine("4. Продать товар");
                Console.WriteLine("5. Поиск товара по коду");
                Console.WriteLine("6. Поиск товара по названию");
                Console.WriteLine("7. Поиск товара по категории");
                Console.WriteLine("8. Показать все товары");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": add(); break;
                    case "2": remove(); break;
                    case "3": zakaz(); break;
                    case "4": sell(); break;
                    case "5": searchcode(); break;
                    case "6": searchname(); break;
                    case "7": searchcategor(); break;
                    case "8": show(); break;
                    case "0":
                        Console.WriteLine("Выход из программы...");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}