using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    internal class Program
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


    }
        static void Main(string[] args)
        {
            
    }
    }
}
