using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PR14.Pages
{
    public partial class TicketConfirmationPage : Page
    {
        private readonly Films _film;
        private readonly int _sessionId;
        private readonly List<int> _selectedSitSessionIds;
        private readonly decimal _pricePerSeat;

        public TicketConfirmationPage(
            Films film,
            int sessionId,
            List<int> selectedSitSessionIds,
            decimal pricePerSeat)
        {
            InitializeComponent();

            _film = film;
            _sessionId = sessionId;
            _selectedSitSessionIds = selectedSitSessionIds ?? new List<int>();
            _pricePerSeat = pricePerSeat;

            DataContext = this;

            tbSeats.Text = GetSelectedSeatsString();
        }

        public string FilmName => _film?.FilmName ?? "—";

        public string SessionInfo
        {
            get
            {
                var s = Core.Context.Session
                    .Where(x => x.SessionID == _sessionId)
                    .Select(x => new
                    {
                        Room = x.Room.RoomName,
                        Cat = x.Room.RoomCategory.Name,
                        Dt = x.SessionDate
                    })
                    .FirstOrDefault();

                return s != null
                    ? $"{s.Room} ({s.Cat}) • {s.Dt:dd.MM.yyyy}"
                    : "—";
            }
        }
        public string SelectedSeats => GetSelectedSeatsString();

        public decimal PricePerSeat => _pricePerSeat;

        public decimal TotalPrice => _selectedSitSessionIds.Count * _pricePerSeat;

        private string GetSelectedSeatsString()
        {
            if (!_selectedSitSessionIds.Any()) return "—";

            var numbers = Core.Context.SitSession
                .Where(ss => _selectedSitSessionIds.Contains(ss.SitSessionID))
                .Select(ss => ss.Sit.Number)
                .OrderBy(n => n)
                .ToList();

            return string.Join(", ", numbers);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService?.CanGoBack == true)
            {
                NavigationService.GoBack();
            }
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Core.user == null)
            {
                MessageBox.Show("Необходимо войти в аккаунт.", "Ошибка авторизации",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                foreach (var sitSessionId in _selectedSitSessionIds)
                {
                    var sitSession = Core.Context.SitSession
                        .FirstOrDefault(ss => ss.SitSessionID == sitSessionId);

                    if (sitSession == null) continue;

                    if (sitSession.IsTake)
                    {
                        MessageBox.Show($"Место {sitSession.Sit.Number} уже занято.\nПокупка отменена.",
                            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    sitSession.IsTake = true;

                    Core.Context.Ticket.Add(new Ticket
                    {
                        UserID = Core.user.UserID,
                        SessionID = _sessionId,
                        SitID = sitSession.SitID
                    });
                }

                Core.Context.SaveChanges();

                MessageBox.Show(
                    $"Билеты успешно оформлены!\n\n" +
                    $"Количество: {_selectedSitSessionIds.Count}\n" +
                    $"Сумма: {TotalPrice:N2} ₽",
                    "Успешно",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                NavigationService?.Navigate(new Mainn());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении:\n" + ex.Message,
                    "Ошибка базы данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}