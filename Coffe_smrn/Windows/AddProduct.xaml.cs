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
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        bool isEdit;
        EF.Product editProduct;
        public AddProduct()
        {
            InitializeComponent();
            cb_type.ItemsSource = Classes.AppData.Context.TypeProduct.ToList();
            cb_type.DisplayMemberPath = "Title";
            cb_type.SelectedIndex = 0;
        }

        public AddProduct(EF.Product product)
        {
            InitializeComponent();
            tb_name.Text = "Изменение товара";
            cb_type.ItemsSource = Classes.AppData.Context.TypeProduct.ToList();
            cb_type.DisplayMemberPath = "Title";
            cb_type.SelectedIndex = product.IDTypeProduct-1;
            tb_title.Text = product.Product1;
            tb_description.Text = product.Description;
            tb_price.Text = product.Cost.ToString();
            btn_add.Content = "Изменить";

            isEdit = true;
            editProduct = product;
        }

        private void tb_title_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
                e.Handled = "ячсмитьбюфывапролджэйцукенгшщзхъЯЧСМИТЬБЮФЫВАПРОЛДЖЭЙЦУКЕНГШЩЗХЪ1234567890()-".IndexOf(e.Text) < 0;
        }

        private void tb_price_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_name.Text))
            {
                MessageBox.Show("Введите название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_price.Text))
            {
                MessageBox.Show("Введите цену", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                var resClick = MessageBox.Show("Изменить продукт?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                editProduct.Product1 = tb_title.Text;
                editProduct.Cost = Convert.ToDecimal(tb_price.Text);
                editProduct.Description = tb_description.Text;
                editProduct.IDTypeProduct = cb_type.SelectedIndex + 1;

                Classes.AppData.Context.SaveChanges();
                MessageBox.Show("Продукт изменен");
                this.Close();
            }
            else
            {
                var resClick = MessageBox.Show("Добавить продукт?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                EF.Product product = new EF.Product();
                product.Product1 = tb_title.Text;
                product.Cost = Convert.ToDecimal(tb_price.Text);
                product.Description = tb_description.Text;
                product.IDTypeProduct = cb_type.SelectedIndex + 1;

                Classes.AppData.Context.Product.Add(product);
                Classes.AppData.Context.SaveChanges();
                MessageBox.Show("Продукт добавлен");
                this.Close();
            }
        }
    }
}
