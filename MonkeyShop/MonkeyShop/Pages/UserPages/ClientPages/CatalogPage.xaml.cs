using MonkeyShop.Classes;
using MonkeyShop.DataBase;
using MonkeyShop.Pages.GeneralPages;
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
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        private int number = 0;
        private Func<Product, bool> _filterQuery = x => true;
        private Func<Product, object> _sortQuery = x => x.Id;
        private readonly List<Product> _products;
        private readonly string _allProduct = "Все";
        private List<Product> _sorted;
        private string btnStringClick;

        public CatalogPage()
        {
            InitializeComponent();

            _products = App.Connection.Product.ToList();

            cbSort.ItemsSource = SortingClass.Methods;
            cbFilter.ItemsSource = App.Connection.Category.ToList();
            cbSort.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;

            GetList();
        }

        private void GetList()
        {
            lvProductList.ItemsSource = App.Connection.Product.ToList();
            /*IEnumerable<Product> products = App.Connection.Product;
            products = products.Skip(number).Take(2);
            lvProductList.ItemsSource = products;*/
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass(new ProductPage(lvProductList.SelectedItem as Product)));
        }

        private void BackProduct_Click(object sender, RoutedEventArgs e)
        {
            number--;

            if (number < 0)
                number = 0;

            GetList();
        }

        private void NextProduct_Click(object sender, RoutedEventArgs e)
        {
            number++;

            if (lvProductList.Items.Count < 2)
                number--;

            GetList();
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Product product = button.DataContext as Product;

                var newBasket = new Basket()
                {
                    Count = 1,
                    Product_Id = product.Id,
                    User_Id = App.CurrentUser.Id
                };

                App.Connection.Basket.Add(newBasket);
                App.Connection.SaveChanges();
                MessageBox.Show("Товар добавлен в корзину");
            }
            catch
            {
                MessageBox.Show("Ошибка системы!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Product product = button.DataContext as Product;

                if (MessageBox.Show("Вы действительно хотите удалить данный товар?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Connection.Product.Remove(product);
                    App.Connection.SaveChanges();
                }

                GetList();
            }
            catch
            {
                MessageBox.Show("Ошибка данных!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            lvProductList.ItemsSource = _sorted
                .Where(x => string.Join(" ", x.Title)
                .ToLower()
                .Contains(tbSearch.Text.ToLower()))
                .ToList();
        }

        private void SortingSelect(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    _sortQuery = x => x.Id;
                    break;
                case 1:
                    _sortQuery = x => x.Cost;
                    break;
                case 2:
                    _sortQuery = x => -x.Cost;
                    break;
            }

            FilterAndSort();
        }

        private void FilteringSelect(object sender, SelectionChangedEventArgs e)
        {
            FilterAndSort();
        }

        private void FilterAndSort()
        {
            _sorted = _products.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();
            lvProductList.ItemsSource = _products.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();

            if (tbSearch.Text != "")
                Search();
        }
    }
}
