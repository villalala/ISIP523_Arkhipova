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
using System.Windows.Shapes;

namespace PR15.Pages
{
    /// <summary>
    /// Логика взаимодействия для BuildDetailsWindow.xaml
    /// </summary>
    public partial class BuildDetailsWindow : Window
    {
        public assembly assembl {get;set;} // текущая сборка 
        public List<partassembly> partassemblies {get;set; } // список деталей выранной сборки из партассембли 
        public BuildDetailsWindow(assembly asse) // конструууктор 
        {
            InitializeComponent();
            if (asse != null)
            {
                assembl = asse; // сохраняем сборку выбранную 
            }

            //SaveLstBox.ItemsSource = null;
            partassemblies = Core.Context.partassembly.Where(p => p.assemblyid == assembl.id).ToList(); // хз фиьтрация 
            DataContext = this; 
        }
    }
}
