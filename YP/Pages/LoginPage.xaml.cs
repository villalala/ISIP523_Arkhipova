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
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e) // кнопка войти 
        {
            var user = Core.Context.Users.FirstOrDefault(u => u.login == LoginBox.Text && u.password == PassBox.Password); // поиск пользователя 

            if (user == null)
            {
                MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.isFrozen)
            {
                MessageBox.Show("Ваш аккаунт заморожен. Обратитесь к администратору.", "Доступ запрещён", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Core.currentUser = user; // сохранение текущего пользователя 
            NavigationService.Navigate(new CatalogPage());

            var main = Window.GetWindow(this) as MainWindow; // ссылка на главное окно 
            main.MainFrame.Navigate(new MainPage());
        }

        private void GoToRegBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegisterPage());
        }
    }
}