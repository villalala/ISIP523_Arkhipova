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
        private Book _book;

        public BookPage(Book book)
        {
            InitializeComponent();
            _book = book;
            Loaded += BookPage_Loaded;
        }

        private void BookPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _book;
            LoadReviews();
            LoadCurrentStatus();
        }

        private void LoadReviews()
        {
            reviewsList.ItemsSource = Core.Context.Reviews.Where(r => r.ID_Book == _book.ID_Book).ToList();
        }

        private void LoadCurrentStatus()
        {
            if (Core.currentUser == null) return;

            var existing = Core.Context.ReadingLists.FirstOrDefault(rl => rl.ID_Users == Core.currentUser.ID_Users && rl.ID_Book == _book.ID_Book);

            if (existing != null)
            {
                var status = Core.Context.StatusBook.FirstOrDefault(s => s.ID_StatB == existing.ID_StatB);
                if (status.name == "В планах") rbPlan.IsChecked = true;
                else if (status.name == "Читаю") rbRead.IsChecked = true;
                else if (status.name == "Прочитано") rbDone.IsChecked = true;
                else if (status.name == "Заброшено") rbDrop.IsChecked = true;
            }
        }

        private void SaveStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Core.currentUser == null)
            {
                MessageBox.Show("Войдите в аккаунт, чтобы управлять списком", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string selectedStatus = "";
            if (rbPlan.IsChecked == true) selectedStatus = "В планах";
            else if (rbRead.IsChecked == true) selectedStatus = "Читаю";
            else if (rbDone.IsChecked == true) selectedStatus = "Прочитано";
            else if (rbDrop.IsChecked == true) selectedStatus = "Заброшено";

            if (string.IsNullOrEmpty(selectedStatus)) return;

            UpdateReadingList(selectedStatus);
        }

        private void UpdateReadingList(string statusName)
        {
            var status = Core.Context.StatusBook.FirstOrDefault(s => s.name == statusName);
            if (status == null) return;

            var existing = Core.Context.ReadingLists.FirstOrDefault(rl => rl.ID_Users == Core.currentUser.ID_Users && rl.ID_Book == _book.ID_Book);

            if (existing == null)
            {
                Core.Context.ReadingLists.Add(new ReadingLists
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
                existing.ID_StatB = status.ID_StatB;
                Core.Context.SaveChanges();
                MessageBox.Show($"Статус изменён на «{statusName}»", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ReadBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(_book.text ?? "Текст книги отсутствует", "Чтение", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SubmitReviewBtn_Click(object sender, RoutedEventArgs e)
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

            Core.Context.Reviews.Add(new Reviews { ID_Users = Core.currentUser.ID_Users, ID_Book = _book.ID_Book, text = text, rating = rating });
            Core.Context.SaveChanges();
            MessageBox.Show("Отзыв успешно добавлен");
            inputTextTB.Text = "";
            LoadReviews();
        }

        private void ComplaintBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Core.currentUser == null) { MessageBox.Show("Войдите в аккаунт"); return; }
            string text = inputTextTB.Text;
            if (string.IsNullOrWhiteSpace(text)) { MessageBox.Show("Опишите причину жалобы"); return; }

            Core.Context.Complaints.Add(new Complaints { ID_Users = Core.currentUser.ID_Users, ID_Book = _book.ID_Book, ID_Reviews = null, text = text });
            Core.Context.SaveChanges();
            MessageBox.Show("Жалоба отправлена администратору");
            inputTextTB.Text = "";
        }
    }
}
