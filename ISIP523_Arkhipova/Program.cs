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

        static void ServiceClient(player player, ref bool game, ref int carsServiced,
                                  List<(int detalID, int Quantity, int Remaining)> pendingDeliveries)
        {
            var random = new Random();

            var client = new client
            {
                car = new[] { "Lada", "Toyota", "BMW", "Audi", "Ford" }[random.Next(5)]
            };
            CorePR7.Context.client.Add(client);
            CorePR7.Context.SaveChanges();

            var detals = CorePR7.Context.detal.ToList();
            var brokendetal = detals[random.Next(detals.Count)];

            Console.WriteLine($"Приехал клиент ({client.car}), сломалась деталь: {brokendetal.name}");
            Console.Write("Принять заказ? (да/нет): ");
            if (Console.ReadLine()?.ToLower() != "да")
            {
                decimal fine = 200;
                player.balans -= (int)fine;

                var processRecord = new process
                {
                    ID_client = client.ID_client,
                    ID_player = player.ID_player,
                    status = "Отказано",
                    price = 0,
                    nuzna_detal = brokendetal.ID_detal,
                    postavilli_detal = null
                };
                CorePR7.Context.process.Add(processRecord);
                CorePR7.Context.SaveChanges();
                Console.WriteLine($"Штраф: {fine}. Баланс: {player.balans}");
                return;
            }

            var skladItem = CorePR7.Context.sklad
                .Where(s => s.ID_player == player.ID_player)
                .Join(CorePR7.Context.zakaz, s => s.ID_zakaz, z => z.ID_zakaz, (s, z) => new { s, z })
                .FirstOrDefault(x => x.z.ID_detal == brokendetal.ID_detal && x.s.kol_vo > 0);

            if (skladItem != null)
            {
                skladItem.s.kol_vo--;

                decimal repairPrice = (decimal)(brokendetal.price * 1.5);
                player.balans += (int)repairPrice;
                player.pochinki++;

                var processRecord = new process
                {
                    ID_client = client.ID_client,
                    ID_player = player.ID_player,
                    status = "Выполнен",
                    price = (float)repairPrice,
                    nuzna_detal = brokendetal.ID_detal,
                    postavilli_detal = brokendetal.ID_detal
                };
                CorePR7.Context.process.Add(processRecord);
                CorePR7.Context.SaveChanges();
                Console.WriteLine($"Ремонт выполнен! Баланс: {player.balans}");
            }
            else
            {
                Console.WriteLine("Нет нужной детали. Клиент уехал.");
                player.balans = player.balans - 4000;
                Console.WriteLine($"Штраф: 4000. Баланс: {player.balans}");
                var processRecord = new process
                {
                    ID_client = client.ID_client,
                    ID_player = player.ID_player,
                    status = "Клиент уехал",
                    price = 0,
                    nuzna_detal = brokendetal.ID_detal,
                    postavilli_detal = null
                };
                CorePR7.Context.process.Add(processRecord);
                CorePR7.Context.SaveChanges();
            }

            carsServiced++;

            for (int i = 0; i < pendingDeliveries.Count; i++)
            {
                var delivery = pendingDeliveries[i];
                delivery.Remaining--;
                if (delivery.Remaining <= 0)
                {
                    var zakaz = new zakaz
                    {
                        ID_detal = delivery.detalID,
                        kol_vo = delivery.Quantity
                    };
                    CorePR7.Context.zakaz.Add(zakaz);
                    CorePR7.Context.SaveChanges();

                    var existingSklad = CorePR7.Context.sklad
                        .Where(s => s.ID_player == player.ID_player)
                        .Join(CorePR7.Context.zakaz, s => s.ID_zakaz, z => z.ID_zakaz, (s, z) => new { s, z })
                        .FirstOrDefault(x => x.z.ID_detal == delivery.detalID);

                    if (existingSklad == null)
                    {
                        CorePR7.Context.sklad.Add(new sklad
                        {
                            ID_zakaz = zakaz.ID_zakaz,
                            ID_player = player.ID_player,
                            kol_vo = delivery.Quantity
                        });
                    }
                    else
                    {
                        existingSklad.s.kol_vo += delivery.Quantity;
                    }

                    Console.WriteLine($"Поставка деталей ({delivery.Quantity} шт.) прибыла на склад!");
                    pendingDeliveries.RemoveAt(i);
                    i--;
                }
                else
                {
                    pendingDeliveries[i] = delivery;
                }
            }

            CorePR7.Context.SaveChanges();
        }
        static void Purchasedetals(player player, List<(int detalID, int Quantity, int Remaining)> pendingDeliveries)
        {
            Console.WriteLine("\nДетали для заказа:");
            var detals = CorePR7.Context.detal.ToList();
            for (int i = 0; i < detals.Count; i++)
                Console.WriteLine($"{i + 1}. {detals[i].name} — {detals[i].price}");

            Console.WriteLine("\nВаш склад:");
            var playerSklad = CorePR7.Context.sklad
                .Where(s => s.ID_player == player.ID_player)
                .Join(CorePR7.Context.zakaz, s => s.ID_zakaz, z => z.ID_zakaz, (s, z) => new { s, z })
                .Join(CorePR7.Context.detal, x => x.z.ID_detal, d => d.ID_detal, (x, d) => new { detalName = d.name, Quantity = x.s.kol_vo })
                .ToList();

            if (playerSklad.Count == 0)
            {
                Console.WriteLine("На данный момент на складе ничего нет.");
            }
            else
            {
                foreach (var item in playerSklad)
                {
                    Console.WriteLine($"{item.detalName} — Количество: {item.Quantity}");
                }
            }

            Console.Write("Введите номер детали: ");
            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > detals.Count)
                return;

            var detal = detals[choice - 1];
            Console.Write("Введите количество: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
                return;

            decimal cost = (decimal)detal.price * qty;
            if (player.balans < cost)
            {
                Console.WriteLine("Недостаточно средств!");
                return;
            }

            player.balans -= (int)cost;

            pendingDeliveries.Add((detal.ID_detal, qty, 2));
            CorePR7.Context.SaveChanges();

            Console.WriteLine($"Заказано {qty} шт. {detal.name} за {cost}. Прибудет после 2 клиентов.");
        }

        static void CheckPendingDeliveries(player player, List<(int detalID, int Quantity, int Remaining)> pendingDeliveries)
        {
            if (pendingDeliveries.Count == 0)
            {
                Console.WriteLine("Нет ожидаемых поставок.");
                return;
            }

            Console.WriteLine("Ожидаемые поставки:");
            foreach (var d in pendingDeliveries)
            {
                var detal = CorePR7.Context.detal.First(p => p.ID_detal == d.detalID);
                Console.WriteLine($"{detal.name} количество: {d.Quantity} — прибудет через {d.Remaining} клиента(ов)");
            }
        }
    }
}
