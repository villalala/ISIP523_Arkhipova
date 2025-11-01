using System;
using System.Collections.Generic;

namespace ISIP523_Arkhipova
{
    class Program
    {
        static Random rand = new Random();

        enum Sunduk
        {
            derevyanniy_mec = 1,
            jelezniy_klinok,
            volshebniy_posoh,
            topor_voina,
            kojanaya_bronya,
            jeleznaya_bronya,
            almaznaya_bronya,
            mantia_nevidimka,
            lechebnoe_zele
        }

        class player
        {
            public double nowHP;
            public double ataka;
            public double zachita;

            public player(double nowHP, double ataka, double zachita)
            {
                this.nowHP = nowHP;
                this.ataka = ataka;
                this.zachita = zachita;
            }
        }

        class zlodei
        {
            public string name;
            public double nowHP;
            public double ataka;
            public double zachita;

            public zlodei(string name, double nowHP, double ataka, double zachita)
            {
                this.name = name;
                this.nowHP = nowHP;
                this.ataka = ataka;
                this.zachita = zachita;
            }

            public virtual double uronIgroku(player igrok)
            {
                double uron = ataka - igrok.zachita;
                if (uron < 0)
                {
                    uron = 0;
                }
                return uron;
            }
        }

        class Goblin : zlodei
        {
            public double shansKrita;
            public Goblin(string name = "Гоблин", double nowHP = 30, double ataka = 8, double zachita = 3, double shansKrita = 20)
                : base(name, nowHP, ataka, zachita)
            {
                this.shansKrita = shansKrita;
            }

            public override double uronIgroku(player igrok)
            {
                double uron = ataka - igrok.zachita;
                if (rand.Next(100) < shansKrita)
                {
                    uron = uron * 2;
                    Console.WriteLine("Крит. удар!");
                }
                if (uron < 0)
                {
                    uron = 0;
                }
                return uron;
            }
        }

        class Sceleton : zlodei
        {
            public Sceleton(string name = "Скелет", double nowHP = 25, double ataka = 10, double zachita = 2)
                 : base(name, nowHP, ataka, zachita) { }

            public override double uronIgroku(player igrok)
            {
                double uron = ataka;
                if (uron < 0)
                {
                    uron = 0;
                }
                return uron;
            }
        }

        class Mage : zlodei
        {
            public double shansZamorozki;
            public bool zamorojen = false;

            public Mage(string name = "Маг", double nowHP = 20, double ataka = 12, double zachita = 1, double shansZamorozki = 25)
                : base(name, nowHP, ataka, zachita)
            {
                this.shansZamorozki = shansZamorozki;
            }

            public bool zamorozitIgroka()
            {
                bool holod = rand.Next(100) < shansZamorozki;
                return holod;
            }
        }

        class VVG : Goblin
        {
            public VVG(string name = "ВВГ - босс гоблинов", double nowHP = 60, double ataka = 12, double zachita = 4, double shansKrita = 30)
                : base(name, nowHP, ataka, zachita, shansKrita)
            {
            }
        }

        class Kovalski : Sceleton
        {
            public Kovalski(string name = "Ковальский - босс скелетов", double nowHP = 63, double ataka = 13, double zachita = 3)
                : base(name, nowHP, ataka, zachita)
            {
            }
        }

        class ArhiMaks : Mage
        {
            public ArhiMaks(string name = "Архимаг C++ - босс магов", double nowHP = 36, double ataka = 19, double zachita = 1, double shansZamorozki = 35)
                : base(name, nowHP, ataka, zachita, shansZamorozki)
            {
            }
        }

        class Pestov : Sceleton
        {
            public double shansZamorozki;
            public bool zamorojen = false;

            public Pestov(string name = "Пестов С-- - босс скелетов", double nowHP = 33, double ataka = 18, double zachita = 1, double shansZamorozki = 15)
                : base(name, nowHP, ataka, zachita)
            {
                this.shansZamorozki = shansZamorozki;
            }

            public bool zamorozitIgroka()
            {
                bool holod = rand.Next(100) < shansZamorozki;
                return holod;
            }
        }

        static zlodei viborVraga()
        {
            int ch = rand.Next(3);
            switch (ch)
            {
                case 0: return new Goblin();
                case 1: return new Sceleton();
                case 2: return new Mage();
                default: return new Mage();
            }
        }

