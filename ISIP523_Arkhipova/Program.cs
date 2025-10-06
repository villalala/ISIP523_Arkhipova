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


            static void Main(string[] args)
        {
            
    }
    }
}
