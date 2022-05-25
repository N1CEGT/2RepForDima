using Lopushok.Models;
using Lopushok.ViewModels;
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
using System.Windows.Shapes;

namespace Lopushok.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        public ProductsWindow()
        {
            InitializeComponent();

            var sortOptions = new string[] {
                "Наименование ⬆",
                "Наименование ⬇",
                "Номер производственного цеха ⬆",
                "Номер производственного цеха ⬇",
                "Минимальная стоимость для агента ⬆",
                "Минимальная стоимость для агента ⬇",
            };
            SortByComboBox.ItemsSource = sortOptions;

            var filterOptions = App.DB.ProductTypes.AsNoTracking().ToList();
            filterOptions.Insert(0, new Models.ProductType
            {
                Name = "Все типы"
            });
            FilterByComboBox.DisplayMemberPath = "Name";
            FilterByComboBox.ItemsSource = filterOptions;

            ChangeCostButton.Visibility = Visibility.Collapsed;

            LoadData();
        }

        private int currentPage = 1;
        private int perPage = 20;
        private int totalPages;

        private void LoadData()
        {
            var products = App.DB.Products
                .OrderBy(x => x.Id)
                .AsQueryable();

            if (!string.IsNullOrEmpty(SearchTextBox.Text))
            {
                products = products.Where(x => x.Name.Contains(SearchTextBox.Text) || x.Description.Contains(SearchTextBox.Text));
            }

            if (FilterByComboBox.SelectedIndex > 0)
            {
                if (FilterByComboBox.SelectedItem is ProductType productType)
                {
                    products = products.Where(x => x.ProductTypeId == productType.Id);
                }
            }

            if (SortByComboBox.SelectedIndex != -1)
            {
                switch (SortByComboBox.SelectedIndex)
                {
                    case 0:
                        products = products.OrderBy(x => x.Name);
                        break;
                    case 1:
                        products = products.OrderByDescending(x => x.Name);
                        break;
                    case 2:
                        products = products.OrderBy(x => x.WorkshopId);
                        break;
                    case 3:
                        products = products.OrderByDescending(x => x.WorkshopId);
                        break;
                    case 4:
                        products = products.OrderBy(x => x.MinimumCostForAgent);
                        break;
                    case 5:
                        products = products.OrderByDescending(x => x.MinimumCostForAgent);
                        break;
                }
            }

            if (products.Count() > 20)
            {
                totalPages = (int)Math.Ceiling((decimal)products.Count() / (decimal)perPage);
                var pages = new List<int>();
                for (int i = 1; i <= totalPages; i++)
                    pages.Add(i);
                PagesListView.ItemsSource = pages;

                products = products
                    .Skip((currentPage - 1) * perPage)
                    .Take(perPage);

                PaginationGrid.Visibility = Visibility.Visible;
            }
            else
            {
                PaginationGrid.Visibility = Visibility.Collapsed;
            }

            ProductsListView.ItemsSource = products.ToList().Select(x => new ProductViewModel(
                x,
                string.IsNullOrEmpty(x.Image) ? Visibility.Visible : Visibility.Collapsed,
                x.Image?.Replace("products", @"Images\Products").Replace("jpg", "jpeg"),
                x.ProductType.Name,
                x.Name,
                x.NumberArticle,
                string.Join(" ,", x.ProductsMaterials.Select(y => y.Material.Name)),
                x.ProductsMaterials.Any() ?
                    x.ProductsMaterials.Select(y => y.Material.Cost * y.AmountOfMaterial).Sum()
                    :
                    x.MinimumCostForAgent
            ));

            if (ProductsListView.Items.Count == 0)
            {
                MessageBox.Show("Поиск не дал результатов", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (PaginationGrid.IsVisible && PagesListView.SelectedValue == null)
            {
                PagesListView.SelectedValue = currentPage;
            }
        }

        private void ProductsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsListView.SelectedItem is ProductViewModel viewModel)
            {
                var editWindow = new Windows.AddEditProductWindow(viewModel.Product)
                {
                    Owner = this
                };
                editWindow.ShowDialog();
                LoadData();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ShowHideButton();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ShowHideButton();
        }

        private void ShowHideButton()
        {
            if (ProductsListView.Items.Cast<ProductViewModel>().Count(x => x.Checked) > 1)
            {
                ChangeCostButton.Visibility = Visibility.Visible;
            }
            else
            {
                ChangeCostButton.Visibility = Visibility.Collapsed;
            }
        }

        private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void FilterByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void ChangeCostButton_Click(object sender, RoutedEventArgs e)
        {
            var products = ProductsListView.Items.Cast<ProductViewModel>().Where(x => x.Checked).ToList();
            if (!products.Any())
            {
                MessageBox.Show("Поставьте галку минимум у двух продуктов!", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new Windows.AddEditProductWindow()
            {
                Owner = this
            };
            addWindow.ShowDialog();
            LoadData();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PagesListView.SelectedValue is int page)
            {
                currentPage = page;
                DisableEnableButtons();
                LoadData();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage + 1 > totalPages)
            {
                NextPage.IsEnabled = false;
                return;
            }
            else
            {
                NextPage.IsEnabled = true;
            }

            currentPage++;
            PagesListView.SelectedValue = currentPage;
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage - 1 <= 0)
            {
                PreviousPage.IsEnabled = false;
                return;
            }
            else
            {
                PreviousPage.IsEnabled = true;
            }

            currentPage--;
            PagesListView.SelectedValue = currentPage;
        }

        private void DisableEnableButtons()
        {
            if (currentPage + 1 > totalPages)
            {
                NextPage.IsEnabled = false;
            }
            else
            {
                NextPage.IsEnabled = true;
            }

            if (currentPage - 1 <= 0)
            {
                PreviousPage.IsEnabled = false;
            }
            else
            {
                PreviousPage.IsEnabled = true;
            }
        }
    }
}
