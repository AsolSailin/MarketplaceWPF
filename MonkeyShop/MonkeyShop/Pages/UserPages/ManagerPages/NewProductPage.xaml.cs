using Microsoft.Win32;
using MonkeyShop.DataBase;
using MonkeyShop.Classes;
using MonkeyShop.Pages.GeneralPages;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace MonkeyShop.Pages.UserPages.ManagerPages
{
    /// <summary>
    /// Логика взаимодействия для NewProductPage.xaml
    /// </summary>
    public partial class NewProductPage : Page
    {
        public byte[] Image { get; set; }

        public NewProductPage()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tbTitle.Text != "" && tbCost.Text != "" && tbDescription.Text != "" && Image != null)
                {
                    var newProduct = new Product()
                    {
                        Title = tbTitle.Text,
                        Cost = int.Parse(tbCost.Text),
                        Description = tbDescription.Text,
                        Image = Image
                    };

                    App.Connection.Product.Add(newProduct);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Товар успешно дабавлен!");
                    NavClass.NextPage(new NavComponentsClass(new NewProductPage()));
                }
                else
                {
                    MessageBox.Show("Неверные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch
            {
                MessageBox.Show("Неверные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ImageBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as Button;
                var dialog = new OpenFileDialog();

                if (dialog.ShowDialog() != null)
                {
                    Image = File.ReadAllBytes(dialog.FileName);
                    imgImage.Source = new BitmapImage(new Uri(dialog.FileName));
                }
            }
            catch
            {
                MessageBox.Show("Неверный формат файла!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
