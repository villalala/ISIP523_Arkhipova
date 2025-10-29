using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    class predmet
    {
        public string nazvanie;
        public string tip;
        public int ataka;
        public int zaschita;

        public predmet(string nazvanie, string tip, int atk, int def)
        {
            this.nazvanie = nazvanie;
            this.tip = tip;
            ataka = atk;
            zaschita = def;
        }
        public override string ToString()
        {
            if (tip == "оружие")
                return $"{nazvanie}; атака: {ataka}";
            if (tip == "доспехи")
                return $"{nazvanie}; защита: {zaschita}";
            else
                return nazvanie;
        }
    }

    class player
    {
        public int maxHP;
        public int nowHP;
        public predmet oruzie;
        public predmet dospehi;
        public bool zamorozka;
        public bool zhiv => nowHP > 0;
        public int ataka => oruzie.ataka;
        public int zachita => dospehi.zaschita;

        public player()
        {
            maxHP = 100;
            nowHP = maxHP;
            oruzie = new predmet("Руки", "оружие", 5, 0);
            dospehi = new predmet("Кожанные штаны", "доспехи", 0, 2);
            zamorozka = false;
        }

        public void poluchenieUrona(int uron)
        {
            nowHP = Math.Max(0, nowHP - uron);
        }

        public void lechenie()
        {
            nowHP = maxHP;
        }

        public override string ToString()
        {
            return $"Игрок (HP: {nowHP}/{maxHP}, Атака: {ataka}, Защита: {zachita})";
        }
    }

    class zlodei
    {
        public int maxHP;
        public int nowHP;
        public string name;
        public int ataka;
        public int zachita;
        public double shansKrita;
        public double shansZamorozki;
        public bool ignorZachita;
        public bool zhiv => nowHP > 0;

        public zlodei(string nazvanie, int hp, int atk, int zachit)
        {
            this.name = nazvanie;
            maxHP = hp;
            nowHP = maxHP;
            ataka = atk;
            zachita = zachit;
            shansKrita = 0;
            shansZamorozki = 0;
            ignorZachita = false;
        }
        public void poluchenieUrona(int uron)
        {
            nowHP = Math.Max(0, nowHP - uron);
        }

        public override string ToString()
        {
            return $"{name}; атака: {ataka}, защита: {zachita}";
        }
    }

    class game
    {
        private int schetHodov;
        public player igrok;
        public Random rand;

        private List<zlodei> vrag;
        private List<predmet> oruzie;
        private List<predmet> dospehi;

        private game()
        {
            schetHodov = 0;
            igrok = new player();
            rand = new Random();
            InicialVragi();
            InicialPredmeti();
        }

        private void InicialVragi()
        {
            vrag = new List<zlodei>
        {
            new zlodei("Гоблин", 30, 8, 3) { shansKrita = 0.2 },
            new zlodei("Скелет", 25, 10, 2) { ignorZachita = true },
            new zlodei("Маг", 20, 12, 1) { shansZamorozki = 0.25 },

            new zlodei("ВВГ (Босс Гоблин)", 60, 12, 4) { shansKrita = 0.3 },
            new zlodei("Ковальский (Босс Скелет)", 63, 13, 3) { ignorZachita = true },
            new zlodei("Архимаг C++ (Босс Маг)", 36, 19, 1) { shansZamorozki = 0.35 },
            new zlodei("Пестов С-- (Босс Скелет)", 33, 18, 1) { ignorZachita = true, shansZamorozki = 0.15 },
            };
        }

        private void InicialPredmeti()
        {
            oruzie = new List<predmet>
            {
                new predmet("Деревянный меч", "oruzhie", 8, 0),
                new predmet("Железный клинок", "oruzhie", 15, 0),
                new predmet("Волшебный посох", "oruzhie", 12, 3),
                new predmet("Топор воина", "oruzhie", 18, 0)
            };
            dospehi = new List<predmet>
            {
                new predmet("Кожаная броня", "dospehi", 0, 5),
                new predmet("Железная броня", "dospehi", 0, 10),
                new predmet("Алмазная броня", "dospehi", 0, 15),
                new predmet("Мантия неведимка", "dospehi", 5, 8)
            };
        }

        public void nachalo()
        {
            Console.WriteLine("Добро пожаловать в самую лучшую игру в вашей жизни!");
            Console.WriteLine("Каждый ход вас ждет либо сундук, либо встреча с врагом.");
            Console.WriteLine("Каждые 10 ходов вас ждет встреча с боссом!\n");

            while (igrok.zhiv)
            {
                schetHodov++;
                Console.WriteLine($"=== Ход {schetHodov} ===");

                if (rand.Next(2) == 0)
                {
                    obrSunduk();
                }
                else
                {
                    obrVragi();
                }

                if (igrok.zhiv)
                {
                    Console.WriteLine("Нажмите Enter для следующего хода...");
                    Console.ReadLine();
                }
            }
            Console.WriteLine($"\nИгра окончена! Вы прошли {schetHodov} ходов.");
        }

        private void obrSunduk()
        {
            Console.WriteLine("Поздравляю! Вы нашли сундук.");

            int tip = rand.Next(3);
            if (tip == 0)
            {
                predmet newOruzie = oruzie[rand.Next(oruzie.Count)];
                Console.WriteLine($"В сундуке {newOruzie}");
                Console.WriteLine($"Ваше текущее оружие: {igrok.oruzie}");
                Console.WriteLine("Взять новое оружие? (да/нет)");
                string vibor = Console.ReadLine();
                if (vibor == "да")
                {
                    igrok.oruzie = newOruzie;
                    Console.WriteLine($"Вы экипировали {newOruzie}");
                }
            }
            else if (tip == 1)
            {
                predmet newDospeh = dospehi[rand.Next(dospehi.Count)];
                Console.WriteLine($"В сундуке {newDospeh}");
                Console.WriteLine($"Ваши текущие доспехи: {igrok.dospehi}");
                Console.WriteLine("Взять новые доспехи? (да/нет)");
                string vibor = Console.ReadLine();
                if (vibor == "да")
                {
                    igrok.dospehi = newDospeh;
                    Console.WriteLine($"Вы надели {newDospeh}");
                }
            }
            else
            {
                Console.WriteLine("Вы нашли лечебное зелье!");
                igrok.lechenie();
                Console.WriteLine("Ваше здоровье полностью вылечено!");
            }
        }

        private void obrVragi()
        {
            zlodei zlodei;
            if (schetHodov % 10 == 0)
            {
                zlodei = vrag[rand.Next(3, 7)];
                Console.WriteLine($"Вы встретили босса {zlodei.name}!");
            }
            else
            {
                zlodei = vrag[rand.Next(3)];
                Console.WriteLine($"Вы встретили врага {zlodei.name}");
            }

            bitva(zlodei);
        }

        private void bitva(zlodei vrag)
        {
            bool igrokZaschita = false;

            while (igrok.zhiv && vrag.zhiv)
            {
                if (!igrok.zamorozka)
                {
                    Console.WriteLine($"{igrok}");
                    Console.WriteLine("1 - Атаковать");
                    Console.WriteLine("2 - Защищаться");
                    Console.Write("Ваш выбор: ");

                    string vibor = Console.ReadLine();

                    if (vibor == "1")
                    {
                        int uron = igrok.ataka;
                        vrag.poluchenieUrona(uron);
                        Console.WriteLine($"Вы атакуете и наносите {uron} урона!");
                        igrokZaschita = false;
                    }
                    else if (vibor == "2")
                    {
                        Console.WriteLine("Вы готовитесь к защите...");
                        igrokZaschita = true;
                    }
                    else
                    {
                        Console.WriteLine("Неверный ввод, вы пропускаете ход!");
                        igrokZaschita = false;
                    }
                }
                else
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    igrok.zamorozka = false;
                    igrokZaschita = false;
                }

                if (!vrag.zhiv)
                {
                    Console.WriteLine($"Вы победили {vrag.name}!");
                    break;
                }
            }

            }
        }


        internal class Program
        {
            static void Main(string[] args)
            {

            }
        }
    }
}

