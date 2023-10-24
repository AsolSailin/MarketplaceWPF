using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        public int? Price { get; set; }

        public BasketPage()
        {
            InitializeComponent();
            GetList();
            cbPoint.ItemsSource = App.Connection.IssuePoint.ToList();
        }

        private void GetList()
        {
            lvProductList.ItemsSource = App.Connection.Basket.Where(x => x.User_Id == App.CurrentUser.Id).ToList();
            Price = 0;

            foreach (Basket basket in lvProductList.ItemsSource)
            {
                Price += basket.Count * basket.Product.Cost;
            }
            
            tbPrice.Text = Price.ToString() + "р.";
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Basket basket = button.DataContext as Basket;

            if (basket.Count == 1)
            {
                if (MessageBox.Show("Вы действительно хотите убрать товар из корзины?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Connection.Basket.Remove(basket);
                }
            }
            else
            {
                basket.Count++;
                App.Connection.Basket.AddOrUpdate(basket);
            }

            App.Connection.SaveChanges();
            GetList();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Basket basket = button.DataContext as Basket;
            basket.Count++;
            App.Connection.Basket.AddOrUpdate(basket);
            App.Connection.SaveChanges();
            GetList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Basket basket = button.DataContext as Basket;

                if (MessageBox.Show("Вы действительно хотите убрать товар из корзины?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Connection.Basket.Remove(basket);
                    App.Connection.SaveChanges();
                }

                GetList();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.CurrentIssuePoint != null)
                {
                    var userBasket = App.Connection.Basket.Where(x => x.User_Id == App.CurrentUser.Id);

                    var newOrder = new Order()
                    {
                        PlacingDate = DateTime.Now,
                        PurchaseAmount = Price,
                        IssuePoint = App.CurrentIssuePoint,
                        User = App.CurrentUser,
                        Status_Id = 1
                    };

                    App.Connection.Order.Add(newOrder);

                    foreach (var basketProduct in userBasket)
                    {
                        var newProductOrder = new ProductOrder()
                        {
                            Count = basketProduct.Count,
                            Product = basketProduct.Product,
                            Order = newOrder
                        };

                        App.Connection.ProductOrder.Add(newProductOrder);
                        App.Connection.Basket.Remove(basketProduct);
                    }

                    App.Connection.SaveChanges();
                    MessageBox.Show("Заказ успешно оформлен");
                    GetList();
                    App.CurrentIssuePoint = null;
                }
                else
                {
                    MessageBox.Show("Для оформления заказа необходимо выбрать пункт выдачи!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new HistoryPage()));
        }

        private void Point_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.CurrentIssuePoint = cbPoint.SelectedItem as IssuePoint;
        }
    }
}
