using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PR14.Pages
{
    /// <summary>
    /// Логика взаимодействия для страницы регистрации
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Метод регистрации (для тестов и кнопки)
        /// </summary>
        public bool Register(string login, string password, string username, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(username))
                return false;

            if (dateOfBirth > DateTime.Now)
                return false;

            if (Core.Context.User.Any(u => u.Login == login))
                return false;

            var newUser = new User
            {
                Username = username,
                Login = login,
                Password = password,
                DateOfBirth = dateOfBirth
            };

            Core.Context.User.Add(newUser);
            Core.Context.SaveChanges();
            Core.user = newUser;

            return true;
        }

        private void Reg_But_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateBr = DateBr.SelectedDate ?? DateTime.Now;

            bool success = Register(LoginBT.Text, PasswordBT.Text, Name.Text, dateBr);

            if (success)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех");
                NavigationService.Navigate(new Mainn());
            }
            else
            {
                MessageBox.Show("Ошибка регистрации! Проверьте данные.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Mainn());
        }
    }
}