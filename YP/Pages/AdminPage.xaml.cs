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

        private void AdminPage_Loaded(object sender, RoutedEventArgs e) => LoadAllData();

        private void LoadAllData()
        {
            LoadComplaints();
            LoadUnfreezeRequests();
            LoadAuthorRequests();
            LoadFrozenItems();
            LoadUsers();
            LoadRolesForComboBox();
        }

        private void LoadComplaints() => complaintsList.ItemsSource = Core.Context.Complaints.ToList();

        private void ComplaintAccept_Click(object sender, RoutedEventArgs e)
        {
            if (complaintsList.SelectedItem is Complaints complaint)
            {
                Core.Context.Complaints.Remove(complaint);
                Core.Context.SaveChanges();
                MessageBox.Show("Жалоба принята и удалена из списка.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadComplaints();
            }
        }

        private void ComplaintReject_Click(object sender, RoutedEventArgs e)
        {
            if (complaintsList.SelectedItem is Complaints complaint)
            {
                Core.Context.Complaints.Remove(complaint);
                Core.Context.SaveChanges();
                MessageBox.Show("Жалоба отклонена и удалена из списка.", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadComplaints();
            }
        }

        private void LoadUnfreezeRequests()
        {
            unfreezeList.ItemsSource = Core.Context.Unfreeze.Where(u => u.ID_status == 1).ToList();
        }

        private void UnfreezeAccept_Click(object sender, RoutedEventArgs e)
        {
            if (unfreezeList.SelectedItem is Unfreeze request)
            {
                request.ID_status = 2;
                if (request.Users != null)
                {
                    request.Users.isFrozen = false;
                    MessageBox.Show("Заявка принята. Аккаунт разморожен.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                if (request.Book != null)
                {
                    request.Book.isFrozen = false;
                    MessageBox.Show("Заявка принята. Книга разморожена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Core.Context.SaveChanges();
                LoadAllData();
            }
        }

        private void UnfreezeReject_Click(object sender, RoutedEventArgs e)
        {
            if (unfreezeList.SelectedItem is Unfreeze request)
            {
                request.ID_status = 3;
                Core.Context.SaveChanges();
                MessageBox.Show("Заявка отклонена.", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadUnfreezeRequests();
            }
        }

        private void LoadAuthorRequests()
        {
            authorReqList.ItemsSource = Core.Context.AuthorRoleRequests.Where(r => r.ID_RequestStatuses == 1).ToList();
        }

        private void AuthorReqAccept_Click(object sender, RoutedEventArgs e)
        {
            if (authorReqList.SelectedItem is AuthorRoleRequests request)
            {
                request.ID_RequestStatuses = 2;
                if (request.Users != null) request.Users.ID_Roles = 2;
                Core.Context.SaveChanges();
                MessageBox.Show("Заявка одобрена! Пользователь получил роль Автора.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAllData();
            }
        }

        private void AuthorReqReject_Click(object sender, RoutedEventArgs e)
        {
            if (authorReqList.SelectedItem is AuthorRoleRequests request)
            {
                request.ID_RequestStatuses = 3;
                Core.Context.SaveChanges();
                MessageBox.Show("Заявка отклонена.", "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadAuthorRequests();
            }
        }

        private void LoadFrozenItems()
        {
            frozenUsersList.ItemsSource = Core.Context.Users.Where(u => u.isFrozen == true).ToList();
            frozenBooksList.ItemsSource = Core.Context.Book.Where(b => b.isFrozen == true).ToList();
        }
        private void RefreshFrozen_Click(object sender, RoutedEventArgs e) => LoadFrozenItems();

        private void LoadUsers() => usersList.ItemsSource = Core.Context.Users.ToList();

        private void LoadRolesForComboBox()
        {
            roleCB.ItemsSource = Core.Context.Roles.ToList();
            roleCB.DisplayMemberPath = "name";
            roleCB.SelectedValuePath = "ID_Roles";
        }

        private void UsersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (usersList.SelectedItem is Users user)
            {
                roleCB.SelectedValue = user.ID_Roles;
            }
        }

        private void SaveRole_Click(object sender, RoutedEventArgs e)
        {
            if (usersList.SelectedItem is Users user && roleCB.SelectedValue is int roleId)
            {
                user.ID_Roles = roleId;
                Core.Context.SaveChanges();
                MessageBox.Show("Роль успешно изменена.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadUsers();
            }
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (usersList.SelectedItem is Users user && !string.IsNullOrWhiteSpace(newPassTB.Text))
            {
                user.password = newPassTB.Text;
                Core.Context.SaveChanges();
                MessageBox.Show("Пароль изменён.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                newPassTB.Text = "";
            }
        }
    }
}