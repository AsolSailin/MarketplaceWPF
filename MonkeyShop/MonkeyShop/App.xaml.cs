using MonkeyShop.DataBase;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MonkeyShop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MonkeyShopEntities Connection = new MonkeyShopEntities();
        public static bool isAuth = false;
        public static User CurrentUser;
        public static Account CurrentAccount;
        public static IssuePoint CurrentIssuePoint;
    }
}
