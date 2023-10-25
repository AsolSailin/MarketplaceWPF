using MonkeyShop.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MonkeyShop.DataBase
{
    public partial class Product
    {
        public Visibility ClientVisibilityMethod
        {
            get
            {
                switch (App.CurrentUser.Role.Title)
                {
                    case "Клиент":
                        return Visibility.Visible;
                    case "Менеджер":
                        return Visibility.Collapsed;
                    default:
                        return Visibility.Visible;
                }
            }
        }

        public Visibility ManagerVisibilityMethod
        {
            get
            {
                switch (App.CurrentUser.Role.Title)
                {
                    case "Клиент":
                        return Visibility.Collapsed;
                    case "Менеджер":
                        return Visibility.Visible;
                    default:
                        return Visibility.Visible;
                }
            }
        }

        /*public string BtnClickMethod
        {
            get
            {
                switch (App.CurrentUser.Role.Title)
                {
                    case "Клиент":
                        return "AddProduct_Click";
                    case "Менеджер":
                        return "DeleteProduct_Click";
                    default:
                        return "";
                }
            }
        }*/
    }
}
