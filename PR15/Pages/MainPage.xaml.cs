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
        public MainPage() // конструктор 
        {
            InitializeComponent();
            ItemLstBox.ItemsSource = Core.Context.parttype.ToList(); // заполняет список всеми данными
        }

        private void Page_Loaded(object sender, RoutedEventArgs e) 
        {
            ChoiceLstBox.ItemsSource = Core.sborka; // заполняет список выбранным данными из списка в коре типо
           
        }

        private void ItemBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button; // чтобы точно кнопка нажималась, а не что то другое. для лист бокса подойдет
            if (btn == null) return; // проверка на ноль
            parttype ChoiseType = btn.DataContext as parttype; // данные передаются как парттайп в зависимости от нажатой кнопки
            NavigationService.Navigate(new Pages.DetailsPage(ChoiseType)); //переход на страницу 

        }

        private void DelBtn_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button; // чтобы точно кнопка нажималась, а не что то другое. для лист бокса подойдет
            if (btn == null) { // проверка на ноль
                MessageBox.Show("оаошывд");
                return; }
            basepart ChoisePart = btn.DataContext as basepart; // данные передаются как бейспарт в зависимости от нажатой кнопки
            Core.sborka.Remove(ChoisePart); // удаление из списка сборки в коде
            ChoiceLstBox.ItemsSource = null; // ну типо тоже удаление 
            ChoiceLstBox.ItemsSource = Core.sborka; // ну окончательное удаление 
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            assembly NewAssembly = new assembly // конструктор для ассембли 
            {
                name = NameTextBox.Text, // типо получение имени их тхт бокса 
                author = AuthorTextBox.Text, // типо получение автора их тхт бокса 
            };
            Core.Context.assembly.Add(NewAssembly); // сохранение в таблицу ассембли
            foreach (basepart Choice in Lists.Choise) // цикл для перебора в бейспарте для запоминания выбора польователя 
            {
                partassembly NewPart = new partassembly // конструктор 
                {
                    partid = Choice.id, // получение айди из переменной выбора пользователя типо
                    assemblyid = NewAssembly.id, // получение айди из ассембли, который был в конструкторе 
                };
                Core.Context.partassembly.Add(NewPart); // сохранение в таблицу партассембли
            }
            Core.Context.SaveChanges(); // сохранение сохранений в бд
            // код писатьь буквы фу. согласен буквы зло 001001101 ...___... 001110011001 жопа=0 ❌❌❌❌❌❌ 
        }

        private void SavePageBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SaveAssembly());
        }
    }
}
