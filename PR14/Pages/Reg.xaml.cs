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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void Reg_But_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateBr = DateBr.SelectedDate.Value;
            if (dateBr > DateTime.Now) {
                MessageBox.Show("Дата рождения не может быть больше текущей!");
             }
            var newUser = new User
            {
                Username = Name.Text,
                DateOfBirth = dateBr,
                Password = PasswordBT.Text,
                Login = LoginBT.Text
            };
            Core.Context.User.Add(newUser);
            Core.Context.SaveChanges();
            Core.user = newUser;

            NavigationService.Navigate(new Mainn());

        }
        private void Exit_But_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Mainn());
        }
    }
}
