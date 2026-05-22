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
        public AuthorPage()
        {
            InitializeComponent();
            Loaded += AuthorPage_Loaded;
        }

        private void AuthorPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks();
        }

        private void LoadBooks()
        {
            if (Core.currentUser == null) return;

            int authorId = Core.currentUser.ID_Users;

            myBooksList.ItemsSource = Core.Context.Book.Where(b => b.ID_Author == authorId && b.isFrozen == false).ToList();

            frozenBooksList.ItemsSource = Core.Context.Book.Where(b => b.ID_Author == authorId && b.isFrozen == true).ToList();
        }

        private void AddBookBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBookPage(null));
        }

        private void EditBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Book book)
            {
                NavigationService.Navigate(new AddBookPage(book));
            }
        }

        private void AppealBookBtn_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).DataContext is Book frozenBook)
            {
                var existing = Core.Context.Unfreeze.FirstOrDefault(u => u.ID_Book == frozenBook.ID_Book && u.ID_status == 1);

                if (existing != null)
                {
                    MessageBox.Show("Заявка на разморозку этой книги уже на рассмотрении.", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var appeal = new Unfreeze
                {
                    ID_Users = Core.currentUser.ID_Users,
                    ID_Book = frozenBook.ID_Book,
                    text = "Прошу снять заморозку с книги. Нарушений не обнаружено.",
                    ID_status = 1
                };

                Core.Context.Unfreeze.Add(appeal);
                Core.Context.SaveChanges();

                MessageBox.Show("Заявка отправлена администратору.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
