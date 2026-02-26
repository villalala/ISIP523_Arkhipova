using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR14.Pages
{
    public partial class SeatSelectionPage : Page
    {
        private Films _film;
        private int _sessionId;
        private decimal _pricePerSeat;
        private List<SitSession> _sitSessions = new List<SitSession>();
        private List<int> _selectedSitIds = new List<int>();

        public SeatSelectionPage(Films film, int sessionId, decimal price)
        {
            InitializeComponent();
            _film = film;
            _sessionId = sessionId;
            _pricePerSeat = price;
            DataContext = this;
            LoadSeats();
        }

        public string FilmName => _film?.FilmName ?? "Без названия";

        public string SessionInfo
        {
            get
            {
                var s = Core.Context.Session.FirstOrDefault(x => x.SessionID == _sessionId);
                if (s == null) return "Сеанс не найден";
                return $"{s.Room.RoomName} ({s.Room.RoomCategory.Name}) • {s.SessionDate:dd.MM.yyyy}";
            }
        }

        private void LoadSeats()
        {
            _sitSessions = Core.Context.SitSession
                .Where(ss => ss.SessionID == _sessionId)
                .OrderBy(ss => ss.Sit.Number)
                .ToList();

            SeatsGrid.ItemsSource = _sitSessions.Select(ss => new
            {
                SitSessionID = ss.SitSessionID,
                Number = ss.Sit.Number,
                IsTaken = ss.IsTake
            }).ToList();
        }

        private void SeatButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                tbSelectedSeats.Text = "Клик по кнопке!";

                var item = button.DataContext;
                if (item == null)
                {
                    tbSelectedSeats.Text += "\nDataContext null";
                    return;
                }

                int sitSessionId = 0;
                try
                {
                    sitSessionId = (int)item.GetType().GetProperty("SitSessionID").GetValue(item);
                    tbSelectedSeats.Text += $"\nID места: {sitSessionId}";
                }
                catch
                {
                    tbSelectedSeats.Text += "\nНе удалось достать ID";
                    return;
                }

                var sitSession = _sitSessions.FirstOrDefault(x => x.SitSessionID == sitSessionId);
                if (sitSession == null || sitSession.IsTake)
                {
                    tbSelectedSeats.Text += "\nМесто занято или не найдено";
                    return;
                }

                tbSelectedSeats.Text += "\nМесто свободно";

                if (_selectedSitIds.Contains(sitSessionId))
                {
                    _selectedSitIds.Remove(sitSessionId);
                    tbSelectedSeats.Text += "\nМесто убрано";
                }
                else
                {
                    _selectedSitIds.Add(sitSessionId);
                    tbSelectedSeats.Text += "\nМесто добавлено";
                }

                tbSelectedSeats.Text += $"\nТеперь выбрано: {_selectedSitIds.Count} мест";

                UpdateSelectedInfo();
            }
        }

        private void UpdateSelectedInfo()
        {
            int count = _selectedSitIds.Count;

            if (count == 0)
            {
                tbSelectedSeats.Text = "Места не выбраны";
                btnBuy.IsEnabled = false;
            }
            else
            {
                decimal total = count * _pricePerSeat;
                tbSelectedSeats.Text = $"Выбрано {count} × {_pricePerSeat:N2} ₽ = {total:N2} ₽";
                btnBuy.IsEnabled = true;
            }
        }
        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedSitIds.Count == 0) return;

            NavigationService?.Navigate(new TicketConfirmationPage(
                _film,
                _sessionId,
                _selectedSitIds,
                _pricePerSeat
            ));
        }
        private void Exit_But_Click(Object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Mainn());
        }
    }
}