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
    /// Логика взаимодействия для Reports.xaml
    /// </summary>
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
            dp_end.SelectedDate = DateTime.Now;
            dp_begin.SelectedDate = DateTime.Now.AddMonths(-1);
            Filter();
        }

        private void Filter()
        {
            decimal sum = 0;
            List<EF.VW_OrderProduct> report = Classes.AppData.Context.VW_OrderProduct.ToList();
            if(dp_begin.SelectedDate > dp_end.SelectedDate)
            {
                report = report.Where(i => i.TimeOfOrder <= dp_begin.SelectedDate && i.TimeOfOrder >= dp_end.SelectedDate).ToList();
            }
            else
            {
                report = report.Where(i => i.TimeOfOrder >= dp_begin.SelectedDate && i.TimeOfOrder <= dp_end.SelectedDate).ToList();
            }

            lv_report.ItemsSource=report;

            foreach(EF.VW_OrderProduct product in report)
            {
                sum += Convert.ToDecimal(product.sumOfOrder);
            }
            tb_finalSum.Text=sum.ToString();
        }
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void dp_begin_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void dp_end_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
