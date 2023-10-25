using Microsoft.Win32;
using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.GeneralPages;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace MonkeyShop.Pages.UserPages.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private Product CurrentProduct { get; set; }
        private byte[] Image { get; set; }
        private bool editBtnClick = true;

        public ProductPage(Product product)
        {
            InitializeComponent();
            GetManagerBtn();

            DataContext = product;
            CurrentProduct = product;
        }

        private void GetManagerBtn()
        {
            if (App.CurrentUser.Role.Title == "Manager")
            {
                if (App.IsAdd)
                {
                    btnAdd.Visibility = Visibility.Visible;
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                }
                else
                {
                    btnAdd.Visibility = Visibility.Collapsed;
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                }
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (tboxTitle.Text != "" && tboxCost.Text != "" && tboxDescription.Text != "" && Image != null)
                {
                    var newProduct = new Product()
                    {
                        Title = tboxTitle.Text,
                        Cost = int.Parse(tboxCost.Text),
                        Description = tboxDescription.Text,
                        Image = Image,
                        IsDeleted = false
                    };

                    App.Connection.Product.Add(newProduct);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Товар успешно дабавлен!");
                    NavClass.NextPage(new NavComponentsClass(new CatalogPage()));
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

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if (editBtnClick)
            {
                tboxTitle.IsReadOnly = false;
                tboxCost.IsReadOnly = false;
                tboxDescription.IsReadOnly = false;
                tboxCategory.IsEnabled = true;
                editBtnClick = false;

                MessageBox.Show("Вы перешли в режим редактирования! Для сохранения изменений необходимо нажать кнопку снова!");
            }
            else
            {
                if (tboxTitle.Text != "" && tboxCategory.Text != "" && 
                    tboxCost.Text != "" && tboxDescription.Text != "")
                {
                    CurrentProduct = new Product()
                    {
                        Title = tboxTitle.Text,
                        Cost = int.Parse(tboxCost.Text),
                        Description = tboxDescription.Text,
                    };

                    App.Connection.Product.AddOrUpdate(CurrentProduct);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Товар успешно изменен!");

                    tboxTitle.IsReadOnly = true;
                    tboxCost.IsReadOnly = true;
                    tboxDescription.IsReadOnly = true;
                    tboxCategory.IsEnabled = false;
                    editBtnClick = true;
                }
                else
                {
                    MessageBox.Show("Неверные данные!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить данный товар?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                CurrentProduct.IsDeleted = true;

                App.Connection.Product.AddOrUpdate(CurrentProduct);
                App.Connection.SaveChanges();
                MessageBox.Show("Товар был успешно удален!");
                NavClass.NextPage(new NavComponentsClass(new CatalogPage()));
            }
        }
    }
}
