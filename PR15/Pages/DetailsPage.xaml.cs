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
    /// Логика взаимодействия для DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        parttype parttype;
        public DetailsPage(parttype Pt) // конструууктор
        {
            InitializeComponent();
            parttype = Pt; // тип данных сохраняется в переменной 
            List<basepart> details = Core.Context.basepart.Where(b=>b.parttypeid == parttype.id).ToList(); // это фильтрация 
            ItemLstBox.ItemsSource = details; // запоняется список деталей отфильтрованный 
        }

        private void GetBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button; // чтобы точно кнопка нажималась, а не что то другое. для лист бокса подойдет
            basepart ChoisePart = btn.DataContext as basepart; // данные передаются как бейспарт в зависимости от нажатой кнопки
            if (btn == null) return; // проверка на ноль
            Core.sborka.Add(ChoisePart); // в список в коре добавляется выбранный элемент 
            NavigationService.GoBack(); // и сразу на главную страницу 
        }
    }
}
