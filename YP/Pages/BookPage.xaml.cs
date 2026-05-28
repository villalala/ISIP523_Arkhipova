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
    public partial class BookPage : Page
    {
        private Book _book; // поле для хранения выбранной книги 

        public BookPage(Book book) // конструктор 
        {
            InitializeComponent();
            _book = book; // сохранение выбранной книги в поле классе 
            Loaded += BookPage_Loaded;
        }

        private void BookPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _book;
            LoadReviews(); // грузит отзывы
            LoadCurrentStatus(); // проверка статуса книги в листе юзера
            if (Core.currentUser != null && Core.currentUser.ID_Roles == 3) // если админ - кнопка заморозки
            {
                FreezeBookBtn.Visibility = Visibility.Visible; // видимость

                if (_book.isFrozen) // если книга уже заморожена - блокировка 
                {
                    FreezeBookBtn.IsEnabled = false;
                    FreezeBookBtn.Content = "Уже заморожена";
                }
            }
        }

        private void LoadReviews() // грузит отзывы
        {
            reviewsList.ItemsSource = Core.Context.Reviews.Where(r => r.ID_Book == _book.ID_Book).ToList(); // точно ли та книга 
        }

        private void LoadCurrentStatus() // проверка добавления статуса 
        {
            if (Core.currentUser == null) return;

            var existing = Core.Context.ReadingLists.FirstOrDefault(rl => rl.ID_Users == Core.currentUser.ID_Users && rl.ID_Book == _book.ID_Book); // ищет запись в таблице юзера + книга 

            if (existing != null)
            {
                var status = Core.Context.StatusBook.FirstOrDefault(s => s.ID_StatB == existing.ID_StatB);
                if (status.name == "В планах") rbPlan.IsChecked = true;
                else if (status.name == "Читаю") rbRead.IsChecked = true;
                else if (status.name == "Прочитано") rbDone.IsChecked = true;
                else if (status.name == "Заброшено") rbDrop.IsChecked = true;
            }
        }

        private void SaveStatusBtn_Click(object sender, RoutedEventArgs e) // сохранение статуса 
        {
            if (Core.currentUser == null)
            {
                MessageBox.Show("Войдите в аккаунт, чтобы управлять списком", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedStatus = ""; // определение какой статус выбран 
            if (rbPlan.IsChecked == true) selectedStatus = "В планах";
            else if (rbRead.IsChecked == true) selectedStatus = "Читаю";
            else if (rbDone.IsChecked == true) selectedStatus = "Прочитано";
            else if (rbDrop.IsChecked == true) selectedStatus = "Заброшено";

            if (string.IsNullOrEmpty(selectedStatus)) return;

            UpdateReadingList(selectedStatus); // обновление бд  
        }

        private void UpdateReadingList(string statusName) // обновление бд 
        {
            var status = Core.Context.StatusBook.FirstOrDefault(s => s.name == statusName); // название статуса ищет 
            if (status == null) return;

            var existing = Core.Context.ReadingLists.FirstOrDefault(rl => rl.ID_Users == Core.currentUser.ID_Users && rl.ID_Book == _book.ID_Book); // проверка есть ли уже такая же запись 

            if (existing == null)
            {
                Core.Context.ReadingLists.Add(new ReadingLists // добавляет книгу в список чтения 
                {
                    ID_Users = Core.currentUser.ID_Users,
                    ID_Book = _book.ID_Book,
                    ID_StatB = status.ID_StatB
                });
                Core.Context.SaveChanges();
                MessageBox.Show($"Книга добавлена в список «{statusName}»", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                existing.ID_StatB = status.ID_StatB; // меняет ид статуса 
                Core.Context.SaveChanges();
                MessageBox.Show($"Статус изменён на «{statusName}»", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SubmitReviewBtn_Click(object sender, RoutedEventArgs e) // оставить отзыв 
        {
            if (Core.currentUser == null) { MessageBox.Show("Войдите в аккаунт"); 
                return; }
            var inList = Core.Context.ReadingLists.Any(rl => rl.ID_Users == Core.currentUser.ID_Users && rl.ID_Book == _book.ID_Book);
            if (!inList) { MessageBox.Show("Добавьте книгу в список, чтобы оставить отзыв"); 
                return; }
            var alreadyReviewed = Core.Context.Reviews.Any(r => r.ID_Users == Core.currentUser.ID_Users && r.ID_Book == _book.ID_Book);
            if (alreadyReviewed) { MessageBox.Show("Вы уже оставляли отзыв на эту книгу"); 
                return; }

            int rating = ratingCB.SelectedIndex + 1;
            string text = inputTextTB.Text;
            if (string.IsNullOrWhiteSpace(text)) { MessageBox.Show("Введите текст отзыва"); return; }

            Core.Context.Reviews.Add(new Reviews { ID_Users = Core.currentUser.ID_Users, ID_Book = _book.ID_Book, text = text, rating = rating }); // создание отзыва 
            Core.Context.SaveChanges();
            MessageBox.Show("Отзыв успешно добавлен");
            inputTextTB.Text = "";
            LoadReviews();
        }

        private void ComplaintBtn_Click(object sender, RoutedEventArgs e) // жалоба 
        {
            if (Core.currentUser == null) { MessageBox.Show("Войдите в аккаунт"); return; }
            string text = inputTextTB.Text;
            if (string.IsNullOrWhiteSpace(text)) { MessageBox.Show("Опишите причину жалобы"); return; }

            Core.Context.Complaints.Add(new Complaints { ID_Users = Core.currentUser.ID_Users, ID_Book = _book.ID_Book, ID_Reviews = null, text = text }); // добавление жалобы 
            Core.Context.SaveChanges();
            MessageBox.Show("Жалоба отправлена администратору");
            inputTextTB.Text = "";
        }

        private void ReadBtn_Click(object sender, RoutedEventArgs e) // чтение книги 
        {
            NavigationService.Navigate(new ReaderPage(_book));
        }
        private void FreezeBookBtn_Click(object sender, RoutedEventArgs e) // заморозить книгу 
        {
            if (_book == null) return;

            var confirm = MessageBox.Show(
                "Вы уверены, что хотите заморозить эту книгу?\nОна станет недоступна для читателей.",
                "Подтверждение действия",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                _book.isFrozen = true; 
                Core.Context.SaveChanges();

                FreezeBookBtn.IsEnabled = false; // обновление интерфейса, блокировка кнопки
                FreezeBookBtn.Content = "Заморожена";

                MessageBox.Show("Книга успешно заморожена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
