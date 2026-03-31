using PR15.Models;
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

namespace PR15.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            ItemLstBox.ItemsSource = Core.Context.parttype.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ChoiceLstBox.ItemsSource = Lists.Choise;
           
        }

        private void ItemBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            parttype ChoiseType = btn.DataContext as parttype;
            NavigationService.Navigate(new Pages.DetailsPage(ChoiseType));

        }
    }
}
