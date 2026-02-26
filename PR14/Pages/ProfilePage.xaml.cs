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
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();

            DataContext = Core.user;
            LoadTickets();
        }

        private void LoadTickets()
        {
            var tickets = Core.Context.Ticket
                .Where(t => t.UserID == Core.user.UserID)
                .Select(t => new
                {
                    Film = t.Session.Films.FilmName,
                    Date = t.Session.SessionDate,
                    Room = t.Session.Room.RoomName,
                    Seat = t.Sit.Number,
                    Price = t.Session.Room.RoomCategory.Price
                })
                .ToList();

            TicketsList.ItemsSource = tickets;
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Mainn());
        }
    }
}
