using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int carsServiced = 0; // счётчик обслуженных машин
            List<(int PartID, int Quantity, int Remaining)> pendingDeliveries = new List<(int PartID, int Quantity, int Remaining)>(); // ожидание деталей

            Console.WriteLine("Добро пожаловать в автосервис!");
            Console.Write("Введите имя игрока: ");
            string playername = Console.ReadLine();

            var player = new player
            {
                name = playername,
                balans = 5000,
                pochinki = 0,
                TotalFines = 0
            };
            CorePR7.Context.player.Add(player);
            CorePR7.Context.SaveChanges();

            Console.WriteLine($"Игрок {player.name} создан! Баланс: {player.balans}");

            // базовые детали
            if (!CorePR7.Context.Part.Any())
            {
                CorePR7.Context.Part.Add(new Part { name = "Двигатель", PurchasePrice = 1000, RepairPrice = 1500 });
                CorePR7.Context.Part.Add(new Part { name = "Тормоза", PurchasePrice = 300, RepairPrice = 600 });
                CorePR7.Context.Part.Add(new Part { name = "Фары", PurchasePrice = 200, RepairPrice = 400 });
                CorePR7.Context.SaveChanges();
                Console.WriteLine("Базовые детали добавлены в базу данных.");
            }
            else
            {
                Console.WriteLine("Детали уже есть в базе данных.");
            }

            

