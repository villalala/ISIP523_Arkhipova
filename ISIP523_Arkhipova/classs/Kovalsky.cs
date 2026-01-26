using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ISIP523_Arkhipova
{
     class Kovalsky : Skeleton
    {
        public Kovalsky()
        {
            Name = "Ковальский";
            MaxHP = (int)Math.Round(20 * 2.5);
            HP = MaxHP;
            attack = (int)Math.Round(3 * 1.3);
            defense = (int)Math.Round(2 * 1.4);
        }
    }
}

