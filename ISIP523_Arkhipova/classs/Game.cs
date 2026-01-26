using System;
using ISIP523_Arkhipova;
using ISIP523_Zhurikhin;
using static ISIP523_Arkhipova.GameFabric;

namespace ISIP523_Zhurikhin
{
    internal class Game
    {
        private Player player;
        private int turnCount = 0;
        private static System.Random random = new System.Random();

        public void start()
        {
            Console.WriteLine("Как вас будут звать?");
            string name = Console.ReadLine() ?? "Герой";

            Console.WriteLine("Выберите стартовое оружие. (1-Меч, 2-Клеймор, 3-Топор)");
            string choice = Console.ReadLine();
            Weapon startWeapon;

            switch (choice)
            {
                case "1":
                    startWeapon = new Sword();
                    break;

                case "2":
                    startWeapon = new Claymore();
                    break;

                case "3":
                    startWeapon = new Axe();
                    break;

                default:
                    startWeapon = new Sword();
                    break;
            }

            Console.WriteLine("Выберите броню. (1-Тяжелая Броня (+1 урон Меча), 2-Средняя Броня (+1 урон Клеймора), 3-Легкая Броня (+1 урон Топора))");
            string armorChoice = Console.ReadLine();
            armor startArmor;

            switch (armorChoice)
            {
                case "1":
                    startArmor = new armor { name = "Тяжелая Броня", defense = 1, bufftype = "Меч" };
                    break;

                case "2":
                    startArmor = new armor { name = "Средняя Броня", defense = 1, bufftype = "Клеймор" };
                    break;

                case "3":
                    startArmor = new armor { name = "Легкая Броня", defense = 1, bufftype = "Топор" };
                    break;

                default:
                    startArmor = new armor { name = "Тяжелая Броня", defense = 1, bufftype = "Меч" };
                    break;
            }

            player = new Player(name, 20, startWeapon, startArmor);
            RunGame();
        }

        private void RunGame()
        {
            while (player.isAlive)
            {
                turnCount++;
                Console.WriteLine($"\nХод {turnCount}");
                DisplayStats();

                if (turnCount % 5 == 0)
                    FightBoss();
                else if (random.NextDouble() < 0.3)
                    OpenChest();
                else
                    FightEnemy();
                Console.ReadLine();
            }

            Console.WriteLine("Игра окончена!");
        }

        private void DisplayStats()
        {
            Console.WriteLine($"{player.Name}: HP={player.HP}/{player.MaxHP}, Оружие={player.equippedweapon.name} (Прочность={player.equippedweapon.durability})");
        }

        private void FightEnemy()
        {
            zlodei enemy = EnemyFactory.CreateRegular();
            Console.WriteLine($"{enemy.Name} появляется!");
            Fight(player, enemy);
        }

        private void FightBoss()
        {
            zlodei boss = EnemyFactory.CreateBoss();
            Console.WriteLine($"БОСС: {boss.Name} появляется!");
            Fight(player, boss);
        }

        private void Fight(Player player, zlodei enemy)
        {
            while (player.isAlive && enemy.isAlive)
            {
                Console.WriteLine($"{enemy.Name}: HP={enemy.HP}/{enemy.MaxHP}");
                if (!player.isFrozen)
                {
                    Console.WriteLine("Выберите: 1-Аттаковать, 2-Защищаться");
                    string choice = Console.ReadLine()?.ToUpper() ?? "2";
                    player.isDefending = false;

                    if (choice == "1")
                    {
                        player.equippedweapon.durability = Math.Max(1, player.equippedweapon.durability - 1);

                        int damage = player.equippedweapon.dmg;
                        if (player.equippedarmor.bufftype == player.equippedweapon.name)
                            damage += 1;

                        enemy.TakeDamage(damage);
                        Console.WriteLine($"{player.Name} атакует с помощью {player.equippedweapon.name} и наносит {damage} урона!");
                        Console.WriteLine("-------------------------------------");
                    }
                    else
                    {
                        player.isDefending = true;
                        Console.WriteLine($"{player.Name} защищается!");
                    }
                }
                else
                {
                    Console.WriteLine($"{player.Name} заморожен и пропускает ход!");
                    player.isFrozen = false;
                }
                if (enemy.isAlive)
                {
                    enemy.AttackPlayer(player);
                }
            }
            if (!enemy.isAlive)
            {
                Console.WriteLine($"{enemy.Name} повержен!");
                OpenChest();
            }
            else if (!player.isAlive)
            {
                Console.WriteLine("Вы погибли...");
            }
        }

        private void OpenChest()
        {
            Console.WriteLine("Вы нашли сундук!");
            object item;
            int chestReward = random.Next(3);

            switch (chestReward)
            {
                case 0:
                    item = new Zelie();
                    break;
                case 1:
                    int weaponType = random.Next(3);
                    switch (weaponType)
                    {
                        case 0:
                            item = new Sword();
                            break;

                        case 1:
                            item = new Claymore();
                            break;

                        case 2:
                            item = new Axe();
                            break;

                        default:
                            item = new Zelie();
                            break;
                    }
                    break;
                case 2:
                    item = new Zelie();
                    break;
                default:
                    item = new Zelie();
                    break;
            }

            if (item is Zelie food)
            {
                player.HP = Math.Min(player.HP + food.healamount, player.MaxHP);
                Console.WriteLine($"Использовано {food.name}, восстановлено {food.healamount} HP!");
            }
            else if (item is Weapon newWeapon)
            {
                Console.WriteLine($"Найдено {newWeapon.name} (Урон={newWeapon.dmg}, Прочность={newWeapon.durability})");
                Console.WriteLine($"Текущее: {player.equippedweapon.name} (Урон={player.equippedweapon.dmg}, Прочность={player.equippedweapon.durability})");
                Console.WriteLine("Экипировать новое оружие? (Д/Н)");
                if (Console.ReadLine()?.ToUpper() == "Д")
                    player.equippedweapon = newWeapon;
            }
        }
    }
}