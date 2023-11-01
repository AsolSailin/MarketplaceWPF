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
        private Category CurrentCategory { get; set; }
        private byte[] Image { get; set; }
        private bool editBtnClick = true;

        public ProductPage(Product product)
        {
            InitializeComponent();
            DataContext = product;
            CurrentProduct = product;
            GetManagerBtn();
        }

        private void GetManagerBtn()
        {
            cbCategory.ItemsSource = App.Connection.Category.ToList();

            if (App.CurrentUser.Role.Title == "Менеджер")
            {
                if (App.IsAdd)
                {
                    btnAdd.Visibility = Visibility.Visible;
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                    tboxTitle.IsReadOnly = false;
                    tboxCost.IsReadOnly = false;
                    tboxDescription.IsReadOnly = false;
                    btnImage.IsEnabled = true;
                    cbCategory.IsEnabled = true;
                    cbCategory.SelectedIndex = -1;
                }
                else
                {
                    btnAdd.Visibility = Visibility.Collapsed;
                    btnEdit.Visibility = Visibility.Visible;
                    btnDelete.Visibility = Visibility.Visible;
                    tboxTitle.IsReadOnly = true;
                    tboxCost.IsReadOnly = true;
                    tboxDescription.IsReadOnly = true;
                    btnImage.IsEnabled = false;
                    cbCategory.IsEnabled = false;
                    cbCategory.SelectedIndex = App.Connection.Category.Where(x => x.Id == CurrentProduct.Category_Id).FirstOrDefault().Id - 1;
                }
            }

            switch (App.CurrentUser.Role.Title)
            {
                case "Клиент":
                    tboxCategory.Visibility = Visibility.Visible;
                    cbCategory.Visibility = Visibility.Collapsed;
                    break;
                case "Менеджер":
                    tboxCategory.Visibility = Visibility.Collapsed;
                    cbCategory.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentCategory = cbCategory.SelectedItem as Category;
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
                        Category = CurrentCategory,
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
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (editBtnClick)
                {
                    tboxTitle.IsReadOnly = false;
                    tboxCost.IsReadOnly = false;
                    tboxDescription.IsReadOnly = false;
                    btnImage.IsEnabled = true;
                    editBtnClick = false;

                    MessageBox.Show("Вы перешли в режим редактирования! Для сохранения изменений необходимо нажать кнопку снова!");
                }
                else
                {
                    if (tboxTitle.Text != "" && tboxCategory.Text != "" &&
                        tboxCost.Text != "" && tboxDescription.Text != "")
                    {
                        App.Connection.SaveChanges();
                        MessageBox.Show("Товар успешно изменен!");

                        tboxTitle.IsReadOnly = true;
                        tboxCost.IsReadOnly = true;
                        tboxDescription.IsReadOnly = true;
                        btnImage.IsEnabled = false;
                        editBtnClick = true;
                    }
                    else
                    {
                        MessageBox.Show("Все поля должны быть заполнены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы действительно хотите удалить данный товар?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    CurrentProduct.IsDeleted = true;

                    App.Connection.SaveChanges();
                    MessageBox.Show("Товар был успешно удален!");
                    NavClass.NextPage(new NavComponentsClass(new CatalogPage()));
                }
            }
            catch
            {
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
