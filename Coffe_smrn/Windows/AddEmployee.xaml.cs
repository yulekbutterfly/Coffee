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
    /// Логика взаимодействия для AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {

        bool isEdit;
        EF.Employee editEmpl;
        public AddEmployee()
        {
            InitializeComponent();
            cb_gender.ItemsSource = Classes.AppData.Context.Gender.ToList();
            cb_gender.DisplayMemberPath = "Title";
            cb_gender.SelectedIndex = 0;
            cb_role.ItemsSource = Classes.AppData.Context.Role.ToList();
            cb_role.DisplayMemberPath = "Title";
            cb_role.SelectedIndex = 0;
        }

        public AddEmployee(EF.Employee employee)
        {
            InitializeComponent();
            cb_gender.ItemsSource = Classes.AppData.Context.Gender.ToList();
            cb_gender.DisplayMemberPath = "Title";
            cb_gender.SelectedIndex = employee.IDGender-1;
            cb_role.ItemsSource = Classes.AppData.Context.Role.ToList();
            cb_role.DisplayMemberPath = "Title";
            cb_role.SelectedIndex = employee.IDRole-1;
            tb_name.Text = "Изменение работника";
            tb_firstName.Text =employee.FirstName;
            tb_secondName.Text=employee.SecondName;
            tb_patronumic.Text = employee.Patronumic;
            tb_pass.Text = employee.Password;
            tb_phone.Text = employee.Phone;
            tb_login.Text=employee.Login;

            editEmpl = employee;
            isEdit = true;
        }

        private void fio_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "ячсмитьбюфывапролджэйцукенгшщзхъЯЧСМИТЬБЮФЫВАПРОЛДЖЭЙЦУКЕНГШЩЗХЪ-".IndexOf(e.Text) < 0;
        }

        private void tb_phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_firstName.Text))
            {
                MessageBox.Show("Введите имя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_secondName.Text))
            {
                MessageBox.Show("Введите фамилию", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_login.Text))
            {
                MessageBox.Show("Введите логин", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_pass.Text))
            {
                MessageBox.Show("Введите пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrWhiteSpace(tb_phone.Text))
            {
                MessageBox.Show("Укажите телефон", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tb_phone.Text.Length!=11)
            {
                MessageBox.Show("Длина телефона должна составлять 12 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (isEdit)
            {
                var resClick = MessageBox.Show("Изменить сотрудника?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                editEmpl.FirstName = tb_firstName.Text;
                editEmpl.SecondName = tb_secondName.Text;
                editEmpl.Patronumic = tb_patronumic.Text;
                editEmpl.Password = tb_pass.Text;
                editEmpl.Login = tb_login.Text;
                editEmpl.Phone = tb_phone.Text;
                editEmpl.IDGender = cb_gender.SelectedIndex + 1;
                editEmpl.IDRole = cb_role.SelectedIndex + 1;

                Classes.AppData.Context.SaveChanges();
                MessageBox.Show("Сотрудник изменен");
                this.Close();
            }    
            else
            {
                var resClick = MessageBox.Show("Добавить сотрудника?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                EF.Employee employee = new EF.Employee();
                employee.FirstName = tb_firstName.Text;
                employee.SecondName = tb_secondName.Text;
                employee.Patronumic = tb_patronumic.Text;
                employee.Password = tb_pass.Text;
                employee.Login = tb_login.Text;
                employee.Phone = tb_phone.Text;
                employee.IDGender = cb_gender.SelectedIndex + 1;
                employee.IDRole = cb_role.SelectedIndex + 1;

                Classes.AppData.Context.Employee.Add(employee);
                Classes.AppData.Context.SaveChanges();
                MessageBox.Show("Сотрудник добавлен");
                this.Close();
            }
            
        }

        private void tb_loginAndPass_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "zxcvbnmasdfghjklqwertyuiopZXCVBNMASDFGHJKLQWERTYUIOP!@$;:?#*1234567890-_=+".IndexOf(e.Text) < 0;

        }
    }
}
