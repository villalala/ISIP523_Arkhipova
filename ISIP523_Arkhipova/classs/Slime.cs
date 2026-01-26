using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ISIP523_Arkhipova
{
    class Slime : zlodei
    {
        public Slime()
        {
            Name = "Слизень";
            MaxHP = 25;
            HP = MaxHP;
            attack = 2;
            defense = 0;
        }

        public override void TakeDamage(int damage)
        {
            int actualDamage = Math.Max(0, damage - 2);
            if (damage > actualDamage)
                Console.WriteLine("Слизень поглощает 2 урона!");

            HP -= actualDamage;
        }

        public override void AttackPlayer(Player player)
        {
            int damage = attack;
            if (player.isDefending && Random.NextDouble() < 0.4)
            {
                Console.WriteLine($"{player.Name} уклоняется от атаки!");
                return;
            }

            damage = Math.Max(0, damage - player.equippedarmor.defense);
            player.HP -= damage;
            Console.WriteLine($"{Name} наносит {damage} урона {player.Name}!");
        }
    }
}
