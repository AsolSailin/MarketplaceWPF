using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages;
using MonkeyShop.Pages.GeneralPages;
using MonkeyShop.Pages.UserPages.ClientPages;
using MonkeyShop.Pages.UserPages.CommonPages;
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

namespace MonkeyShop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigated += MainFrame_Navigated;
            NavClass.main = this;
            NavClass.NextPage(new NavComponentsClass(new AuthorizationPage()));
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;

            if (page is AuthorizationPage)
            {
                btnAcc.Visibility = Visibility.Hidden; 
                btnBack.Visibility = Visibility.Hidden;
            }
            else if (page is CatalogPage)
            {
                btnAcc.Visibility = Visibility.Visible; 
                btnBack.Visibility = Visibility.Visible;
                btnBack.Content = "ВЫЙТИ";
            }
            else
            {
                btnAcc.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Visible;
                btnBack.Content = "НАЗАД";
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (btnBack.Content)
            {
                case "ВЫЙТИ":
                    if (MessageBox.Show("Вы действительно хотите выйти?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        NavClass.NextPage(new NavComponentsClass(new AuthorizationPage()));
                    }
                    break;
                case "НАЗАД":
                    NavClass.BackPage();
                    break;
            }
        }

        private void btnAcc_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new AccountPage(App.CurrentAccount)));
        }
    }
}
