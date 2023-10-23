﻿using MonkeyShop.DataBase;
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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private Product Product { get; set; }

        public ProductPage(Product product)
        {
            InitializeComponent();
            Product = product;
            DataContext = Product;
            tboxCost.Text = product.Cost.ToString() + "р.";
        }
    }
}
