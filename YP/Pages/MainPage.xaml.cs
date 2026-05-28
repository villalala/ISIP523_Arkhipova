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
            UpdateAuthState(); // обновление кнопок если надо 
            ContentFrame.Navigate(new CatalogPage());
        }

        private void UpdateAuthState() // показ интерфейса в зависимости от роли
        {
            if (Core.currentUser == null) // это если не зашел 
            {
                AuthBtn.Visibility = Visibility.Visible;
                adminBtn.Visibility = Visibility.Collapsed;
                authorBtn.Visibility = Visibility.Collapsed;
                return;
            }

            AuthBtn.Visibility = Visibility.Collapsed; // скрытие кнопки войти 
            adminBtn.Visibility = Core.currentUser.ID_Roles == 3 ? Visibility.Visible : Visibility.Collapsed; // админ панель если зашел админ 
            authorBtn.Visibility = Core.currentUser.ID_Roles == 2 ? Visibility.Visible : Visibility.Collapsed; // панель для авторов
        }
        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(new LoginPage());
        }

        private void CatalogBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new CatalogPage()); // перехд в каталог 
        private void ListsBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new ListsPage()); // переход в списик 
        private void AdminBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new AdminPage()); // в админку 
        private void AuthorBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new AuthorPage()); //  вавторское 
        private void ProfileBtn_Click(object sender, RoutedEventArgs e) => ContentFrame.Navigate(new ProfilePage()); // профиль 

        private void LogoutBtn_Click(object sender, RoutedEventArgs e) // выход из аккаунта 
        {
            Core.currentUser = null;
            UpdateAuthState(); // обновление кнопок 
            ContentFrame.Navigate(new CatalogPage());
        }
    }
}
