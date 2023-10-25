using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.GeneralPages;
using MonkeyShop.Pages.UserPages.ClientPages;
using MonkeyShop.Pages.UserPages.EmployeePages;
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
using System.Xml.Linq;

namespace MonkeyShop.Pages.UserPages.CommonPages
{
    /// <summary>
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private bool editBtnClick = true;

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
                    btnNavTwo.Visibility = Visibility.Visible;
                    btnNavOne.Content = "ДОБАВИТЬ ТОВАР";
                    btnNavTwo.Content = "ЗАКАЗЫ";
                    btnNavOne.Click += NewProduct_Click;
                    btnNavTwo.Click += Order_Click;
                    break;
                case "Сотрудник пункта выдачи":
                    btnNavOne.Visibility = Visibility.Hidden;
                    btnNavTwo.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (editBtnClick)
            {
                tboxSurname.IsReadOnly = false;
                tboxName.IsReadOnly = false;
                tboxPatronymic.IsReadOnly = false;
                tboxLogin.IsReadOnly = false;
                tboxPassword.IsReadOnly = false;

                MessageBox.Show("Вы перешли в режим редактирования! Для сохранения изменений необходимо нажать кнопку снова!");
                editBtnClick = false;
            }
            else
            {
                if (tboxSurname.Text != "" && tboxName.Text != "" && tboxPatronymic.Text != "" &&
                    tboxLogin.Text != "" && tboxPassword.Text != "")
                {
                    App.CurrentUser = new User()
                    {
                        Surname = tboxSurname.Text,
                        Name = tboxName.Text,
                        Patronymic = tboxPatronymic.Text
                    };
                    App.CurrentAccount = new Account()
                    {
                        Login = tboxLogin.Text,
                        Password = tboxPassword.Text
                    };

                    App.CurrentUser.Account.Add(App.CurrentAccount);
                    App.Connection.User.Add(App.CurrentUser);
                    App.Connection.Account.Add(App.CurrentAccount);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Данные аккаунта успешно изменены!");
                }
                else
                {
                    MessageBox.Show("Неверные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить аккаунт?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                App.Connection.Account.Remove(App.CurrentAccount);
                App.Connection.User.Remove(App.CurrentUser);
                App.Connection.SaveChanges();
                MessageBox.Show("Аккаунт был успешно удален!");
                NavClass.NextPage(new NavComponentsClass(new AuthorizationPage()));
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
            App.IsAdd = true;
            var product = new Product();
            NavClass.NextPage(new NavComponentsClass(new ProductPage(product)));
        }
    }
}
