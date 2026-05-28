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
    public partial class RegisterPage : Page
    {
        public RegisterPage() => InitializeComponent(); 

        private void RegBtn_Click(object sender, RoutedEventArgs e) // зарегестрироваться 
        {
            if (string.IsNullOrWhiteSpace(RegLoginBox.Text) || string.IsNullOrWhiteSpace(RegPassBox.Password))
            {
                MessageBox.Show("Заполните логин и пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Core.Context.Users.Any(u => u.login == RegLoginBox.Text))
            {
                MessageBox.Show("Такой логин уже занят", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var readerRole = Core.Context.Roles.FirstOrDefault(r => r.name == "Читатель"); // получение роли читателя 
            if (readerRole == null) return;

            var newUser = new Users // создание нового пользователя 
            {
                login = RegLoginBox.Text,
                password = RegPassBox.Password,
                name = RegNameBox.Text,
                email = RegEmailBox.Text,
                ID_Roles = readerRole.ID_Roles, // роль читателя присваивается 
                isFrozen = false
            };

            Core.Context.Users.Add(newUser); // добавление нового пользователя в контекст бд
            Core.Context.SaveChanges();

            MessageBox.Show("Регистрация успешна!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            NavigationService.GoBack();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack(); 
    }
}
