using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.GeneralPages;
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

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Product product = button.DataContext as Product;

                var newBasket = new Basket()
                {
                    Count = 1,
                    Product_Id = product.Id,
                    User_Id = App.CurrentUser.Id
                };

                App.Connection.Basket.Add(newBasket);
                App.Connection.SaveChanges();
                MessageBox.Show("Товар добавлен в корзину");
            }
            catch
            {
                MessageBox.Show("Ошибка системы!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetBasket_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new BasketPage()));
        }
    }
}
