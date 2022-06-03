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

namespace Coffe_smrn.Windows
{
    /// <summary>
    /// Логика взаимодействия для ProductList.xaml
    /// </summary>
    public partial class ProductList : Window
    {
        public ProductList()
        {
            InitializeComponent();
            Filter();
        }

        private void Filter()
        {
            List<EF.Product> products = Classes.AppData.Context.Product.ToList();
            products=products.Where(i => i.IsDeleted == false).ToList();

            products = products.Where(i=>i.TypeProduct.Title.ToLower().Contains(tb_search.Text.ToLower())
            || i.Product1.ToLower().Contains(tb_search.Text.ToLower())
            ).ToList();

            lv_product.ItemsSource =products;
        }
        private void lv_product_KeyDown(object sender, KeyEventArgs e)
        {
            if(lv_product.SelectedItem is EF.Product)
            {
                if (e.Key == Key.Delete || e.Key == Key.Back)
                {
                    var resClick = MessageBox.Show("Удалить товар?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (resClick == MessageBoxResult.No)
                    {
                        return;
                    }
                            var prod = lv_product.SelectedItem as EF.Product;

                            prod.IsDeleted = true;

                            Classes.AppData.Context.SaveChanges();

                            MessageBox.Show("Товар успешно удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                            Filter();
                }
            }
        }

        private void lv_product_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lv_product.SelectedItem is EF.Product)
            {
                var prod = lv_product.SelectedItem as EF.Product;
                AddProduct addProduct= new AddProduct(prod);
                addProduct.ShowDialog();
                Filter();
            }                
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            AddProduct addProduct = new AddProduct();
            addProduct.ShowDialog();
            Filter();
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

    }
}
