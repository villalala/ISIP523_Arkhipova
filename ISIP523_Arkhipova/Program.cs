using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    class analyz
    {
        struct Text
        {
            public string text;
            public int word;
            public string shortt;
            public int predlozen;
            public int glasnie;
            public int soglasn;
            public string longg;
            public Dictionary<char, int> povtor;
        }

        private static List<Text> allStatistics = new List<Text>();

        internal class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
