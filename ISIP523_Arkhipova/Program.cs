using System;
using System.Collections.Generic;
using System.Linq;

namespace ISIP523_Arkhipova
{
    public enum Janr
    {
        Fantasy,
        Nayka,
        Detectiv,
        Horror,
        Comedy
    }

    public class Book
    {
        private static int nextId = 1;

        public int id;
        public string title;
        public string author;
        public Janr janr;
        public int year;
        public decimal price;

        public Book(string title, string author, Janr janr, int year, decimal price)
        {
            this.id = nextId++;
            this.title = title;
            this.author = author;
            this.janr = janr;
            this.year = year;
            this.price = price;
        }

        public static void ResetIdCounter()
        {
            nextId = 1;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"ID: {id}, Название: {title}, Автор: {author}, Жанр: {janr}, Год: {year}, Цена: {price:C}");
        }
    }

    internal class Program
    {
        private static List<Book> books = new List<Book>();

        static void InitializeTestData()
        {
            books.Clear();
            Book.ResetIdCounter();

            books.Add(new Book("Властелин Колец", "Дж. Р. Р. Толкин", Janr.Fantasy, 1954, 1200m));
            books.Add(new Book("Убийство в Восточном экспрессе", "Агата Кристи", Janr.Detectiv, 1934, 650m));
            books.Add(new Book("Гордость и предубеждение", "Джейн Остин", Janr.Comedy, 1813, 700m));
            books.Add(new Book("Дракула", "Брэм Стокер", Janr.Horror, 1897, 900m));
            books.Add(new Book("Гарри Поттер и философский камень", "Дж. К. Роулинг", Janr.Fantasy, 1997, 950m));

            Console.WriteLine("База данных заполнена тестовыми данными!");
            Console.WriteLine($"Добавлено {books.Count} книг\n");
        }

        static void Add()
        {
            Console.WriteLine("ДОБАВЛЕНИЕ КНИГИ");

            try
            {
                Console.Write("Введите название: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                    throw new Exception("Название не может быть пустым");

                Console.Write("Введите автора: ");
                string author = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(author))
                    throw new Exception("Автор не может быть пустым");

                Console.WriteLine("Выберите жанр:");
                Console.WriteLine("0 - Fantasy, 1 - Nayka, 2 - Detectiv, 3 - Horror, 4 - Comedy");
                Console.Write("Жанр (0-4): ");
                int janrInput = int.Parse(Console.ReadLine());
                if (janrInput < 0 || janrInput > 4)
                    throw new Exception("Неверный номер жанра");
                Janr janr = (Janr)janrInput;

                Console.Write("Введите год издания: ");
                int year = Convert.ToInt32(Console.ReadLine());
                if (year < 1000 || year > 2025)
                    throw new Exception("Неверный год издания");

                Console.Write("Введите цену: ");
                decimal price = Convert.ToDecimal(Console.ReadLine());
                if (price < 0)
                    throw new Exception("Цена не может быть отрицательной");

                Book newBook = new Book(title, author, janr, year, price);
                books.Add(newBook);

                Console.WriteLine("Книга добавлена:");
                newBook.DisplayInfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void Delete()
        {
            Console.WriteLine("УДАЛЕНИЕ КНИГИ");

            if (books.Count == 0)
            {
                Console.WriteLine("Список книг пуст!");
                return;
            }
            DisplayAllBooks();
            Console.Write("Введите ID книги для удаления: ");

            try
            {
                int id = Convert.ToInt32(Console.ReadLine());
                Book toRemove = null;
                foreach (Book book in books)
                {
                    if (book.id == id)
                    {
                        toRemove = book;
                        break;
                    }
                }

                if (toRemove != null)
                {
                    books.Remove(toRemove);
                    Console.WriteLine($"Книга '{toRemove.title}' удалена!");
                }
                else
                {
                    Console.WriteLine("Книга с таким ID не найдена!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void Find()
        {
            Console.WriteLine("ПОИСК КНИГ");
            Console.WriteLine("1 - Поиск по названию");
            Console.WriteLine("2 - Поиск по автору");
            Console.WriteLine("3 - Поиск по жанру");
            Console.Write("Выберите тип поиска: ");

            string searchType = Console.ReadLine();

            switch (searchType)
            {
                case "1":
                    SearchByTitle();
                    break;
                case "2":
                    SearchByAuthor();
                    break;
                case "3":
                    SearchByJanr();
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }

        static void SearchByTitle()
        {
            Console.WriteLine("ПОИСК ПО НАЗВАНИЮ");
            Console.Write("Введите название книги: ");
            string title = Console.ReadLine().ToLower();

            var foundBooks = books.Where(b => b.title.ToLower().Contains(title))
                                 .OrderBy(b => b.title);

            if (foundBooks.Any())
            {
                foreach (var book in foundBooks)
                {
                    book.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("Книги не найдены!");
            }
        }

        static void SearchByAuthor()
        {
            Console.WriteLine("ПОИСК ПО АВТОРУ");
            Console.Write("Введите автора: ");
            string author = Console.ReadLine().ToLower();

            var foundBooks = books.Where(b => b.author.ToLower().Contains(author))
                                 .OrderBy(b => b.author);

            if (foundBooks.Any())
            {
                foreach (var book in foundBooks)
                {
                    book.DisplayInfo();
                }
            }
            else
            {
                Console.WriteLine("Книги не найдены!");
            }
        }

        static void SearchByJanr()
        {
            Console.WriteLine("ПОИСК ПО ЖАНРУ");
            Console.WriteLine("0 - Fantasy");
            Console.WriteLine("1 - Nayka");
            Console.WriteLine("2 - Detectiv");
            Console.WriteLine("3 - Horror");
            Console.WriteLine("4 - Comedy");
            Console.Write("Выберите жанр (0-4): ");

            if (int.TryParse(Console.ReadLine(), out int janrInput) && janrInput >= 0 && janrInput <= 4)
            {
                Janr janr = (Janr)janrInput;
                var foundBooks = books.Where(b => b.janr == janr)
                                     .OrderBy(b => b.title);

                if (foundBooks.Any())
                {
                    foreach (var book in foundBooks)
                    {
                        book.DisplayInfo();
                    }
                }
                else
                {
                    Console.WriteLine("Книги не найдены!");
                }
            }
            else
            {
                Console.WriteLine("Неверный жанр!");
            }
        }

        static void Sort()
        {
            Console.WriteLine("СОРТИРОВКА КНИГ");
            Console.WriteLine("1 - Сортировка по названию");
            Console.WriteLine("2 - Сортировка по году издания");
            Console.WriteLine("3 - Сортировка по цене");
            Console.WriteLine("4 - Сортировка по автору");
            Console.Write("Выберите тип сортировки: ");

            string sortType = Console.ReadLine();

            List<Book> sortedBooks = new List<Book>();

            switch (sortType)
            {
                case "1":
                    sortedBooks = books.OrderBy(b => b.title).ToList();
                    Console.WriteLine("Книги отсортированы по названию:");
                    break;
                case "2":
                    sortedBooks = books.OrderBy(b => b.year).ToList();
                    Console.WriteLine("Книги отсортированы по году издания:");
                    break;
                case "3":
                    sortedBooks = books.OrderBy(b => b.price).ToList();
                    Console.WriteLine("Книги отсортированы по цене:");
                    break;
                case "4":
                    sortedBooks = books.OrderBy(b => b.author).ToList();
                    Console.WriteLine("Книги отсортированы по автору:");
                    break;
                default:
                    Console.WriteLine("Неверный выбор!");
                    return;
            }

            foreach (var book in sortedBooks)
            {
                book.DisplayInfo();
            }
        }

        static void Price()
        {
            Console.WriteLine("САМАЯ ДОРОГАЯ И ДЕШЕВАЯ КНИГА");

            if (!books.Any())
            {
                Console.WriteLine("В библиотеке нет книг");
                return;
            }

            var mostExpensive = books.OrderByDescending(b => b.price).First();
            var cheapest = books.OrderBy(b => b.price).First();

            Console.WriteLine("Самая дорогая книга:");
            mostExpensive.DisplayInfo();

            Console.WriteLine("Самая дешевая книга:");
            cheapest.DisplayInfo();
        }

        static void Authors()
        {
            Console.WriteLine("КОЛИЧЕСТВО КНИГ ПО АВТОРАМ");

            if (!books.Any())
            {
                Console.WriteLine("В библиотеке нет книг");
                return;
            }

            var authorsCount = books.GroupBy(b => b.author)
                                   .Select(g => new { Author = g.Key, Count = g.Count() })
                                   .OrderByDescending(a => a.Count)
                                   .ThenBy(a => a.Author);

            foreach (var author in authorsCount)
            {
                Console.WriteLine($"{author.Author}: {author.Count} книг(и)");
            }
        }

        static void DisplayAllBooks()
        {
            Console.WriteLine("ВСЕ КНИГИ В БИБЛИОТЕКЕ");
            if (books.Count == 0)
            {
                Console.WriteLine("Книг нет в библиотеке");
            }
            else
            {
                var sortedBooks = books.OrderBy(b => b.title);
                foreach (var book in sortedBooks)
                {
                    book.DisplayInfo();
                }
                Console.WriteLine($"\nВсего книг: {books.Count}");
            }
        }

        static void DisplayStatistics()
        {
            Console.WriteLine("СТАТИСТИКА БИБЛИОТЕКИ");

            if (!books.Any())
            {
                Console.WriteLine("В библиотеке нет книг");
                return;
            }

            Console.WriteLine($"Общее количество книг: {books.Count}");
            Console.WriteLine($"Общая стоимость всех книг: {books.Sum(b => b.price):C}");
            Console.WriteLine($"Средняя цена книги: {books.Average(b => b.price):C}");
            Console.WriteLine($"Самая старая книга: {books.OrderBy(b => b.year).First().year} год");
            Console.WriteLine($"Самая новая книга: {books.OrderByDescending(b => b.year).First().year} год");

            Console.WriteLine("\nКоличество книг по жанрам:");
            var genreStats = books.GroupBy(b => b.janr)
                                 .Select(g => new { Genre = g.Key, Count = g.Count() })
                                 .OrderByDescending(g => g.Count)
                                 .ThenBy(g => g.Genre);

            foreach (var genre in genreStats)
            {
                Console.WriteLine($"{genre.Genre}: {genre.Count} книг(и)");
            }

            Console.WriteLine("\nТоп-5 самых дорогих книг:");
            var topExpensive = books.OrderByDescending(b => b.price).Take(5);
            foreach (var book in topExpensive)
            {
                book.DisplayInfo();
            }
        }

        static void DisplayMainMenu()
        {
            Console.WriteLine("\n" + new string('=', 40));
            Console.WriteLine("         БИБЛИОТЕКА КНИГ");
            Console.WriteLine(new string('=', 40));
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Найти книги");
            Console.WriteLine("4. Сортировать книги");
            Console.WriteLine("5. Самая дорогая/дешевая книга");
            Console.WriteLine("6. Количество книг по авторам");
            Console.WriteLine("7. Показать все книги");
            Console.WriteLine("8. Статистика библиотеки");
            Console.WriteLine("9. Заполнить тестовыми данными");
            Console.WriteLine("0. Выход");
            Console.WriteLine(new string('=', 40));
            Console.Write("Ваш выбор: ");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в систему управления библиотекой!");

            // Автоматическое заполнение тестовыми данными при запуске
            InitializeTestData();

            while (true)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Add();
                        break;
                    case "2":
                        Delete();
                        break;
                    case "3":
                        Find();
                        break;
                    case "4":
                        Sort();
                        break;
                    case "5":
                        Price();
                        break;
                    case "6":
                        Authors();
                        break;
                    case "7":
                        DisplayAllBooks();
                        break;
                    case "8":
                        DisplayStatistics();
                        break;
                    case "9":
                        InitializeTestData();
                        break;
                    case "0":
                        Console.WriteLine("Спасибо за использование системы! До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }
}