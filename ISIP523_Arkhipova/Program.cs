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
        private static string hranenie = "";

        internal class Program
        {
            static void text()
            {
                Console.WriteLine("Введите текст (минимум 100 символов):");
                Console.WriteLine("(Для завершения ввода нажмите Enter два раза)");

                string fullText = "";
                string line;
                int emptyLine = 0;
                bool hasContent = false;

                while (true)
                {
                    line = Console.ReadLine();

                    if (string.IsNullOrEmpty(line))
                    {
                        emptyLine++;
                        if (emptyLine >= 2 && hasContent)
                        {
                            break;
                        }
                        else if (!hasContent)
                        {
                            Console.WriteLine("Пожалуйста, введите текст.");
                            continue;
                        }
                    }
                    else
                    {
                        emptyLine = 0;
                        hasContent = true;

                        if (!string.IsNullOrEmpty(fullText))
                        {
                            fullText += "\n" + line;
                        }
                        else
                        {
                            fullText = line;
                        }
                    }
                }

                if (fullText.Length < 100)
                {
                    Console.WriteLine($"Ошибка: текст должен содержать минимум 100 символов. Сейчас: {fullText.Length} символов.");
                    return;
                }


                if (fullText.Length < 100)
                {
                    Console.WriteLine("Ошибка: текст должен содержать минимум 100 символов.");
                    return;
                }

                hranenie = fullText;
                Console.WriteLine($"Текст успешно сохранен!");
            }

            static void analizNew()
            {
                if (string.IsNullOrEmpty(hranenie))
                {
                    Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                    return;
                }

                Text stats = new Text();
                stats.text = hranenie;

                stats.word = Word(hranenie);
                stats.shortt = Short(hranenie);
                stats.predlozen = Predloz(hranenie);
                stats.glasnie = Glasn(hranenie);
                stats.soglasn = Soglasn(hranenie);
                stats.longg = Longg(hranenie);
                stats.povtor = Povtor(hranenie);

                all.Add(stats);

                showResult(stats);
            }

            static int Word(string text)
            {
                char[] separators = { ' ', '\n', '\r', '\t' };
                string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                return words.Length;
            }

            static string Short(string text)
            {
                char[] separators = { ' ', '\n', '\r', '\t' };
                string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                string shortest = "";
                int minLength = int.MaxValue;

                foreach (string word in words)
                {
                    string cleanWord = clean(word);
                    if (cleanWord.Length < minLength)
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
                char[] separators = { ' ', '\n', '\r', '\t' };
                string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);
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
                char[] punctuation = { '.', ',', '!', '?', ':', ';', '-', '(', ')', '[', ']', '{', '}', '"', '\'', '\n', '\r', '\t' };
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
                bool a = true;

                while (a)
                {
                    Console.WriteLine("\nМЕНЮ");
                    Console.WriteLine("1 - Ввод нового текста");
                    Console.WriteLine("2 - Подсчёт количества слов");
                    Console.WriteLine("3 - Поиск самого короткого слова");
                    Console.WriteLine("4 - Подсчёт количества предложений");
                    Console.WriteLine("5 - Подсчёт количества гласных букв");
                    Console.WriteLine("6 - Подсчёт количества согласных букв");
                    Console.WriteLine("7 - Поиск самого длинного слова");
                    Console.WriteLine("8 - Статистика по частоте букв");
                    Console.WriteLine("9 - Полный анализ текста");
                    Console.WriteLine("10 - Просмотр статистики по прошлым текстам");
                    Console.WriteLine("0 - Выход");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            text();
                            break;
                        case "2":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine($"Количество слов: {Word(hranenie)}");
                            break;
                        case "3":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine($"Самое короткое слово: '{Short(hranenie)}'");
                            break;
                        case "4":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine($"Количество предложений: {Predloz(hranenie)}");
                            break;
                        case "5":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine($"Количество гласных букв: {Glasn(hranenie)}");
                            break;
                        case "6":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine($"Количество согласных букв: {Soglasn(hranenie)}");
                            break;
                        case "7":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine($"Самое длинное слово: '{Longg(hranenie)}'");
                            break;
                        case "8":
                            if (string.IsNullOrEmpty(hranenie))
                            {
                                Console.WriteLine("Сначала введите текст (пункт 1 в меню)!");
                                break;
                            }
                            Console.WriteLine("Статистика по буквам:");
                            foreach (var pair in Povtor(hranenie))
                            {
                                Console.WriteLine($"  '{pair.Key}': {pair.Value} раз");
                            }
                            break;
                        case "9":
                            analizNew();
                            break;
                        case "10":
                            showOld();
                            break;
                        case "0":
                            a = false;
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            break;
                    }
                }
            }
        }
    }
}

