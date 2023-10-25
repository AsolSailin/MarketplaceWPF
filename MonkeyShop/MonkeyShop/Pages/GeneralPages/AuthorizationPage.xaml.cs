using MonkeyShop.Classes;
using MonkeyShop.Pages.UserPages.ClientPages;
using MonkeyShop.Pages.UserPages.EmployeePages;
using System;
using System.Collections.Generic;
using System.Data.Common;
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

namespace MonkeyShop.Pages.GeneralPages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void AuthoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "" && tbPassword.Password != "")
            {
                var data = App.Connection.Account.Where(x => x.Password == tbPassword.Password
                && x.Login == tbLogin.Text).FirstOrDefault();

                if (data != null)
                {
                    App.CurrentAccount = data;
                    App.CurrentUser = App.Connection.User.Where(x => x.Id == data.User_Id).FirstOrDefault();

                    switch (App.CurrentUser.Role.Title)
                    {
                        case "Клиент":
                            NavClass.NextPage(new NavComponentsClass(new CatalogPage()));
                            break;
                        case "Менеджер":
                            NavClass.NextPage(new NavComponentsClass(new CatalogPage()));
                            break;
                        case "Сотрудник пункта выдачи":
                            NavClass.NextPage(new NavComponentsClass(new IssuancePage()));
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин и пароль, пожалуйста!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new RegistrationPage()));
        }
    }
}
