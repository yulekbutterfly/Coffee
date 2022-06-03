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
using static Coffe_smrn.Classes.GlobalValues;


namespace Coffe_smrn.Windows
{
    /// <summary>
    /// Логика взаимодействия для MakeOrder.xaml
    /// </summary>
    public partial class MakeOrder : Window
    {
        List<EF.Product> productOrder = new List<EF.Product>();
        public MakeOrder()
        {
            InitializeComponent();
            tb_empl.Text = AuthorizedEmpl.FIO;
            dp_dateOfOrder.SelectedDate = DateTime.Now;
            lv_available.ItemsSource = Classes.AppData.Context.Product.ToList();
        }

        private void tb_searchAvalible_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(tb_searchAvalible.Text))
            {
                lv_available.ItemsSource = Classes.AppData.Context.Product.ToList();
            }
            else
            {
                List<EF.Product> product = Classes.AppData.Context.Product.Where(i => i.Product1.ToLower().Contains(tb_searchAvalible.Text.ToLower())).ToList();
                lv_available.ItemsSource = product;
            }
        }

        private void tb_searchOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<EF.Product> productOrderFilter = productOrder.Where(i => i.Product1.ToLower().Contains(tb_searchOrder.Text.ToLower())).ToList();
            lv_order.ItemsSource = productOrderFilter;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            EF.Order order= new EF.Order();
            EF.OrderProduct orderProduct = new EF.OrderProduct();
            order.IDEmployee = AuthorizedEmpl.ID;
            order.TimeOfOrder =Convert.ToDateTime(dp_dateOfOrder.SelectedDate);
            Classes.AppData.Context.Order.Add(order);
            Classes.AppData.Context.SaveChanges();
            foreach(EF.Product product in productOrder)
            {
                orderProduct.IDOrder = order.ID;
                orderProduct.IDProduct = product.ID;
                Classes.AppData.Context.OrderProduct.Add(orderProduct);
                Classes.AppData.Context.SaveChanges();
            }

            MessageBox.Show("Данные внесены");
            this.Close();
        }

        private void lv_order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lv_order.SelectedItem is EF.Product)
            {
                var product = lv_order.SelectedItem as EF.Product;
                productOrder.Remove(product);
                lv_order.ItemsSource = productOrder.ToList();
            }
        }

        private void lv_available_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lv_available.SelectedItem is EF.Product)
            {
                var product =lv_available.SelectedItem as EF.Product;
                productOrder.Add(product);
                lv_order.ItemsSource=productOrder.ToList();
            }
        }
    }
}
