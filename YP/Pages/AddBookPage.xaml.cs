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
    public partial class AddBookPage : Page
    {
        private Book currentBook; // хранения книги, которую  редактируем
        
        // конструктор страницы
        public AddBookPage(Book book)
        {
            InitializeComponent();
            currentBook = book; // сохраняет переданный объект в поле класса
            Loaded += AddBookPage_Loaded; // ждет, пока страница полностью прогрузится и только тогда запускает
        }

        private void AddBookPage_Loaded(object sender, RoutedEventArgs e)
        {
            GenreCB.ItemsSource = Core.Context.Genres.ToList();  // загружает справочник жанров из бд

            if (currentBook != null)
            {
                PageTitle.Text = "Редактирование книги";
                NameTB.Text = currentBook.name;
                CoverTB.Text = currentBook.cover;
                DescTB.Text = currentBook.description;
                TextTB.Text = currentBook.text;

                var linkedGenre = Core.Context.BookGenre.FirstOrDefault(bg => bg.ID_Book == currentBook.ID_Book); // ищет запись, где ID_Book совпадает с выбраннйо книгой
                if (linkedGenre != null)
                {
                    GenreCB.SelectedItem = Core.Context.Genres.FirstOrDefault(g => g.ID_Genres == linkedGenre.ID_Genres); // если связь найдена, устанавливает жанр в ComboBox как выбранный
                }
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTB.Text) || GenreCB.SelectedItem == null)
            {
                MessageBox.Show("Заполните название и выберите жанр.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var selectedGenre = (Genres)GenreCB.SelectedItem; // получает объект выбранного жанра

            if (currentBook == null) // создаёт экземпляр сущности и заполняет свойства данными из полей
            {
                Book newBook = new Book
                {
                    name = NameTB.Text,
                    cover = CoverTB.Text,
                    description = DescTB.Text,
                    text = TextTB.Text,
                    ID_Author = Core.currentUser.ID_Users,
                    isFrozen = false
                };

                Core.Context.Book.Add(newBook);
                Core.Context.SaveChanges(); // получает ID

                BookGenre newGenreLink = new BookGenre // создает связь с жанров, после получения Id книги
                {
                    ID_Book = newBook.ID_Book,
                    ID_Genres = selectedGenre.ID_Genres
                };
                Core.Context.BookGenre.Add(newGenreLink);
            }
            else
            {
                // отслеживание изменений, типо редактирование 
                currentBook.name = NameTB.Text;
                currentBook.cover = CoverTB.Text;
                currentBook.description = DescTB.Text;
                currentBook.text = TextTB.Text;

                // поиск старой связи
                var oldLink = Core.Context.BookGenre.FirstOrDefault(link => link.ID_Book == currentBook.ID_Book);
                if (oldLink != null) Core.Context.BookGenre.Remove(oldLink); // старая на удаление 

                // создание новой записи
                BookGenre updatedGenreLink = new BookGenre 
                {
                    ID_Book = currentBook.ID_Book,
                    ID_Genres = selectedGenre.ID_Genres
                };
                Core.Context.BookGenre.Add(updatedGenreLink);
            }

            Core.Context.SaveChanges();
            MessageBox.Show("Книга сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
