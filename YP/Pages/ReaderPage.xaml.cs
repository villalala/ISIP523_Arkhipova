using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YP.Pages
{
    public partial class ReaderPage : Page
    {
        private Book currentBook;

        public ReaderPage(Book book)
        {
            InitializeComponent();
            currentBook = book;
            Loaded += ReaderPage_Loaded;
        }

        private void ReaderPage_Loaded(object sender, RoutedEventArgs e)
        {
            BookTitle.Text = currentBook.name; // название
            LoadBookText();
        }

        private void LoadBookText()  // загрузка книги и текста 
        {
            TextContent.Document.Blocks.Clear(); // осищение предыдущего текста 

            // весь текст на абзацы, разделение как двойной ентер 
            string[] paragraphs = currentBook.text.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string paragraph in paragraphs) // каждый абзац по отдельности 
            {
                bool isChapterTitle = paragraph.Trim().StartsWith("Глава") || (paragraph.Trim().Length < 50 && !paragraph.Contains(" ")); // является ли абзац началом главы или нет 
                 
                Paragraph p = new Paragraph(new Run(paragraph.Trim())) // новый блок текста, дальше оформление 
                {
                    FontSize = isChapterTitle ? 20 : 16,
                    FontWeight = isChapterTitle ? FontWeights.Bold : FontWeights.Normal,
                    Margin = isChapterTitle ? new Thickness(0, 30, 0, 20) : new Thickness(0, 0, 0, 15),
                    TextAlignment = isChapterTitle ? TextAlignment.Justify : TextAlignment.Justify,
                    LineHeight = 28
                };

                TextContent.Document.Blocks.Add(p); // добавление обработанного абзаца в документ 
            }

            TextScroll.ScrollToHome(); // прокручивание в начало текста 
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
