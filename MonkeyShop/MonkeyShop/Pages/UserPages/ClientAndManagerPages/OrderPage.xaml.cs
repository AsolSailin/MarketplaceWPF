using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.UserPages.ClientPages;
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

namespace MonkeyShop.Pages.UserPages.CommonPages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private OrderStatus CurrentStatus { get; set; }
        private Order CurrentOrder { get; set; }

        public OrderPage(Order order)
        {
            InitializeComponent();
            DataContext = order;
            CurrentOrder = order;
            GetList();
            GetStatus();
        }

        private void GetList()
        {
            lvProductList.ItemsSource = App.Connection.ProductOrder.Where(x => x.Order_Id == CurrentOrder.Id).ToList();
        }

        private void GetStatus()
        {
            cbStatus.ItemsSource = App.Connection.OrderStatus.Where(x => x.Id >= CurrentOrder.Status_Id).ToList();
            cbStatus.SelectedIndex = 0;
        }

        private void Status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentStatus = cbStatus.SelectedItem as OrderStatus;
            CurrentOrder.OrderStatus = CurrentStatus;

            App.Connection.Order.AddOrUpdate(CurrentOrder);
            App.Connection.SaveChanges();
        }

        private void GetOrderList_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new HistoryPage()));
        }
    }
}
