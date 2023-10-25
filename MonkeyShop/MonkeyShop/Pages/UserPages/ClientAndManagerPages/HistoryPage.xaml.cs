using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.UserPages.CommonPages;
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
    /// Логика взаимодействия для HistoryPage.xaml
    /// </summary>
    public partial class HistoryPage : Page
    {
        public HistoryPage()
        {
            InitializeComponent();
            GetList();
        }

        private void GetList()
        {
            if (App.CurrentUser.Role.Title == "Клиент")
                lvOrderList.ItemsSource = App.Connection.Order.Where(x => x.User_Id == App.CurrentUser.Id).ToList();
            else
                lvOrderList.ItemsSource = App.Connection.Order.ToList();
        }

        private void OrderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new OrderPage(lvOrderList.SelectedItem as Order)));
        }
    }
}
