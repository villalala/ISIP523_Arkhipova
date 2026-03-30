using PR14.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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

namespace PR14
{
    /// <summary>
    /// Логика взаимодействия для Mainn.xaml
    /// </summary>
    public partial class Mainn : Page
    {
        private int _movieId;
        private object user;

        public Mainn()
        {
            InitializeComponent();
            DataContext = this;
            FilmsLB.Items.Clear();
            FilmsLB.ItemsSource = Core.Context.Films.ToList();

            
        }


        private void LoadFilms()
        {
            FilmsLB.ItemsSource = Core.Context.Films.ToList();
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string search = SearchBox.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(search))
            {
                LoadFilms();
                return;
            }

            var searchedFilm = Core.Context.Films.Where(f => f.FilmName.ToLower().Contains(search)).ToList();

            FilmsLB.ItemsSource = searchedFilm;
        }

        private void Sort_Checked(object sender, RoutedEventArgs e)
        {
            var films = Core.Context.Films.ToList();

            if (SortByName.IsChecked == true)
            {
                films = films.OrderBy(f => f.FilmName).ToList();
            }
            else if (SortByRating.IsChecked == true)
            {
                films = films.OrderByDescending(f => f.Rating).ToList();
            }

            FilmsLB.ItemsSource = films.ToList();
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignIn());
        }

        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Reg());
        }

        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilePage());

            if (Core.user == null)
            {
                MessageBox.Show("Войдите в профиль!!!!!! :)");
                NavigationService.Navigate(new SignIn());
                return;
            }
            DataContext = Core.user;

        }


        private void FilmsLB_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FilmsLB.SelectedItem is Films selectedFilm)
            {
                NavigationService.Navigate(new MovieDetail(selectedFilm));
            }
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            Core.user = null;
            NavigationService.Navigate(new Mainn());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Core.user != null)
            {
                Reg.Visibility = Visibility.Collapsed;
                SignIn.Visibility = Visibility.Collapsed;
                SignOut.Visibility = Visibility.Visible;
                Profile.Visibility = Visibility.Visible;
            }
        }

        private void FilmsLB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
