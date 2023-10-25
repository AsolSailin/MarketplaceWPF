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
        private readonly Func<Product, bool> _filterQuery = x => true;
        private Func<Product, object> _sortQuery = x => x.Id;
        private List<Product> _sorted;
        private static readonly Category _allCategory = new Category() { Title = "Все" };
        private List<Product> Products { get; set; }
        private List<Product> SortedProducts { get; set; }

        public CatalogPage()
        {
            InitializeComponent();
            GetSortingAndFiltering();
            GetList();
        }

        private void GetSortingAndFiltering()
        {
            cbSort.ItemsSource = SortingClass.Methods;

            var categoryList = App.Connection.Category.ToList();
            categoryList.Add(_allCategory);
            cbFilter.ItemsSource = categoryList;

            cbSort.SelectedIndex = -1;
            cbFilter.SelectedIndex = -1;
        }

        private void GetList()
        {
            Products = App.Connection.Product.ToList();
            SortedProducts = Products;
            lvProductList.ItemsSource = SortedProducts;
        }

        private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.IsAdd = false;
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
                .Contains(tboxSearch.Text.ToLower()))
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
            var categorySortComboBoxSelectedItem = cbFilter.SelectedItem as Category;

            if (categorySortComboBoxSelectedItem.Title.Equals("Все"))
                SortedProducts = Products;
            else
                SortedProducts = Products.Where(z => z.Category.Equals(categorySortComboBoxSelectedItem)).ToList();

            FilterAndSort();
        }

        private void FilterAndSort()
        {
            _sorted = SortedProducts.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();
            lvProductList.ItemsSource = SortedProducts.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();

            if (tboxSearch.Text != "")
                Search();
        }
    }
}
