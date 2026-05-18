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
    public partial class AuthorPage : Page
    {
        private Book _selectedBook;

        public AuthorPage()
        {
            InitializeComponent();
            Loaded += AuthorPage_Loaded;
        }

        private void AuthorPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Core.currentUser == null || Core.currentUser.ID_Roles != 2)
            {
                MessageBox.Show("Доступ только для авторов", "Ошибка доступа", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService.GoBack();
                return;
            }

            LoadMyBooks();
        }

        private void LoadMyBooks()
        {
            var authorBooks = Core.Context.Book.Where(b => b.ID_Author == Core.currentUser.ID_Users).ToList();

            myBooksList.ItemsSource = authorBooks;
        }

        private void MyBooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (myBooksList.SelectedItem is Book book)
            {
                _selectedBook = book;
                titleTB.Text = book.name;
                coverTB.Text = book.cover;
                descTB.Text = book.description;
                textTB.Text = book.text;
            }
        }

        private void SaveBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(titleTB.Text))
            {
                MessageBox.Show("Введите название книги", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_selectedBook == null)
            {
                var newBook = new Book
                {
                    name = titleTB.Text,
                    cover = coverTB.Text,
                    description = descTB.Text,
                    text = textTB.Text,
                    ID_Author = Core.currentUser.ID_Users,
                    isFrozen = false
                };
                Core.Context.Book.Add(newBook);
                MessageBox.Show("Книга добавлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                _selectedBook.name = titleTB.Text;
                _selectedBook.cover = coverTB.Text;
                _selectedBook.description = descTB.Text;
                _selectedBook.text = textTB.Text;
                MessageBox.Show("Книга обновлена", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            Core.Context.SaveChanges();
            ClearForm();
            LoadMyBooks();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedBook = null;
            titleTB.Text = "Название книги";
            coverTB.Text = "Ссылка на обложку";
            descTB.Text = "Аннотация";
            textTB.Text = "Текст книги...";
            myBooksList.SelectedItem = null;
        }
    }
}
