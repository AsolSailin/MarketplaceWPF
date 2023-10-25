using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages;
using MonkeyShop.Pages.GeneralPages;
using MonkeyShop.Pages.UserPages.ClientPages;
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
                btnExit.Visibility = Visibility.Hidden;
                btnCatalog.Visibility = Visibility.Hidden;
            }
            else if (page is CatalogPage)
            {
                btnAcc.Visibility = Visibility.Visible; 
                btnExit.Visibility = Visibility.Visible;
                btnCatalog.Visibility = Visibility.Hidden;
                btnExit.Content = "ВЫЙТИ";
            }
            else
            {
                btnAcc.Visibility = Visibility.Visible;
                btnCatalog.Visibility = Visibility.Visible;
                btnExit.Visibility = Visibility.Visible;
            }

            if (App.CurrentUser != null)
                if (App.CurrentUser.Role.Title == "Сотрудник пункта выдачи")
                    btnCatalog.Visibility = Visibility.Hidden;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти?", "",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
                NavClass.NextPage(new NavComponentsClass(new AuthorizationPage()));
        }

        private void AccBtn_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new AccountPage(App.CurrentAccount)));
        }

        private void CatalogBtn_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new CatalogPage()));
        }
    }
}
