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
    /// Логика взаимодействия для Str1.xaml
    /// </summary>
    public partial class Str1 : Page
    {
        List<Product> Spisok = ClassBD.BD.Product.ToList(); // создание списка для вывода списка в приложения из БД
        public Str1()
        {
            InitializeComponent();
            ListProdukt.ItemsSource = Spisok; // подкючение БД к лист вью
            Filter.Items.Add("Все типы");
            List<ProductType> mt = ClassBD.BD.ProductType.ToList();
            for (int i = 0; i < mt.Count; i++)
            {
                Filter.Items.Add(mt[i].Title);
            }
            Filter.SelectedIndex = 0;
            Records.Text = "Записей: " + Spisok.Count().ToString() + " из " + Spisok.Count().ToString();
           
        }

        List<Product> SpisokFilter = new List<Product>();

        List<Product> SpisokSeartch = new List<Product>();
        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text != String.Empty)
            {
                SpisokSeartch = Spisok.Where(x => x.Title.Contains(Search.Text) || x.Description.Contains(Search.Text)).ToList();
                FilterSort();
            }
            else
            {
                FilterSort();
            }
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort();
        }

        private void Add_Click(object sender, RoutedEventArgs e) //добавление
        {
            changes change = new changes();
            change.ShowDialog();
            FrameClass.Frm.Navigate(new Str1());



        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort();
        }

        private void ListProdukt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Change_minCoast.Visibility = Visibility.Visible;
        }

        private void Materials_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            int index = Convert.ToInt32(tb.Uid);
            List<ProductMaterial> mtList = ClassBD.BD.ProductMaterial.Where(x => x.MaterialID == index).ToList();
            string str = "";
            
            
            foreach (ProductMaterial item in mtList)
            {
                str += item.Material.Title + ", ";

            }
            if (mtList.Count == 0)
            {
                tb.Text = "Материалы: нет";
            }
            else
            {
            tb.Text = "Материалы: " + str.Substring(0, str.Length - 2);

            }

        }

        private void Redactor_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int index = Convert.ToInt32(button.Uid);
            Product Product = ClassBD.BD.Product.FirstOrDefault(y => y.ID == index);
            changes change = new changes(Product);
            change.ShowDialog();
            FrameClass.Frm.Navigate(new Str1());

        }

        private void FilterSort()
        {
            int filterIndex = Filter.SelectedIndex;

            if (Search.Text != String.Empty)
            {
                if (filterIndex != 0)
                {
                    SpisokFilter = SpisokSeartch.Where(x => x.ProductTypeID == filterIndex).ToList();
                }
                else
                {
                    SpisokFilter = SpisokSeartch;
                }
            }
            else
            {
                if (filterIndex != 0)
                {
                    SpisokFilter = Spisok.Where(x => x.ProductTypeID == filterIndex).ToList();
                }
                else
                {
                    SpisokFilter = Spisok;
                }
            }
            switch (Sort.SelectedIndex)
            {
                case 0:
                    SpisokFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                    break;
                case 1:
                    SpisokFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                    SpisokFilter.Reverse();
                    break;
                case 2:
                    SpisokFilter.Sort((x, y) => x.ProductionWorkshopNumber.CompareTo(y.ProductionWorkshopNumber));
                    break;
                case 3:
                    SpisokFilter.Sort((x, y) => x.ProductionWorkshopNumber.CompareTo(y.ProductionWorkshopNumber));
                    SpisokFilter.Reverse();
                    break;
                case 4:
                    SpisokFilter.Sort((x, y) => x.MinCostForAgent.CompareTo(y.MinCostForAgent));
                    break;
                case 5:
                    SpisokFilter.Sort((x, y) => x.MinCostForAgent.CompareTo(y.MinCostForAgent));
                    SpisokFilter.Reverse();
                    break;

            }

            ListProdukt.ItemsSource = SpisokFilter;
            ListProdukt.Items.Refresh();
            Records.Text = "Записей: " + SpisokFilter.Count().ToString() + " из " + Spisok.Count().ToString();
        }

        private void Change_minCoast_Click(object sender, RoutedEventArgs e)
        {
            var selectedList = ListProdukt.SelectedItems;
            Decimal Costmax = 0;
            foreach (Product pr in selectedList)
            {
                if (pr.MinCostForAgent > Costmax)
                {
                    Costmax = pr.MinCostForAgent;
                }
            }
            MinAgent MinAg = new MinAgent(Costmax);
            MinAg.ShowDialog();
            if (MinAg.CostAg > 0)
            {
                foreach (Product ma in selectedList)
                {
                    ma.MinCostForAgent = MinAg.CostAg;
                }
                ListProdukt.Items.Refresh();
            }
        }
    }
}
