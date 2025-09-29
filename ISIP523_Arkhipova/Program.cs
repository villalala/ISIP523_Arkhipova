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
            static void analizNew()
            {
                Console.WriteLine("Введите текст (минимум 100 символов):");
                string inputText = Console.ReadLine();

                if (inputText == null || inputText.Length < 100)
                {
                    Console.WriteLine("Ошибка: текст должен содержать минимум 100 символов.");
                    return;
                }

                Text stats = new Text();
                stats.text = inputText;

                stats.word = Word(inputText);
                stats.shortt = Short(inputText);
                stats.predlozen = Predloz(inputText);
                stats.glasnie = Glasn(inputText);
                stats.soglasn = Soglasn(inputText);
                stats.longg = Longg(inputText);
                stats.povtor = Povtor(inputText);

                all.Add(stats);

                result(stats);
            }

            static int Word(string text)
            {
                string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                return words.Length;
            }

            static string Short(string text)
            {
                string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string shortest = "";
                int minLength = int.MaxValue;

                foreach (string word in words)
                {
                    string cleanWord = clean(word);
                    if (cleanWord.Length > 0 && cleanWord.Length < minLength)
                    {
                        minLength = cleanWord.Length;
                        shortest = cleanWord;
                    }
                }
                return shortest;
            }

            static int Predloz(string text)
            {
                int count = 0;
                string[] sentences = text.Split(new char[] { '.', '!', '?', '…' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string sentence in sentences)
                {
                    if (!empty(sentence))
                    {
                        count++;
                    }
                }

                return count;
            }


            static void Main(string[] args)
        {

        }
    }
}
