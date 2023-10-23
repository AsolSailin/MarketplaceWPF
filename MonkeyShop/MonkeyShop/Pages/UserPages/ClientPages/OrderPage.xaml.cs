using MonkeyShop.DataBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public OrderPage(Order order)
        {
            InitializeComponent();
            DataContext = order;
            GetList(order);
            tboxPrice.Text = order.PurchaseAmount.ToString();
        }

        private void GetList(Order order)
        {
            lvProductList.ItemsSource = App.Connection.ProductOrder.Where(x => x.Order_Id == order.Id).ToList();
        }
    }
}
