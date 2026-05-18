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
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
            Loaded += CatalogPage_Loaded;
        }

        private void CatalogPage_Loaded(object sender, RoutedEventArgs e)
        {
            searchTB.Text = "";

            var genres = Core.Context.Genres.OrderBy(g => g.name).ToList();
            var allItem = new Genres { ID_Genres = 0, name = "Все жанры", description = "" };
            var list = new List<Genres> { allItem };
            list.AddRange(genres);
            genreCB.ItemsSource = list;
            genreCB.SelectedIndex = 0;

            sortCB.SelectedIndex = 0; 

            LoadBooks();
        }

        private void LoadBooks()
        {
            if (genreCB == null || sortCB == null || booksList == null)
                return;

            var query = Core.Context.Book.Where(b => !b.isFrozen);

            if (!string.IsNullOrWhiteSpace(searchTB?.Text) && searchTB.Text != "Поиск...")
            {
                string s = searchTB.Text.ToLower();
                query = query.Where(b =>b.name.ToLower().Contains(s) || b.Users.name.ToLower().Contains(s));
            }

            if (genreCB.SelectedItem is Genres genre && genre.ID_Genres > 0)
            {
                var bookIds = Core.Context.BookGenre.Where(bg => bg.ID_Genres == genre.ID_Genres).Select(bg => bg.ID_Book).ToList();
                query = query.Where(b => bookIds.Contains(b.ID_Book));
            }

            if (sortCB.SelectedIndex == 1)
            {
                query = query.OrderByDescending(b => b.Reviews.Any() ? b.Reviews.Average(r => r.rating) : 0);
            }
            else
            {
                query = query.OrderBy(b => b.name);
            }
            booksList.ItemsSource = query.ToList();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e) => LoadBooks();
        private void GenreCB_SelectionChanged(object sender, SelectionChangedEventArgs e) => LoadBooks();
        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e) => LoadBooks();

        private void BooksList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (booksList.SelectedItem is Book book)
            {
                NavigationService.Navigate(new BookPage(book));
            }
        }
        private void ResetFiltersBtn_Click(object sender, RoutedEventArgs e)
        {
            searchTB.Text = "";
            genreCB.SelectedIndex = 0;
            sortCB.SelectedIndex = 0;
            LoadBooks();
        }
        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}