        static zlodei viborBossa()
        {
            int ch = rand.Next(4);
            switch (ch)
            {
                case 0: return new VVG();
                case 1: return new Kovalski();
                case 2: return new ArhiMaks();
                case 3: return new Pestov();
                default: return new ArhiMaks();
            }
        }

        static void smenaDospehov(player igrok, double novayaZaschita)
        {
            Console.WriteLine($"Текущая защита - {igrok.zachita}.");
            Console.WriteLine("Вы хотите сменить броню? (да/нет)");
            string otvet = Console.ReadLine().ToLower();

            if (otvet == "да")
            {
                igrok.zachita = novayaZaschita;
                Console.WriteLine($"Экипирована новая броня! Текущая защита - {igrok.zachita}");
            }
            else
            {
                Console.WriteLine($"Вы оставили свою броню.");
            }
        }

        static void smenaOruzhia(player igrok, double novayaAtaka)
        {
            Console.WriteLine($"Ваша текущая атака - {igrok.ataka}.");
            Console.WriteLine("Вы хотите сменить оружие? (да/нет)");
            string otvet = Console.ReadLine().ToLower();

            if (otvet == "да")
            {
                igrok.ataka = novayaAtaka;
                Console.WriteLine($"Экипировано новое оружие! Текущая атака - {igrok.ataka} ");
            }
            else
            {
                Console.WriteLine($"Вы оставили своё оружие.");
            }
        }

        static void otkritSunduk(player igrok)
        {
            Sunduk loot = (Sunduk)rand.Next(1, 10);
            switch (loot)
            {
                case Sunduk.derevyanniy_mec:
                    Console.WriteLine("Вам выпал деревянный меч! +8 к атаке.");
                    smenaOruzhia(igrok, 8);
                    break;

                case Sunduk.jelezniy_klinok:
                    Console.WriteLine("Вам выпал железный клинок! +15 к атаке.");
                    smenaOruzhia(igrok, 15);
                    break;

                case Sunduk.volshebniy_posoh:
                    Console.WriteLine("Вам выпал волшебный посох! +12 к атаке.");
                    smenaOruzhia(igrok, 12);
                    break;

                case Sunduk.topor_voina:
                    Console.WriteLine("Вам выпал топор воина! +18 к атаке.");
                    smenaOruzhia(igrok, 18);
                    break;

                case Sunduk.kojanaya_bronya:
                    Console.WriteLine("Вам выпала кожаная броня! +5 к защите.");
                    smenaDospehov(igrok, 5);
                    break;

                case Sunduk.jeleznaya_bronya:
                    Console.WriteLine("Вам выпала железная броня! +10 к защите.");
                    smenaDospehov(igrok, 10);
                    break;

                case Sunduk.almaznaya_bronya:
                    Console.WriteLine("Вам выпала алмазная броня! +15 к защите.");
                    smenaDospehov(igrok, 15);
                    break;

                case Sunduk.mantia_nevidimka:
                    Console.WriteLine("Вам выпала мантия невидимка! +8 к защите.");
                    smenaDospehov(igrok, 8);
                    break;

                case Sunduk.lechebnoe_zele:
                    Console.WriteLine("Вам выпало лечебное зелье! ХП полностью восстановлены.");
                    igrok.nowHP = 100;
                    break;
            }
        }

