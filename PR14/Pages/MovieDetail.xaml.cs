using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для MovieDetail.xaml
    /// </summary>

        public partial class MovieDetail : Page
        {
            private Films _currentFilm;

            public MovieDetail(Films selectedFilm)
            {
                InitializeComponent();
                _currentFilm = selectedFilm;

                DataContext = _currentFilm;

                LoadSessions();
            }

            private void LoadSessions()
            {
                var sessions = Core.Context.Session
                    .Where(s => s.FilmID == _currentFilm.FilmID)
                    .Select(s => new
                    {
                        s.SessionID,
                        DateTime = s.SessionDate,
                        Room = s.Room.RoomName,
                        Category = s.Room.RoomCategory.Name,
                        Price = s.Room.RoomCategory.Price
                    })
                    .OrderBy(s => s.DateTime)
                    .ToList();

                lbSessions.ItemsSource = sessions;
            }
            private void lbSessions_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (lbSessions.SelectedItem == null) return;

                dynamic selected = lbSessions.SelectedItem;

                if (Core.user == null)
                {
                    MessageBox.Show("Для покупки билета необходимо войти в аккаунт.", "Требуется авторизация",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                        NavigationService.Navigate(new Mainn());
                    return;
                }

            NavigationService?.Navigate(new SeatSelectionPage(_currentFilm, selected.SessionID, selected.Price));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
            {
                NavigationService?.GoBack();
            }
        }
    }

