using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace ISIP523_Arkhipova
{
    public static class GameFabric
    {
        public static class EnemyFactory
        {
            public static zlodei CreateRegular()
            {
                int enemyType = Random.Next(4);

                switch (enemyType)
                {
                    case 0:
                        return new Goblin();

                    case 1:
                        return new Skeleton();

                    case 2:
                        return new Mage();

                    case 3:
                        return new Slime();

                    default:
                        return new Goblin();
                }
            }

            public static zlodei CreateBoss()
            {
                int bossType = Random.Next(4);

                switch (bossType)
                {
                    case 0:
                        return new VVG();

                    case 1:
                        return new Kovalsky();

                    case 2:
                        return new Archmage();

                    case 3:
                        return new Pestov();

                    default:
                        return new VVG();
                }
            }
        }
    }
}