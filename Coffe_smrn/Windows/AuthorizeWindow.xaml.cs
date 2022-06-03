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
    /// Логика взаимодействия для AuthorizeWindow.xaml
    /// </summary>
    public partial class AuthorizeWindow : Window
    {
        public AuthorizeWindow()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {

            var authUser = Classes.AppData.Context.Employee.ToList().
                Where(i => i.Login == tb_login.Text && i.Password == tb_pass.Password).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(tb_login.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_pass.Password))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(authUser != null)
            {
                if(authUser.IDRole==2)
                {
                    ExtendedAccess = true;
                }
                AuthorizedEmpl = authUser;
                MainWindow mainWindow = new MainWindow();
                this.Hide();
                mainWindow.ShowDialog();
                this.ShowDialog();

            }
            else
            {
                MessageBox.Show("Пользователь не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
