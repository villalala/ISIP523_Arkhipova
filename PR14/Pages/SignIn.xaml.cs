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

namespace PR14.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignIn_But_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(LoginBT.Text) || string.IsNullOrEmpty(PasswordBT.Text))
            {
                MessageBox.Show("Введите логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var user = Core.Context.User
               .FirstOrDefault(u => u.Login == LoginBT.Text && u.Password == PasswordBT.Text);

            if (user != null)
            {
                MessageBox.Show($"Добро пожаловать, {user.Username ?? user.Login}!", "Успех");
                NavigationService.Navigate(new Mainn());

            }
            Core.user = user;

        }

        private void Exit_But_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Mainn());
        }
    }
}
