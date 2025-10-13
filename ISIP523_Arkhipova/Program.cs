using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Book(string title, string author, Janr janr, int year, double price)
        {
            id = nextId++;
            title = title;
            author = author;
            janr = janr;
            year = year;
            price = price;
        }

        public void Info()
        {
            Console.WriteLine($"ID: {id}, Название: {title}, Автор: {author}, Жанр: {janr}, Год: {year}, Цена: {price:C}");
        }


        internal class Program
        {
            private static List<Book> books = new List<Book>();
            
            static void Add()
            {
                Console.WriteLine("ДОБАВЛЕНИЕ КНИГИ");

                Console.Write("Введите название: ");
                string title = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                    throw new Exception("Название не может быть пустым");

                Console.Write("Введите автора: ");
                string author = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(author))
                    throw new Exception("Автор не может быть пустым");

                Console.WriteLine("Выберите жанр:");
                Console.WriteLine("0 - фантастика, 1 - наука, 2 - детектив, 3 - хоррор, 4 - комедия");
                Console.Write("Жанр (0-5): ");
                Janr janr = (Janr)int.Parse(Console.ReadLine());
                if (janree < 0 || janree > 5)
                    throw new Exception("Неверный номер жанра");
                Genre genre = (Genre)genreInput;

                Console.Write("Введите год издания: ");
                int year = Convert.ToInt32(Console.ReadLine());
                if (year < 1000 || year > 2025)
                    throw new Exception("Неверный год издания");

                Console.Write("Введите цену: ");
                double price = Convert.ToDouble(Console.ReadLine());
                if (price < 0)
                    throw new Exception("Цена не может быть отрицательной");

                Book newBook = new Book(title, author, janr, year, price);
                books.Add(newBook);

                Console.WriteLine("Книга добавлена:");
                newBook.DisplayInfo();

                Console.WriteLine("Книга добавлена:");
            }

            static void Delete()
            {
                static void DeleteBookMenu()
                {
                    Console.WriteLine("УДАЛЕНИЕ КНИГИ");

                    if (books.Count == 0)
                    {
                        Console.WriteLine("Список книг пуст!");
                        return;
                    }

                    DisplayAllBooksMenu();
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
                }
            }

            static void Find()
            {
                Console.WriteLine("ПОИСК КНИГ");
                Console.WriteLine("1 - Поиск по названию");
                Console.WriteLine("2 - Поиск по автору");
                Console.WriteLine("3 - Поиск по жанру");
                Console.Write("Выберите тип поиска: ");

                string search = Console.ReadLine();

                switch (search)
                {
                    case "1":
                        SearchByTitle();
                        break;
                    case "2":
                        SearchByAuthor();
                        break;
                    case "3":
                        SearchByGenre();
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

                bool found = false;
                foreach (Book book in books)
                {
                    if (book.title.ToLower().Contains(title))
                    {
                        book.DisplayInfo();
                        found = true;
                    }
                }
                if (!found) Console.WriteLine("Книги не найдены!");
            }

            static void SearchByAuthor()
            {
                Console.WriteLine("ПОИСК ПО АВТОРУ");
                Console.Write("Введите автора: ");
                string author = Console.ReadLine().ToLower();

                bool found = false;
                foreach (Book book in books)
                {
                    if (book.author.ToLower().Contains(author))
                    {
                        book.DisplayInfo();
                        found = true;
                    }
                }
                if (!found) Console.WriteLine("Книги не найдены!");
            }

            static void SearchByJanr()
            {
                Console.WriteLine("ПОИСК ПО ЖАНРУ");
                Console.WriteLine("0 - Fantasy");
                Console.WriteLine("1 - ScienceFiction");
                Console.WriteLine("2 - Mystery");
                Console.WriteLine("3 - Romance");
                Console.WriteLine("4 - Horror");
                Console.WriteLine("5 - Biography");
                Console.Write("Выберите жанр (0-5): ");

                if (Enum.TryParse(Console.ReadLine(), out Genre genre))
                {
                    bool found = false;
                    foreach (Book book in books)
                    {
                        if (book.genre == genre)
                        {
                            book.DisplayInfo();
                            found = true;
                        }
                    }

                    if (!found) Console.WriteLine("Книги не найдены!");
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
                Console.Write("Выберите тип сортировки: ");

                string sort = Console.ReadLine();

                switch (sortType)
                {
                    case "1":
                        BubbleSortByTitle();
                        break;
                    case "2":
                        BubbleSortByYear();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        return;
                }
                Console.WriteLine("Книги отсортированы:");
                foreach (var book in books)
                {
                    book.DisplayInfo();
                }
            }

            static void SortByTitle()
            {
                for (int i = 0; i < books.Count - 1; i++)
                {
                    for (int j = 0; j < books.Count - i - 1; j++)
                    {
                        if (string.Compare(books[j].title, books[j + 1].title) > 0)
                        {
                            Book temp = books[j];
                            books[j] = books[j + 1];
                            books[j + 1] = temp;
                        }
                    }
                }
            }

            static void SortByYear()
            {
                for (int i = 0; i < books.Count - 1; i++)
                {
                    for (int j = 0; j < books.Count - i - 1; j++)
                    {
                        if (books[j].year > books[j + 1].year)
                        {
                            Book temp = books[j];
                            books[j] = books[j + 1];
                            books[j + 1] = temp;
                        }
                    }
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
                List<Book> sortedBooks = new List<Book>(books);

                for (int i = 0; i < sortedBooks.Count - 1; i++)
                {
                    for (int j = 0; j < sortedBooks.Count - i - 1; j++)
                    {
                        if (sortedBooks[j].price < sortedBooks[j + 1].price)
                        {
                            Book temp = sortedBooks[j];
                            sortedBooks[j] = sortedBooks[j + 1];
                            sortedBooks[j + 1] = temp;
                        }
                    }
                }
                var dorogo = sortedBooks[0];
                var deshego = sortedBooks[sortedBooks.Count - 1];

                Console.WriteLine("Самая дорогая книга:");
                dorogo.DisplayInfo();

                Console.WriteLine("Самая дешевая книга:");
                deshego.DisplayInfo();
            }


            static void Authors()
            {
                Console.WriteLine("КОЛИЧЕСТВО КНИГ ПО АВТОРАМ");

                if (!books.Any())
                {
                    Console.WriteLine("В библиотеке нет книг");
                    return;
                }
                List<(string Author, int Count)> authorsCount = new List<(string, int)>();

                foreach (Book book in books)
                {
                    bool authorFound = false;
                    for (int i = 0; i < authorsCount.Count; i++)
                    {
                        if (authorsCount[i].Author == book.author)
                        {
                            authorsCount[i] = (authorsCount[i].Author, authorsCount[i].Count + 1);
                            authorFound = true;
                            break;
                        }
                    }

                    if (!authorFound)
                    {
                        authorsCount.Add((book.author, 1));
                    }
                }

                for (int i = 0; i < authorsCount.Count - 1; i++)
                {
                    for (int j = 0; j < authorsCount.Count - i - 1; j++)
                    {
                        if (authorsCount[j].Count < authorsCount[j + 1].Count)
                        {
                            var temp = authorsCount[j];
                            authorsCount[j] = authorsCount[j + 1];
                            authorsCount[j + 1] = temp;
                        }
                    }
                }

                foreach (var author in authorsCount)
                {
                    Console.WriteLine($"{author.Author}: {author.Count} книг(и)");
                }
            }

            static void AllBooks()
            {
                Console.WriteLine("ВСЕ КНИГИ В БИБЛИОТЕКЕ");
                if (books.Count == 0)
                {
                    Console.WriteLine("Книг нет в библиотеке");
                }
                else
                {
                    foreach (var book in books)
                    {
                        book.DisplayInfo();
                    }
                    Console.WriteLine($"\nВсего книг: {books.Count}");
                }
            }

            static void Test()
            {
                books.Add(new Book("Властелин Колец", "Дж. Р. Р. Толкин", Genre.Fantasy, 1954, 1200m));
                books.Add(new Book("1984", "Джордж Оруэлл", Genre.ScienceFiction, 1949, 800m));
                books.Add(new Book("Убийство в Восточном экспрессе", "Агата Кристи", Genre.Mystery, 1934, 650m));
                books.Add(new Book("Гордость и предубеждение", "Джейн Остин", Genre.Romance, 1813, 700m));
                books.Add(new Book("Дракула", "Брэм Стокер", Genre.Horror, 1897, 900m));

                Console.WriteLine("Тестовые книги добавлены!");
            }
        }

        static void Main(string[] args)
        {
            private static List<Book> books = new List<Book>();

        static void Main(string[] args)
        {
            AddTestBooks();

            Console.WriteLine("ГЛАВНОЕ МЕНЮ");
            Console.WriteLine("1. Добавить книгу");
            Console.WriteLine("2. Удалить книгу");
            Console.WriteLine("3. Найти книги");
            Console.WriteLine("4. Сортировать книги");
            Console.WriteLine("5. Самая дорогая/дешевая книга");
            Console.WriteLine("6. Количество книг по авторам");
            Console.WriteLine("7. Показать все книги");
            Console.WriteLine("0. Выход");
            Console.Write("Ваш выбор: ");
            Console.WriteLine("Библиотека книг");

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
                        AllBooks();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }

   
    }
}

