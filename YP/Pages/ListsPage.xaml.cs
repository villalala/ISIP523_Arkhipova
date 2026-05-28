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
    public partial class ListsPage : Page
    {
        public ListsPage()
        {
            InitializeComponent();
            Loaded += ListsPage_Loaded;
        }

        private void ListsPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadBooks("Читаю"); // по умолчанию этот список 
        }

        private void StatusFilter_Click(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb) 
            { 
                LoadBooks(rb.Content.ToString()); // смотрит какой радио бтн нажали и выводит нужную страницу
            }
        }

        private void LoadBooks(string statusName) // загружает книги пользоваетля 
        {
            if (Core.currentUser == null)
            {
                booksList.ItemsSource = null; // очищает книги чтобы чужие не показать 
                return;
            }

            // фильтрует только нужные книги 
            var query = Core.Context.ReadingLists.Where(rl => rl.ID_Users == Core.currentUser.ID_Users && rl.StatusBook.name == statusName).Select(rl => rl.Book).ToList(); 

            booksList.ItemsSource = query; // показывает эти книги 
        }

        private void BooksList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (booksList.SelectedItem is Book book)
            {
                NavigationService.Navigate(new BookPage(book));
            }
        }
    }
}
