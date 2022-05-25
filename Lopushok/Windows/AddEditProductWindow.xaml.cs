using Lopushok.Models;
using Microsoft.Win32;
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
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Lopushok.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductWindow.xaml
    /// </summary>
    public partial class AddEditProductWindow : Window
    {
        public Product Product { get; set; } = new Product();

        public AddEditProductWindow(Product product = null)
        {
            InitializeComponent();

            if (product != null)
            {
                Product = product;
                Title = "Редактирование продукции";

                if (!string.IsNullOrEmpty(Product.Image))
                {
                    SelectPhoto.Content = "Изменить фото";
                }
            }
            else
            {
                DeleteButton.Visibility = Visibility.Collapsed;
            }

            MaterialsComboBox.ItemsSource = App.DB.Materials.ToList();
            MaterialsComboBox.DisplayMemberPath = "Name";

            ProductTypeComboBox.ItemsSource = App.DB.ProductTypes.ToList();
            ProductTypeComboBox.DisplayMemberPath = "Name";

            WorkshopComboBox.ItemsSource = App.DB.Workshops.ToList();
            WorkshopComboBox.DisplayMemberPath = "Id";

            DataContext = Product;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) 
        {
            try
            {
                var errors = new List<string>();

                if (string.IsNullOrEmpty(NameTextBox.Text) ||
                    string.IsNullOrEmpty(NumberArticleTextBox.Text) ||
                    string.IsNullOrEmpty(MinimumPersonForProductionTextBox.Text) ||
                    string.IsNullOrEmpty(MinimumCostForAgentTextBox.Text) ||
                    ProductTypeComboBox.SelectedIndex == -1 ||
                    WorkshopComboBox.SelectedIndex == -1)
                {
                    MessageBox.Show("Заполните все поля!", "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                if (!int.TryParse(NumberArticleTextBox.Text, out int article))
                {
                    errors.Add("Артикул должен быть числом!");
                }
                else
                {
                    if (Product.Id == 0 && App.DB.Products.Any(x => x.NumberArticle == article))
                    {
                        errors.Add("Продукт с таким артикулом уже есть в базе!");
                    }
                }

                if (!int.TryParse(MinimumPersonForProductionTextBox.Text, out int minimumPersonForProduction))
                {
                    errors.Add("Количество человек для производства должно быть числом!");
                }

                if (!int.TryParse(MinimumCostForAgentTextBox.Text, out int minimumCostForAgent))
                {
                    errors.Add("Мнимальная стоимость для агента должна быть числом!");
                }

                if (errors.Any())
                {
                    MessageBox.Show(Title + " невозможно из-за ошибок:\n" + string.Join("\n", errors), "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                    return;
                }

                if (Product.Id == 0)
                {
                    var product = App.DB.Products.Add(Product);

                }

                App.DB.SaveChanges();
                DialogResult = true;
            }
            catch
            {
                MessageBox.Show("Ошибка работы с базой!", "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (Product.Id > 0)
                App.DB.Entry(Product).Reload();

            DialogResult = false;
        }

        private void SelectPhoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image file|*.jpg;*.png;*.jpeg";
                if (openFileDialog.ShowDialog() == true)
                {
                    var imagePath = openFileDialog.FileName;
                    var filename = Path.GetFileName(imagePath);
                    var newPath = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Products", filename);

                    File.Copy(imagePath, newPath, true);
                    Product.Image = $@"\products\{filename}";
                    MessageBox.Show(SelectPhoto.Content + " прошло успешно", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {

            }
        }

        private void DeleteMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Вы действительно хотите удалить материал у продукта?", "Вопрос",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (MaterialsDataGrid.SelectedItem is ProductsMaterial productsMaterial)
                {
                    Product.ProductsMaterials.Remove(productsMaterial);
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = MessageBox.Show("Вы действительно хотите удалить продукт?", "Вопрос",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (Product.SaleHistories.Any())
                {
                    MessageBox.Show("Продукт нельзя удалить, у него есть продажи!", "Ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        App.DB.ProductsMaterials.RemoveRange(Product.ProductsMaterials);
                        App.DB.MinimumCostHistories.RemoveRange(Product.MinimumCostHistories);
                        App.DB.Products.Remove(Product);
                        App.DB.SaveChanges();
                        DialogResult = true;
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void AddMaterialButton_Click(object sender, RoutedEventArgs e)
        {
            if (MaterialsComboBox.SelectedItem is Material material)
            {
                if (int.TryParse(MaterialAmountTextBox.Text, out int materialAmount))
                {
                    Product.ProductsMaterials.Add(new ProductsMaterial()
                    {
                        MaterialId = material.Id,
                        ProductId = 0,
                        AmountOfMaterial = materialAmount
                    });
                }
                else
                {
                    MessageBox.Show("Количество материала должно быть числом!", "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                }
            }
        }

        private void MaterialsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
