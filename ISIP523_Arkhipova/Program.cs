using System;
using System.Collections.Generic;
using System.Linq;

namespace ISIP523_Arkhipova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int carsServiced = 0;
            List<(int detalID, int Quantity, int Remaining)> pendingDeliveries = new List<(int detalID, int Quantity, int Remaining)>();

            Console.WriteLine("Добро пожаловать в автосервис!");
            Console.Write("Введите имя игрока: ");
            string playerName = Console.ReadLine();

            var player = new player
            {
                name = playerName,
                balans = 50000,
                pochinki = 0
            };
            CorePR7.Context.player.Add(player);
            CorePR7.Context.SaveChanges();

            Console.WriteLine($"Игрок {player.name} создан! Баланс: {player.balans}");

            if (!CorePR7.Context.detal.Any())
            {
                CorePR7.Context.detal.Add(new detal { name = "Двигатель", price = 10000 });
                CorePR7.Context.detal.Add(new detal { name = "Тормоза", price = 3000 });
                CorePR7.Context.detal.Add(new detal { name = "Фары", price = 2000 });
                CorePR7.Context.SaveChanges();
                Console.WriteLine("Базовые детали добавлены в базу данных.");
            }
            else
            {
                Console.WriteLine("Детали уже есть в базе данных.");
            }

            var detals = CorePR7.Context.detal.ToList();
            foreach (var det in detals)
            {
                var zakaz = new zakaz
                {
                    ID_detal = det.ID_detal,
                    kol_vo = 1
                };
                CorePR7.Context.zakaz.Add(zakaz);
                CorePR7.Context.SaveChanges();

                CorePR7.Context.sklad.Add(new sklad
                {
                    ID_zakaz = zakaz.ID_zakaz,
                    ID_player = player.ID_player,
                    kol_vo = 1
                });
            }
            CorePR7.Context.SaveChanges();

            bool game = true;
            while (game)
            {
                Console.WriteLine("\nМЕНЮ:");
                Console.WriteLine("1. Новый клиент");
                Console.WriteLine("2. Закупить детали");
                Console.WriteLine("3. Проверить доставки");
                Console.WriteLine("4. Выход");
                Console.WriteLine($"Баланс: {player.balans}");
                Console.Write("Ваш выбор: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ServiceClient(player, ref game, ref carsServiced, pendingDeliveries);
                        break;
                    case "2":
                        Purchasedetals(player, pendingDeliveries);
                        break;
                    case "3":
                        CheckPendingDeliveries(player, pendingDeliveries);
                        break;
                    case "4":
                        game = false;
                        break;
                    default:
                        Console.WriteLine("Неверный пункт меню.");
                        break;
                }
            }
        }

        