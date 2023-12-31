﻿using MonkeyShop.DataBase   ;
using MonkeyShop.Classes;
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

namespace MonkeyShop.Pages.GeneralPages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (tboxSurname.Text != "" && tboxName.Text != "" && tboxPatronymic.Text != "" &&
                tboxLogin.Text != "" && pboxPassword.Password != "")
            {
                User newUser = new User()
                {
                    Surname = tboxSurname.Text,
                    Name = tboxName.Text,
                    Patronymic = tboxPatronymic.Text,
                    Role = App.Connection.Role.Where(x => x.Id == 1).FirstOrDefault()
                };
                Account newAccount = new Account()
                {
                    Login = tboxLogin.Text,
                    Password = pboxPassword.Password
                };

                newUser.Account.Add(newAccount);
                App.Connection.User.Add(newUser);
                App.Connection.Account.Add(newAccount);
                App.Connection.SaveChanges();
                MessageBox.Show("Регистрация прошла успешно!");
                NavClass.NextPage(new NavComponentsClass(new AuthorizationPage()));
            }
            else
            {
                MessageBox.Show("Неверные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAuthBtn_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new AuthorizationPage()));
        }
    }
}
