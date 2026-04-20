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
using System.Xml.Linq;

namespace PR15.Pages
{
    /// <summary>
    /// Логика взаимодействия для SaveAssembly.xaml
    /// </summary>
    public partial class SaveAssembly : Page
    { 
        public SaveAssembly()
        {
            InitializeComponent();
            SaveLstBox.ItemsSource = Core.Context.assembly.ToList();
        }

        private void PartBtn_Click_1(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            var asse = btn.DataContext as assembly;
            var f = new BuildDetailsWindow(asse);
            f.ShowDialog();
            //NavigationService.Navigate(new BuildDetailsWindow(asse));

        }

    }
}
