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
        public BasketPage()
        {
            InitializeComponent();
            GetList();
        }

        private void GetList()
        {
            lvProductList.ItemsSource = App.Connection.Basket.Where(x => x.User_Id == App.CurrentUser.Id).ToList();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Basket basket = button.DataContext as Basket;
            basket.Count++;
            App.Connection.Basket.AddOrUpdate(basket);
            App.Connection.SaveChanges();
            GetList();
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Basket basket = button.DataContext as Basket;

            if(basket.Count == 1)
            {
                if (MessageBox.Show("Вы действительно хотите убрать товар из карзины?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Connection.Basket.Remove(basket);
                }
            }
            else
            {
                basket.Count--;
                App.Connection.Basket.AddOrUpdate(basket);
            }

            App.Connection.SaveChanges();
            GetList();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Basket basket = button.DataContext as Basket;

                if (MessageBox.Show("Вы действительно хотите убрать товар из карзины?", "",
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
                MessageBox.Show("Данная услуга не может быть удалена, так как на нее записаны клиенты", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
