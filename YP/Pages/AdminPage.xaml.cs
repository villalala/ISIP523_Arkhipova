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
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            Loaded += AdminPage_Loaded;
        }

        private void AdminPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoadComplaints();
        }

        private void LoadComplaints()
        {
            complaintsGrid.ItemsSource = Core.Context.Complaints.ToList();
        }

        private void ComplaintsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (complaintsGrid.SelectedItem is Complaints complaint)
            {
                btnToggleUser.IsEnabled = complaint.Users != null;
                btnToggleBook.IsEnabled = complaint.Book != null;

                btnToggleUser.Content = complaint.Users.isFrozen == true
                    ? "Разморозить пользователя"
                    : "Заморозить пользователя";

                btnToggleBook.Content = complaint.Book.isFrozen == true
                    ? "Разморозить книгу"
                    : "Заморозить книгу";
            }
            else
            {
                btnToggleUser.IsEnabled = false;
                btnToggleBook.IsEnabled = false;
            }
        }

        private void ToggleUser_Click(object sender, RoutedEventArgs e)
        {
            if (complaintsGrid.SelectedItem is Complaints complaint && complaint.Users != null)
            {
                bool newState = !complaint.Users.isFrozen;
                complaint.Users.isFrozen = newState;
                Core.Context.SaveChanges();

                string action = newState ? "Заморожен" : "Разморожен";
                MessageBox.Show($"Пользователь {complaint.Users.name} {action.ToLower()}.",
                                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadComplaints();
            }
        }

        private void ToggleBook_Click(object sender, RoutedEventArgs e)
        {
            if (complaintsGrid.SelectedItem is Complaints complaint && complaint.Book != null)
            {
                bool newState = !complaint.Book.isFrozen;
                complaint.Book.isFrozen = newState;
                Core.Context.SaveChanges();

                string action = newState ? "Заморожена" : "Разморожена";
                MessageBox.Show($"Книга \"{complaint.Book.name}\" {action.ToLower()}.",
                                "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadComplaints();
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e) => LoadComplaints();
    }
}
