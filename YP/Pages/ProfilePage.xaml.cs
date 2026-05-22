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

        private void ProfilePage_Loaded(object sender, RoutedEventArgs e)
        {
            if (Core.currentUser == null)
            {
                MessageBox.Show("Войдите в систему", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.GoBack();
                return;
            }

            // Привязываем данные пользователя (как у тебя)
            DataContext = Core.currentUser;

            // Показываем предупреждение, если аккаунт заморожен
            if (Core.currentUser.isFrozen)
            {
                freezeWarning.Visibility = Visibility.Visible;
            }

            // Скрываем кнопку заявки, если уже автор/админ
            if (Core.currentUser.ID_Roles == 2 || Core.currentUser.ID_Roles == 3)
                btnApplyAuthor.Visibility = Visibility.Collapsed;

            // Загружаем отзывы
            LoadReviews();
        }

        private void LoadReviews()
        {
            var userReviews = Core.Context.Reviews
                .Where(r => r.ID_Users == Core.currentUser.ID_Users)
                .ToList();
            reviewsList.ItemsSource = userReviews;
        }

        private void ApplyAuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Core.currentUser == null) return;

            // Проверяем: не подана ли уже заявка со статусом "На рассмотрении" (ID=1)
            var existing = Core.Context.AuthorRoleRequests
                .FirstOrDefault(r => r.ID_Users == Core.currentUser.ID_Users
                                  && r.ID_RequestStatuses == 1);

            if (existing != null)
            {
                MessageBox.Show("У вас уже есть заявка на рассмотрении.", "Инфо",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // Создаём новую заявку
            var newRequest = new AuthorRoleRequests
            {
                ID_Users = Core.currentUser.ID_Users,
                ID_RequestStatuses = 1 // 1 = "На рассмотрении"
            };

            Core.Context.AuthorRoleRequests.Add(newRequest);
            Core.Context.SaveChanges();

            MessageBox.Show("Заявка отправлена! Ожидайте решения администратора.", "Успех",
                MessageBoxButton.OK, MessageBoxImage.Information);

            // Блокируем кнопку, чтобы не спамить
            btnApplyAuthor.IsEnabled = false;
            btnApplyAuthor.Content = "Заявка отправлена";
        }

        private void AppealFreezeBtn_Click(object sender, RoutedEventArgs e)
        {
            // Заглушка, если нет таблицы апелляций
            MessageBox.Show("Апелляция отправлена администратору.", "Инфо",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}