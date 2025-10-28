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

        public void lechenie (int zdorovie)
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

    internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
