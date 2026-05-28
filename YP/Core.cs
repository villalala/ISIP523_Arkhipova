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
        public static Users currentUser { get; set; } // текущий пользователь 
    }
    public partial class Book
    {
        public double AverageRating // средний рейтинг вычисление 
        {
            get
            {
                if (Reviews == null || !Reviews.Any()) return 0;
                return Reviews.Average(r => r.rating);
            }
        }
    }
}