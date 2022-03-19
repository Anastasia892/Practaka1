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

namespace Practaka1
{
    /// <summary>
    /// Логика взаимодействия для MinAgent.xaml
    /// </summary>
    public partial class MinAgent : Window
    {
        public MinAgent(Decimal max)
        {
            InitializeComponent();
            TbminAg.Text = max.ToString();
        }
        public Decimal CostAg
        {
            get
            {
                return costAg;
            }
        }

            Decimal costAg;
        private void ButChange_Click(object sender, RoutedEventArgs e)
        {
            costAg = Convert.ToDecimal(TbminAg.Text);
            this.Close();
            
        }

    }
}
