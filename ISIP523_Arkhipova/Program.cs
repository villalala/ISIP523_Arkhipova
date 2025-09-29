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

        private static List<Text> all = new List<Text>();

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

                showResult(stats);
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

            static int Glasn(string text)
            {
                int count = 0;
                string lowerText = text.ToLower();

                foreach (char c in lowerText)
                {
                    if (isGlasn(c))
                    {
                        count++;
                    }
                }
                return count;
            }

            static int Soglasn(string text)
            {
                int count = 0;
                string lowerText = text.ToLower();

                foreach (char c in lowerText)
                {
                    if (isSoglasn(c))
                    {
                        count++;
                    }
                }
                return count;
            }

            static string Longg(string text)
            {
                string[] words = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string longest = "";
                int maxLength = 0;

                foreach (string word in words)
                {
                    string cleanWord = clean(word);
                    if (cleanWord.Length > maxLength)
                    {
                        maxLength = cleanWord.Length;
                        longest = cleanWord;
                    }
                }
                return longest;
            }

            static string clean(string word)
            {
                char[] punctuation = { '.', ',', '!', '?', ':', ';', '-', '(', ')', '[', ']', '{', '}', '"', '\'' };
                string cleanWord = word.Trim(punctuation);
                return cleanWord;
            }

            static bool empty(string str)
            {
                return string.IsNullOrWhiteSpace(str);
            }

            static bool isGlasn(char c)
            {
                return c == 'а' || c == 'е' || c == 'ё' || c == 'и' || c == 'о' ||
                       c == 'у' || c == 'ы' || c == 'э' || c == 'ю' || c == 'я';
            }

            static bool isSoglasn(char c)
            {
                return c == 'б' || c == 'в' || c == 'г' || c == 'д' || c == 'ж' || c == 'з' ||
                       c == 'й' || c == 'к' || c == 'л' || c == 'м' || c == 'н' || c == 'п' ||
                       c == 'р' || c == 'с' || c == 'т' || c == 'ф' || c == 'х' || c == 'ц' ||
                       c == 'ч' || c == 'ш' || c == 'щ';
            }

            static Dictionary<char, int> Povtor(string text)
            {
                Dictionary<char, int> povtor = new Dictionary<char, int>();
                string lowerText = text.ToLower();

                foreach (char c in lowerText)
                {
                    if (char.IsLetter(c))
                    {
                        if (povtor.ContainsKey(c))
                        {
                            povtor[c]++;
                        }
                        else
                        {
                            povtor[c] = 1;
                        }
                    }
                }
                return povtor;
            }

            static void showResult(Text stats)
            {
                Console.WriteLine("РЕЗУЛЬТАТЫ АНАЛИЗА ТЕКСТА");
                Console.WriteLine($"Общее количество символов: {stats.text.Length}");
                Console.WriteLine($"Количество слов: {stats.word}");
                Console.WriteLine($"Самое короткое слово: '{stats.shortt}'");
                Console.WriteLine($"Количество предложений: {stats.predlozen}");
                Console.WriteLine($"Количество гласных букв: {stats.glasnie}");
                Console.WriteLine($"Количество согласных букв: {stats.soglasn}");
                Console.WriteLine($"Самое длинное слово: '{stats.longg}'");

                Console.WriteLine("Статистика по буквам:");
                if (stats.povtor.Count > 0)
                {
                    foreach (var pair in stats.povtor)
                    {
                        Console.WriteLine($"  '{pair.Key}': {pair.Value} раз");
                    }
                }
            }

            static void showOld()
            {
                if (all.Count == 0)
                {
                    Console.WriteLine("Статистика по прошлым текстам отсутствует.");
                    return;
                }

                Console.WriteLine($"СТАТИСТИКА ПО ПРОШЛЫМ ТЕКСТАМ (всего: {all.Count})");

                for (int i = 0; i < all.Count; i++)
                {
                    Console.WriteLine($"Текст #{i + 1}");
                    Console.WriteLine($"Длина текста: {all[i].text.Length} символов");
                    Console.WriteLine($"Количество слов: {all[i].word}");
                    Console.WriteLine($"Количество предложений: {all[i].predlozen}");
                    Console.WriteLine($"Самое длинное слово: '{all[i].longg}'");
                }
            }


        static void Main(string[] args)
            {

            }
        }
    }
}
