using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.GeneralPages;
using MonkeyShop.Pages.UserPages.ClientPages;
using MonkeyShop.Pages.UserPages.EmployeePages;
using MonkeyShop.Pages.UserPages.ManagerPages;
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

namespace MonkeyShop.Pages.UserPages.CommonPages
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage(Account account)
        {
            InitializeComponent();
            DataContext = account;
            GetNav();
        }

        private void GetNav()
        {
            switch (App.CurrentUser.Role.Title)
            {
                case "Клиент":
                    btnNavOne.Visibility = Visibility.Visible;
                    btnNavTwo.Visibility = Visibility.Visible;
                    btnNavOne.Content = "КОРЗИНА";
                    btnNavTwo.Content = "ЗАКАЗЫ";
                    btnNavOne.Click += Basket_Click;
                    btnNavTwo.Click += Order_Click;
                    break;
                case "Менеджер":
                    btnNavOne.Visibility = Visibility.Visible;
                    btnNavTwo.Visibility = Visibility.Hidden;
                    btnNavOne.Content = "ДОБАВИТЬ ТОВАР";
                    btnNavOne.Click += NewProduct_Click;
                    break;
                case "Сотрудник пункта выдачи":
                    btnNavOne.Visibility = Visibility.Hidden;
                    btnNavTwo.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void Basket_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new BasketPage()));
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new HistoryPage()));
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new NewProductPage()));
        }
    }
}
