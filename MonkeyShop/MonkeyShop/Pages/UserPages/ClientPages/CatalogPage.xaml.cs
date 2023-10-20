using MonkeyShop.ADO;
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

namespace MonkeyShop.Pages.UserPages.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
            lvProductList.ItemsSource = App.Connection.Product.ToList();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            //var item = (Product)((Button)sender).Tag;
        }
    }
}
