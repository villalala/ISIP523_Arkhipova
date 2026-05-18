using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YP
{
    internal class Core
    {
        public static YPEntities Context = new YPEntities();
        public static Users currentUser { get; set; }
    }
}
