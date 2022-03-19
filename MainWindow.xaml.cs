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

namespace Practaka1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            ClassBD.BD = new Entities(); //  для вызова из любого конца мира (пр) БД
            InitializeComponent();
            FrameClass.Frm = Frame1; // для вызова из любого конца мира фрейма
            FrameClass.Frm.Navigate(new Str1()); // переход на страницу str1 сразу

        }
    }
}