        static void bitva(player igrok, zlodei vrag)
        {
            Console.WriteLine($"Вам повстречался враг {vrag.name} !");

            bool igrokZaschita = false;
            bool zamorozka = false;

            while (igrok.nowHP > 0 && vrag.nowHP > 0)
            {
                if (!zamorozka)
                {
                    Console.WriteLine("Ход игрока: выберите действие:");
                    Console.WriteLine("1 - Атака");
                    Console.WriteLine("2 - Защита (40% уклонение, иначе блок 70–100% от брони)");

                    string deistvie = Console.ReadLine();
                    igrokZaschita = deistvie == "2";

                    if (!igrokZaschita)
                    {
                        double uronIgroka = igrok.ataka - vrag.zachita;
                        if (uronIgroka < 0) uronIgroka = 0;

                        vrag.nowHP -= uronIgroka;
                        if (vrag.nowHP < 0)
                            vrag.nowHP = 0;
                        Console.WriteLine($"Вы атаковали! Нанесено {uronIgroka} урона. У {vrag.name} осталось {vrag.nowHP} HP.");
                    }
                    else
                    {
                        Console.WriteLine("Вы заняли оборону! Готовитесь к удару врага...");
                    }
                }
                else
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    zamorozka = false;
                    igrokZaschita = false;
                }

                if (vrag.nowHP <= 0)
                {
                    Console.WriteLine($"{vrag.name} повержен!");
                    return;
                }

                Console.WriteLine($"\nХод {vrag.name}:");
                double uronVraga = vrag.uronIgroku(igrok);
                string soobshenie = $"{vrag.name} атакует. Урон: {uronVraga}";

                if (vrag is Mage mag)
                {
                    if (mag.zamorozitIgroka())
                    {
                        zamorozka = true;
                        soobshenie = $"{vrag.name} замораживает вас! Вы пропустите следующий ход.";
                        uronVraga = 0;
                    }
                }
                else if (vrag is Pestov pestov)
                {
                    if (pestov.zamorozitIgroka())
                    {
                        zamorozka = true;
                        soobshenie = $"{vrag.name} замораживает вас! Вы пропустите следующий ход.";
                        uronVraga = 0;
                    }
                }

                Console.WriteLine(soobshenie);

                if (igrokZaschita)
                {
                    if (rand.Next(100) < 40)
                    {
                        Console.WriteLine("Вы уклонились от атаки!");
                        uronVraga = 0;
                    }
                    else
                    {
                        double silaBloka = igrok.zachita * (rand.Next(70, 101) / 100.0);
                        uronVraga -= silaBloka;
                        if (uronVraga < 0) uronVraga = 0;

                        Console.WriteLine($"Блок! Урон снижен бронёй ({silaBloka}).");
                    }
                    igrokZaschita = false;
                }

                igrok.nowHP -= uronVraga;
                Console.WriteLine($"Вы получили {uronVraga} урона. ");
                Console.WriteLine($"Здоровье: {igrok.nowHP} HP");

                if (igrok.nowHP <= 0)
                {
                    Console.WriteLine("Игрок погиб... Игра окончена.");
                    return;
                }

                if (igrok.nowHP > 0 && vrag.nowHP > 0)
                {
                    Console.WriteLine($"\nПосле раунда:");
                    Console.WriteLine($"Игрок: {igrok.nowHP} HP");
                    Console.WriteLine($"{vrag.name}: {vrag.nowHP} HP");
                }
            }
        }

        static void Main(string[] args)
        {
            player igrok = new player(100, 5, 2);

            Console.WriteLine("Добро пожаловать в лучшую текстовую игру в вашей жизни!");
            Console.WriteLine("Каждый ход вас ждет либо сундук, либо встреча с врагом!");
            Console.WriteLine("Каждые 10 ходов вас ждет встреча с боссом!\n");
            Console.WriteLine($"Ваше снаряжение: оружие - \"Руки\" урон: 5; доспехи - \"Кожанные штаны\" защита: 2");

            int schetHodov = 1;

            while (igrok.nowHP > 0)
            {
                Console.WriteLine($"Ход {schetHodov}");

                Console.WriteLine($"Здоровье: {igrok.nowHP} HP");
                Console.WriteLine($"Атака - {igrok.ataka}");
                Console.WriteLine($"Защита - {igrok.zachita}");

                if (rand.Next(2) == 0)
                {
                    if (schetHodov % 10 == 0)
                    {
                        Console.WriteLine("ВНИМАНИЕ! Появился босс!");

                        zlodei boss = viborBossa();
                        bitva(igrok, boss);
                    }
                    else
                    {
                        zlodei vrag = viborVraga();
                        bitva(igrok, vrag);
                    }

                    if (igrok.nowHP <= 0)
                    {
                        Console.WriteLine("Вы погибли... Игра окончена!");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Вы нашли сундук! Открыть? (да/нет)");

                    string otvet = Console.ReadLine().ToLower();
                    if (otvet == "да") otkritSunduk(igrok);
                    else Console.WriteLine("Вы прошли мимо сундука.");
                }

                schetHodov++;

                if (igrok.nowHP > 0)
                {
                    Console.WriteLine("\nНажмите Enter для следующего хода...");
                    Console.ReadLine();
                }
            }

            Console.WriteLine($"\nИгра окончена! Вы прошли {schetHodov - 1} ходов.");
            Console.WriteLine("Спасибо за игру!");
        }
    }
}