using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PR14.Pages
{
    /// <summary>
    /// Логика взаимодействия для страницы авторизации
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод авторизации (для тестов и кнопки)
        /// </summary>
        public bool Auth(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                return false;

            var user = Core.Context.User
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                Core.user = user;
                return true;
            }

            return false;
        }

        private void SignIn_But_Click(object sender, RoutedEventArgs e)
        {
            bool success = Auth(LoginBT.Text, PasswordBT.Text);

            if (success)
            {
                MessageBox.Show($"Добро пожаловать, {Core.user.Username ?? Core.user.Login}!", "Успех");
                NavigationService.Navigate(new Mainn());
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Mainn());
        }
    }
}