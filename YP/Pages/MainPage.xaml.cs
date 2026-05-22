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
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateAuthState();
            ContentFrame.Navigate(new CatalogPage());
        }

        private void UpdateAuthState()
        {
            if (Core.currentUser == null)
            {
                AuthBtn.Visibility = Visibility.Visible;
                adminBtn.Visibility = Visibility.Collapsed;
                authorBtn.Visibility = Visibility.Collapsed;
                return;
            }

            AuthBtn.Visibility = Visibility.Collapsed;
            adminBtn.Visibility = Core.currentUser.ID_Roles == 3 ? Visibility.Visible : Visibility.Collapsed;
            authorBtn.Visibility = Core.currentUser.ID_Roles == 2 ? Visibility.Visible : Visibility.Collapsed;
        }
        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LoginPage());
        }

        private void CatalogBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new CatalogPage());
        private void ListsBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new ListsPage());
        private void AdminBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new AdminPage());
        private void AuthorBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new AuthorPage());
        private void ProfileBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new ProfilePage());

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            Core.currentUser = null;
            UpdateAuthState();
            ContentFrame.Navigate(new CatalogPage());
        }
    }
}
