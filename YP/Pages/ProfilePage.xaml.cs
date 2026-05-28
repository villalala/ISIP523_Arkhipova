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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            Loaded += ProfilePage_Loaded;
        }

        private void ProfilePage_Loaded(object sender, RoutedEventArgs e) // отображение профиля 
        {
            if (Core.currentUser == null)
            {
                MessageBox.Show("Войдите в систему", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.GoBack();
                return;
            }

            DataContext = Core.currentUser; // привязка пользователя к текущему контексту 

            if (Core.currentUser.isFrozen) // если заморожен 
            {
                freezeWarning.Visibility = Visibility.Visible;
            }

            if (Core.currentUser.ID_Roles == 2 || Core.currentUser.ID_Roles == 3) btnApplyAuthor.Visibility = Visibility.Collapsed; // скрыть заявку на автора, если он уже автор или админ

            // Загружаем отзывы
            LoadReviews();
        }

        private void LoadReviews() // отзывы пользователя 
        {
            var userReviews = Core.Context.Reviews.Where(r => r.ID_Users == Core.currentUser.ID_Users).ToList(); // отзывы пользователя 
            reviewsList.ItemsSource = userReviews; // привязка отзывов к листу 
        } 

        private void ApplyAuthorBtn_Click(object sender, RoutedEventArgs e) // стать автором 
        {
            if (Core.currentUser == null) return;

            var existing = Core.Context.AuthorRoleRequests.FirstOrDefault(r => r.ID_Users == Core.currentUser.ID_Users && r.ID_RequestStatuses == 1); // проверка нет ли уже заявки 

            if (existing != null)
            {
                MessageBox.Show("У вас уже есть заявка на рассмотрении.", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information); 
                return;
            }

            var newRequest = new AuthorRoleRequests // создание заявки 
            {
                ID_Users = Core.currentUser.ID_Users, // привязка заявки на юзера 
                ID_RequestStatuses = 1 // 1 = "На рассмотрении"
            };

            Core.Context.AuthorRoleRequests.Add(newRequest); // добавление заявки в таблицу 
            Core.Context.SaveChanges();

            MessageBox.Show("Заявка отправлена! Ожидайте решения администратора.", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            btnApplyAuthor.IsEnabled = false; // блокировка книги 
            btnApplyAuthor.Content = "Заявка отправлена";
        }

        private void AppealFreezeBtn_Click(object sender, RoutedEventArgs e) // ну типо аппеляции 
        {
            MessageBox.Show("Апелляция отправлена администратору.", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}