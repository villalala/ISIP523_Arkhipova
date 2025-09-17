using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP523_Arkhipova
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Количество операций (от 2 до 40): ");
            int oper = Convert.ToInt32(Console.ReadLine());
            string[] name = new string[oper];
            double [] cena = new double[oper];

            string zapros = "";
            string[] word = new string[oper];
            for (int i = 0; i < oper; i++)
            {

                Console.WriteLine("Введите данные о товаре: ");
                zapros = Console.ReadLine();
                word = zapros.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                word[1] = word[1].Trim();
                name[i] = word[0];
                cena[i] = Convert.ToInt32(word[1]);

            }
            bool aa = true;
            while (aa)
            {
                Console.WriteLine("1. Вывод данных");
                Console.WriteLine("2. Данные (min, max, среднееБ сумма");
                Console.WriteLine("3. Сортировка");
                Console.WriteLine("4. Конвертер валюты");
                Console.WriteLine("5. Поиск по названию");
                Console.WriteLine("0. Выход");
                int pynkt = Convert.ToInt32(Console.ReadLine());

                switch (pynkt)
                {
                    case 1:
                        for (int i = 0; i < oper; i++)
                        {
                            Console.WriteLine("Товар: " + name[i] + " ");
                            Console.WriteLine("Цена " + cena[i]);
                            Console.WriteLine("(" + name[i] + ";" + cena[i] + ")");
                        }
                        break;
                    case 2:
                        int sum = 0;
                        foreach (int i in cena)
                        {
                            sum += i;
                        }
                        int srednee = sum / oper;
                        Console.WriteLine("Среднее: " + srednee);

                        double min = cena[0];
                        double max = cena[0];
                        foreach (int i in cena)
                        {
                            if (i < min)
                                min = i;
                            if (i > max) 
                                max = i;
                        }
                        Console.WriteLine("Минимальное: " + min);
                        Console.WriteLine("Максимальное: " +max);
                        Console.WriteLine("Сумма: " + sum);
                        break;

                     case 3:
                        for (int i = 0;i < oper; i++)
                        {
                            for (int j = oper - 2; j >= i; j--)
                            {
                                if (cena[j] > cena[j + 1])
                                {
                                    double a = cena[j];
                                    string b = name[j];
                                    cena[j] = cena[j + 1];
                                    name[j] = name[j + 1];
                                    cena[j + 1] = a;
                                    name[j + 1] = b;
                                }
                            }
                        }
                        Console.WriteLine("Отсортировано");
                        break;
                        
                     case 4:
                        Console.WriteLine("1. Доллары");
                        Console.WriteLine("2. Евро");
                        Console.WriteLine("3. Свой вариант");
                        int valut = Convert.ToInt32(Console.ReadLine());

                        switch (valut)
                        {
                            case 1:
                                for (int i = 0; i < oper; i++)
                                    cena[i] = cena[i] * 0.012;
                                break;
                            case 2:
                                for (int i = 0; i < oper; i++)
                                    cena[i] = cena[i] * 0.01;
                                break;
                            case 3:
                                double kurs = Convert.ToDouble(Console.ReadLine());
                                for (int i = 0; i < oper; i++)
                                    cena[i] = cena[i] * kurs;
                                break;
                            default: break;
                        }
                            Console.WriteLine("Конвертировано");
                                break;

                        case 5:
                            Console.WriteLine("Введите название товара: ");
                            string tovar = Console.ReadLine();
                        bool poisk = false;
                        for (int i = 0;i < oper; i++)
                        {
                            if(name[i] == tovar)
                            {
                                Console.WriteLine("Товар: " + name[i] + ", цена: " + cena[i]);
                                Console.WriteLine("(" + name[i] + ";" + cena[i] + ")");
                                poisk = true;
                            }
                        }
                        if (!poisk)
                            Console.WriteLine("Товар" + tovar + "не найден");
                        break;

                    case 0:
                        aa = false;
                        break;
                     default : break;
                }
            }
        }
    }
}
