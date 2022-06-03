using Coffe_smrn.Windows;
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

namespace Coffe_smrn
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if(Classes.GlobalValues.ExtendedAccess!=true)
            {
                btn_report.Visibility = Visibility.Collapsed;
                btn_productList.Visibility = Visibility.Collapsed;
                btn_employeeList.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_makeOrder_Click(object sender, RoutedEventArgs e)
        {
            MakeOrder makeOrder = new MakeOrder();
            this.Hide();
            makeOrder.ShowDialog();
            this.ShowDialog();
        }

        private void btn_report_Click(object sender, RoutedEventArgs e)
        {
            Reports reports = new Reports();
            this.Hide();
            reports.ShowDialog();
            this.ShowDialog();
        }

        private void btn_employeeList_Click(object sender, RoutedEventArgs e)
        {
            EmployeeList employeeList = new EmployeeList();
            this.Hide();
            employeeList.ShowDialog();
            this.ShowDialog();
        }

        private void btn_productList_Click(object sender, RoutedEventArgs e)
        {
            ProductList productList= new ProductList();
            this.Hide();
            productList.ShowDialog();
            this.ShowDialog();
        }
    }
}
