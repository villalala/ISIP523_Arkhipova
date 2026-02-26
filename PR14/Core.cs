using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PR14
{
    internal class Core
    {
        public static cinemaEntities Context = new cinemaEntities();
        public static User user {  get; set; }
    }
}
