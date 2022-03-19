using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Practaka1
{
    /// <summary>
    /// Логика взаимодействия для changes.xaml
    /// </summary>
    public partial class changes : Window
    {
        string img_pat;

        bool Bool = false;
        List<ProductMaterial> PM = ClassBD.BD.ProductMaterial.ToList();
        Product product = new Product();
        public changes()
        {
            InitializeComponent();
            Cb_Tip.ItemsSource = ClassBD.BD.ProductType.ToList();
            Cb_Tip.SelectedValuePath = "ID";
            Cb_Tip.DisplayMemberPath = "Title";

            Material_combo.ItemsSource = ClassBD.BD.Material.ToList();
            Material_combo.SelectedValuePath = "ID";
            Material_combo.DisplayMemberPath = "Title";
            Bool = true;
            Vivod.SelectedValuePath = "ID";
            Vivod.DisplayMemberPath = "Title";

        }

        public changes(Product Edit_zapic)
        {
            InitializeComponent();
            Cb_Tip.ItemsSource = ClassBD.BD.ProductType.ToList();
            Cb_Tip.SelectedValuePath = "ID";
            Cb_Tip.DisplayMemberPath = "Title";

            Material_combo.ItemsSource = ClassBD.BD.Material.ToList();
            Material_combo.SelectedValuePath = "ID";
            Material_combo.DisplayMemberPath = "Title";
            Vivod.SelectedValuePath = "ID";
            Vivod.DisplayMemberPath = "Title";
            product = Edit_zapic;
            Arcticl_box.Text = product.ArticleNumber;
            Name_box.Text = product.Title.ToString();
            Cb_Tip.SelectedIndex = product.ProductTypeID - 1;
            People_box.Text = product.ProductionPersonCount.ToString();
            Namber_box.Text = product.ProductionWorkshopNumber.ToString();
            Min_box.Text = product.MinCostForAgent.ToString();

            if (product.Image != null)
            {
                BitmapImage BI = new BitmapImage(new Uri(product.Image, UriKind.RelativeOrAbsolute));
                Plug.Source = BI;
            }
            List<Material> materials = new List<Material>();

            foreach (ProductMaterial t in ClassBD.BD.ProductMaterial)
            {
                if (t.ProductID == product.ID)
                {
                    List<Material> tempS = ClassBD.BD.Material.Where(x => x.ID == t.ProductID).ToList();
                    materials.AddRange(tempS);
                }
            }

            foreach (Material mat in materials)
            {
                Vivod.Items.Add(mat);
            }
            Vivod.SelectedValuePath = "ID";
            Vivod.DisplayMemberPath = "Title";


        }

        private void Add_but_Click(object sender, RoutedEventArgs e)
        {

            product.Title = Name_box.Text;
            product.ArticleNumber = Arcticl_box.Text.ToString();
            product.ProductTypeID = Cb_Tip.SelectedIndex + 1;
            product.ProductionPersonCount = Convert.ToInt32(People_box.Text);
            product.ProductionWorkshopNumber = Convert.ToInt32(Namber_box.Text);
            product.MinCostForAgent = Convert.ToDecimal(Min_box.Text);
            product.Description = Descripct_box.Text;
            product.Image = img_pat;
            if (Bool == true)
            {
                ClassBD.BD.Product.Add(product);
            }
            ClassBD.BD.SaveChanges();
            List<ProductMaterial> productMaterials = PM.Where(x => x.MaterialID == product.ID).ToList();
            if (productMaterials.Count != 0)
            {
                foreach (ProductMaterial pm in productMaterials)
                {
                    ClassBD.BD.ProductMaterial.Remove(pm);
                }
            }
            foreach (Material t in Vivod.Items)
            {
                ProductMaterial pm = new ProductMaterial();
                pm.ProductID = product.ID;
                pm.MaterialID = t.ID;
                ClassBD.BD.ProductMaterial.Add(pm);
            }
            ClassBD.BD.SaveChanges();
            MessageBox.Show("Запись занесена", "Редактура", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            this.Close();
        }


        private void Add_opis_but_Click(object sender, RoutedEventArgs e)
        {
            List<Material> sup = ClassBD.BD.Material.ToList();
            Vivod.Items.Add(sup.FirstOrDefault(x => x.ID == Material_combo.SelectedIndex + 1));
        }

        private void Del_opis_but_Click(object sender, RoutedEventArgs e)
        {
            Vivod.Items.RemoveAt(Vivod.SelectedIndex);
        }

        private void Add_img_but_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog OFD = new OpenFileDialog();
                OFD.ShowDialog();
                img_pat = OFD.FileName;
                int n = img_pat.IndexOf("materials");
                img_pat = img_pat.Substring(n);
                BitmapImage img = new BitmapImage(new Uri(img_pat, UriKind.RelativeOrAbsolute));
                Plug.Source = img;
            }
            catch
            {
                MessageBox.Show("Картинка не выбрана", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        private void Delite_but_Click(object sender, RoutedEventArgs e)
        {
            if (Bool == true)
            {
                MessageBox.Show("Невозможно удалить запись, так как она еще не существует", "Редактирование", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (MessageBoxResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить эту запись?", "Редактирование", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                ClassBD.BD.Product.Remove(product);
                ClassBD.BD.SaveChanges();
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
